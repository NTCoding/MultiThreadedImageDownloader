using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ImageDownloader
{
	// TODO - we could test this and delegate calling fetching the resource to a resource fetcher
	public class ImageRetriever : IImageRetriever
	{
		private WebClient client;

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
			var dto = new DownloadedImageDTO(src, GetImageDataFor(src));
			images.Add(dto);
		}

		private byte[] GetImageDataFor(string src)
		{
			var request = WebRequest.Create(src);
			var response = request.GetResponse();

			using (var reader = new BinaryReader(response.GetResponseStream()))
			{
				return reader.ReadBytes((int)response.ContentLength);
			}
		}
	}
}