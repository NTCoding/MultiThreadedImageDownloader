namespace ImageDownloader
{
	public interface IImageProvider
	{
		byte[] GetImage(string src);
	}
}