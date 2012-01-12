using System;
using System.Collections.Generic;

namespace ImageDownloader
{
	public interface ITaskHandler
	{
		void HandleTasks(IEnumerable<string> objects, Action<object> task);
	}
}