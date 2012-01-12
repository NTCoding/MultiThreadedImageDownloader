using System;
using ImageDownloader;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests
{
	[TestFixture]
	public class SuperImageDownloaderTests
	{
		private IImageParser parser;
		private IHtmlRetriever retriever;
		private SuperImageDownloader downloader;

		[SetUp]
		public void SetUp()
		{
			parser     = MockRepository.GenerateMock<IImageParser>();
			retriever  = MockRepository.GenerateMock<IHtmlRetriever>();
			downloader = new SuperImageDownloader(retriever, parser);
		}

		[Test]
		public void ShouldFetchHtml_AndPassToImageParser()
		{
			string testUrl = "www.test.blah";

			retriever.Stub(r => r.GetHtml(testUrl)).Return(GetTestHtml());
			
			downloader.Download(testUrl);

			parser.AssertWasCalled(p => p.Parse(GetTestHtml(), testUrl));
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