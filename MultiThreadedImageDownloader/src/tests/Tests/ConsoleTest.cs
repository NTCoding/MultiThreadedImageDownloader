using System;
using System.Collections.Generic;
using ImageDownloader;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class ConsoleTest
	{
		[Test]
		public void DownloadImages_WithSystemThreadingTask()
		{
			var downloader = GetDownloader(new SystemThreadTaskHandler());

			var images = downloader.Download("http://www.bbc.co.uk");

			WriteUrlsToConsole(images);
		}

		[Test]
		public void DownloadImages_WithParallel()
		{
			var downloader = GetDownloader(new ParallelTaskHandler());

			var images = downloader.Download("http://www.bbc.co.uk");

			WriteUrlsToConsole(images);
		}

		private SuperImageDownloader GetDownloader(ITaskHandler handler)
		{
			return new SuperImageDownloader(new HtmlRetriever(), new ImageParser(), new ImageRetriever(new ImageProvider(), handler));
		}

		private void WriteUrlsToConsole(IEnumerable<DownloadedImageDTO> images)
		{
			foreach (var item in images)
			{
				Console.WriteLine(item.URL);
			}
		}
	}
}