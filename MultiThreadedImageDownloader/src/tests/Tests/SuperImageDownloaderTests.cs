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
		private IHtmlRetriever retriever;
		private SuperImageDownloader downloader;

		[SetUp]
		public void SetUp()
		{
			parser     = MockRepository.GenerateMock<IImageParser>();
			retriever  = MockRepository.GenerateMock<IHtmlRetriever>();
			downloader = new SuperImageDownloader();
		}

		[Test]
		public void ShouldFetchHtml_AndPassToImageParser()
		{
			string testUrl = "www.test.blah";

			retriever.Stub(r => r.GetHtml(testUrl)).Return(GetTestHtml());
			
			downloader.Download(testUrl);

			parser.AssertWasCalled(p => p.Parse(GetTestHtml()));
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

	public interface IImageParser
	{
		IEnumerable<string> Parse(string html);
	}

	public interface IHtmlRetriever
	{
		string GetHtml(string url);
	}
}