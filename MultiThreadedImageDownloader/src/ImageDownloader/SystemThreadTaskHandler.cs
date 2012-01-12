using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageDownloader
{
	public class SystemThreadTaskHandler : ITaskHandler
	{
		public void HandleTasks(IEnumerable<string> objects, Action<object> task)
		{
			var tasks = new List<Task>();
			foreach (var src in objects)
			{
				var t = new Task(task, src);
				tasks.Add(t);
				t.Start();
			}

			Task.WaitAll(tasks.ToArray());
		}
	}
}