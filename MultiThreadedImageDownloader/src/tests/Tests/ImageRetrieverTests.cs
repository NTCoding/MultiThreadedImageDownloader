using System.Collections.Generic;
using ImageDownloader;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests
{
	[TestFixture]
	public class ImageRetrieverTests
	{
		private IImageProvider provider;
		private ImageRetriever retriever;

		[SetUp]
		public void SetUp()
		{
			provider = MockRepository.GenerateMock<IImageProvider>();
			retriever = new ImageRetriever(provider);
		}

		[Test]
		public void ShouldAsk_ImageProvider_ToGetEachImage()
		{
			var testSrcs = new [] {"blah", "bloo", "bleh"};

			retriever.RetrieveFor(testSrcs);

			foreach (var src in testSrcs)
			{
				provider.AssertWasCalled(p => p.GetImage(src));
			}
		}
	}
}