using System;

namespace ImageDownloader
{
	public class DownloadedImageDTO
	{
		public DownloadedImageDTO(string url, byte[] data)
		{
			URL = url;
			Data = data;
		}

		public String URL { get; private set; }

		public byte[] Data { get; private set; }
	}
}