using System;
using System.Collections.Generic;
using System.Linq;
using ImageDownloader;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class ImageParserTests
	{
		private new ImageParser parser;

		[SetUp]
		public void SetUp()
		{
			parser = new ImageParser();
		}

		[Test]
		public void ShouldParse1Image()
		{
			var testUrl = "http://www.blah.com";

			var html = @"
							<html><head></head><body>
							<img src=""" + testUrl + @"""/></body></body></html>""";
							

			var result = parser.Parse(html);

			ShouldContainUrls(result, new[] {testUrl});
		}

		private void ShouldContainUrls(IEnumerable<string> result, IEnumerable<string> expected)
		{
			Assert.AreEqual(result.Count(), expected.Count());
			foreach (var url in expected)
			{
				Assert.That(result.Contains(url));
			}
		}

		[Test]
		public void ShouldParse5Images()
		{
			Assert.Fail();
		}

		[Test]
		public void ShouldReturnFull_ForAbsoluteUrls()
		{
			Assert.Fail();
		}
	}

	internal class ImageParser : IImageParser
	{
		public IEnumerable<string> Parse(string html)
		{

			return Enumerable.Empty<string>();
		}
	}
}