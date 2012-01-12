using System.Collections.Generic;
using ImageDownloader;
using NUnit.Framework;
using Rhino.Mocks;

namespace Tests
{
	// TODO - try and test this when you are not falling asleep

	[TestFixture]
	public class ImageRetrieverTests
	{
		private IImageProvider provider;
		private ImageRetriever retriever;

		[SetUp]
		public void SetUp()
		{
			provider = MockRepository.GenerateMock<IImageProvider>();
			retriever = new ImageRetriever(provider, MockRepository.GenerateMock<ITaskHandler>());
		}

		[Test][Ignore]
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