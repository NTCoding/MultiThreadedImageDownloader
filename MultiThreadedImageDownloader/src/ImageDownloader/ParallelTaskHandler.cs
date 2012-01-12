using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageDownloader
{
	public class ParallelTaskHandler : ITaskHandler
	{
		public void HandleTasks(IEnumerable<string> objects, Action<object> task)
		{
			Parallel.ForEach(objects, task);
		}
	}
}