using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImageDownloader;
using NUnit.Framework;

namespace Tests.Integration
{
	[TestFixture]
	public class DownloadingImages
	{
		private const string UrlForTestHtmlPage = "http://www.test.blah/";

		[Test]
		public void GivenUrl_ForAnHtmlPage_ShouldDownloadAllImages()
		{
			// TOD - parameter
			var downloader = new SuperImageDownloader(new TestHtmlRetriever(), new ImageParser(), TODO);
			var downloadedImages = downloader.Download(UrlForTestHtmlPage);

			downloadedImages.ShouldMatch(GetImagesInTestHtmlPage());
		}

		// Move into a test helper
		private IEnumerable<DownloadedImageDTO> GetImagesInTestHtmlPage()
		{
			var absolute = new[]
			               	{
			               		"http://a3.twimg.com/profile_images/1174667503/04_normal.jpg",
			               		"http://widgets.twimg.com/i/widget-logo.png",
			               	};

			foreach (var s in absolute)
			{
				yield return new DownloadedImageDTO(s, GetTestingImageData());
			}

			var relative = new[]
			           	{

							"images/struq_logo_02.png",
							"pictures/homepage_banner_01.jpg",
							"pictures/homepage_banner_02.jpg",
							"pictures/homepage_banner_03.jpg",
							"images/divider_horizontal_01.gif",
							"pictures/logo_lovefilm_small.gif",
							"pictures/logo_king_small.gif",
							"pictures/logo_apponline_small.gif",
							"pictures/thumb_whatsnew.jpg",
							"images/loader.gif",
							"pictures/thumb_livedemo.jpg"
			           	};

			foreach (var s in relative)
			{
				yield return new DownloadedImageDTO(UrlForTestHtmlPage + s, GetTestingImageData());
			}
		}

		private byte[] GetTestingImageData()
		{
			return new[]
			       	{
			       		(byte) 1,
			       		(byte) 2,
			       		(byte) 3
			       	};
		}

		// TODO - Implement using Rx extensions if have time
	}

	public class TestHtmlRetriever : IHtmlRetriever
	{
		public string GetHtml(string url)
		{
			var projRoot = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName + "\\Tests.Integration";
			var htmlPath = Path.Combine(projRoot, "Resources", "blah.html");

			return File.ReadAllText(htmlPath);
		}
	}

	public static class DownloadedImageDTOAssertions
	{
		public static void ShouldMatch (this IEnumerable<DownloadedImageDTO> expected, IEnumerable<DownloadedImageDTO> actual)
		{
			foreach (var dto in actual)
			{
				Assert.That(expected.Any(d => IsMatch(d, dto)));
			}
		}

		// TODO - move into a comparer
		private static bool IsMatch(DownloadedImageDTO first, DownloadedImageDTO second)
		{
			return first.URL == second.URL
			       && first.Data == second.Data;
		}
	}
}