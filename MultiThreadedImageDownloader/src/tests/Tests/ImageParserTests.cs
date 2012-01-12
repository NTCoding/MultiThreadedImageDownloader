using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

			string html = GetHtmlFor(new[] {testUrl});

			var result = parser.Parse(html);

			ShouldContainUrls(result, new[] {testUrl});
		}

		private string GetHtmlFor(IEnumerable<string> urls)
		{
			var start = @"<html><head></head><body>";
			var end = @"</body></body></html>""";

			var html = start;
			foreach (var image in GetImages(urls))
			{
				html += image;
			}

			return html + end;

		}

		private IEnumerable<string> GetImages(IEnumerable<string> url)
		{
			foreach (var u in url)
			{
				yield return @"<img src=""" + u + @""" />";
			}
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

	public class ImageParser : IImageParser
	{
		public IEnumerable<string> Parse(string html)
		{
			string regex = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";
			var matches = Regex.Matches(html, regex, RegexOptions.IgnoreCase);
			foreach (Match match in matches)
			{
				yield return match.Groups[1].Value;
			}

			
		}
	}
}