using System.Collections.Generic;
using System.Linq;
using ImageDownloader;
using NUnit.Framework;

namespace Tests.Integration.Utils
{
	public static class DownloadedImageDTOAssertions
	{
		public static void ShouldMatch (this IEnumerable<DownloadedImageDTO> expected, IEnumerable<DownloadedImageDTO> actual)
		{
			foreach (var dto in actual)
			{
				Assert.That(expected.Any(d => IsMatch(d, dto)), "No match for: " + dto.URL);
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