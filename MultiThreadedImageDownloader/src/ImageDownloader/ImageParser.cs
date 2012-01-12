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
			       	: (url + "/" + src).Replace("///", "/").Replace("//", "/");
		}
	}
}