using System.IO;
using System.Net;

namespace ImageDownloader
{
	public class ImageProvider : IImageProvider
	{
		public byte[] GetImage(string src)
		{
			var request = WebRequest.Create(src);
			var response = request.GetResponse();

			using (var reader = new BinaryReader(response.GetResponseStream()))
			{
				return reader.ReadBytes((int)response.ContentLength);
			}
		}
	}
}