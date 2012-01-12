using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ImageDownloader
{
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
			       	: ConvertToAbsolute(src, url);
		}

		private string ConvertToAbsolute(string src, string url)
		{
			var host = url.EndsWith(@"/")
			            	? url
			            	: url + "/";

			var path = src.StartsWith(@"/")
			           	? src.Substring(1)
			           	: src;

			return host + path;
		}
	}
}