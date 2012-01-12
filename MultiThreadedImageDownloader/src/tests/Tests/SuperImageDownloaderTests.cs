using System;
using System.Collections.Generic;
using ImageDownloader;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests
{
	[TestFixture]
	public class SuperImageDownloaderTests
	{
		private IImageParser parser;
		private IHtmlRetriever htmlRetriever;
		private SuperImageDownloader downloader;
		private IImageRetriever imageRetriever;

		[SetUp]
		public void SetUp()
		{
			parser         = MockRepository.GenerateMock<IImageParser>();
			htmlRetriever  = MockRepository.GenerateMock<IHtmlRetriever>();
			imageRetriever = MockRepository.GenerateMock<IImageRetriever>();
			downloader     = new SuperImageDownloader(htmlRetriever, parser, imageRetriever);
		}

		[Test]
		public void ShouldFetchHtml_AndPassToImageParser()
		{
			string testUrl = "www.test.blah";

			htmlRetriever.Stub(r => r.GetHtml(testUrl)).Return(GetTestHtml());
			
			downloader.Download(testUrl);

			parser.AssertWasCalled(p => p.Parse(GetTestHtml(), testUrl));
		}

		[Test]
		public void ShouldPassParsedSrcs_ToImageRetriever()
		{
			string testUrl = "www.blah.com";

			IEnumerable<string> testSrcs = new[]
			                               	{
												"http://www.blah.com",
												"http://www.bbc.co.uk",
			                               	};

			parser.Stub(p => p.Parse(null, testUrl)).Return(testSrcs);

			downloader.Download(testUrl);

			imageRetriever.AssertWasCalled(i => i.RetrieveFor(testSrcs));
		}

		[Test]
		public void ShouldReturn_ReturnValuesFromImageRetriever()
		{
			var expected = new List<DownloadedImageDTO> {new DownloadedImageDTO("blah", null)};

			imageRetriever.Stub(i => i.RetrieveFor(null)).Return(expected);

			var actual = downloader.Download("blah");

			Assert.AreEqual(expected, actual);
		}

		private string GetTestHtml()
		{
			return @"
					  <html>
						  <head>
						  </head>
						  <body>
								<img src=""http://www.blah.com"" />
						  </body>
					  </html>
					";
		}
	}
}