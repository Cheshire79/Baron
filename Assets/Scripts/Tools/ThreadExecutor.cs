using CustomTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Baron.Tools
{
	public class ThreadExecutor
	{
		private readonly Queue<Action> _actions;
		private readonly AutoResetEvent _locker = new AutoResetEvent(false);
		private readonly Thread _thread;
		private bool _isRunning;
		private static ThreadExecutor _instance;

		public static ThreadExecutor Instance
		{
			get { return _instance ?? (_instance = new ThreadExecutor()); }
		}

		private ThreadExecutor()
		{
			_thread = new Thread(Run);
			_isRunning = true;
			_actions = new Queue<Action>();
			_thread.Start();
		}

		private void Run()
		{
			while (_isRunning)
			{
				while (_actions.Count > 0)
				{
					Action action;
					lock (_actions)
						action = _actions.Dequeue();
					action();
				}
				_locker.WaitOne();
			}
		}

		public void PushTask(Action action)
		{
			//action();
			lock (_actions)
				_actions.Enqueue(action);
			_locker.Set();
		}

		public void Abort()
		{
			_isRunning = false;
			_locker.Set();
			_thread.Join();
		}
		public static void Clear()
		{
			if (_instance._actions.Count > 0)
				try
				{
					CustomLogger.LogError("Thread clear task");

					lock (_instance._actions)
					{
						_instance._actions.Clear();
					}
				}
				catch (Exception ex)
				{
					CustomLogger.LogException(ex);
				}
		}
	}
}
