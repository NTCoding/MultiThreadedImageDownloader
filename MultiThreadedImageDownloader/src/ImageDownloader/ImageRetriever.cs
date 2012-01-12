using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ImageDownloader
{
	public class ImageRetriever : IImageRetriever
	{
		private readonly IImageProvider provider;

		public ImageRetriever(IImageProvider provider)
		{
			this.provider = provider;
		}

		public IEnumerable<DownloadedImageDTO> RetrieveFor(IEnumerable<string> srcs)
		{
			var images = new List<DownloadedImageDTO>();
			var tasks = new List<Task>();
			foreach (var src in srcs)
			{
				var t = new Task((s) => AddImageFor((string)s, ref images), src);
				tasks.Add(t);
				t.Start();
			}
			
			Task.WaitAll(tasks.ToArray());

			return images;
		}

		private void AddImageFor(string src, ref List<DownloadedImageDTO> images)
		{
			var dto = new DownloadedImageDTO(src, TryGetImageDataFor(src));
			images.Add(dto);
		}

		private byte[] TryGetImageDataFor(string src)
		{
			try
			{
				return provider.GetImage(src);
			}
			catch (Exception)
			{
				// TODO - This would be more involved logic - some images may correctly not exist,
				//        while it could be a bug in the system. Manually log for now
				EventLog.WriteEntry("Application", "Failed to get image for: " + src);
				Console.WriteLine("Failed to get image for: " + src);
				return new byte[0];
			}
			
		}
	}
}