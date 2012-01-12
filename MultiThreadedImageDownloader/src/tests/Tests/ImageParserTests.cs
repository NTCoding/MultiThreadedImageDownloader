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
			var testSrc = "http://www.blah.com";

			var result = parser.Parse(GetHtmlFor(new[] {testSrc}), "blah");

			ShouldContainUrls(result, new[] {testSrc});
		}

		[Test]
		public void ShouldParse5Images()
		{
			var testSrcs = new[]
			               	{
								"http://www.ntcoding.co.uk",
								"http://www.struq.com",
								"http://www.bbc.co.uk",
								"http://www.google.co.uk",
								"http://www.planetf1.com"
			               	};

			var result = parser.Parse(GetHtmlFor(testSrcs), "blah");

			ShouldContainUrls(result, testSrcs);
		}

		[Test]
		public void ShouldReturnFullSrc_ForAbsoluteUrls()
		{
			var rootUrl = "http://www.struq.com/";
			var testSrcs = new[]
			               	{
								"public/image/55.png",
								"/public/images/44.png",
								"blah/blah/blah/image.gif"
			               	};
			var absoluteSrcs = testSrcs.Select(t => (rootUrl + t).Replace(@"//", @"/"));

			var result = parser.Parse(GetHtmlFor(testSrcs), "blah");

			ShouldContainUrls(result, absoluteSrcs);
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

		
	}

	public class ImageParser : IImageParser
	{
		public IEnumerable<string> Parse(string html, string url)
		{
			string regex = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";
			var matches = Regex.Matches(html, regex, RegexOptions.IgnoreCase);
			foreach (Match match in matches)
			{
				yield return GetAbsoluteSrc(match, url);
			}

			
		}

		private string GetAbsoluteSrc(Match match, string url)
		{
			var src = match.Groups[1].Value;
			return src.StartsWith("http")
			       	? src
			       	: url + src;
		}
	}
}