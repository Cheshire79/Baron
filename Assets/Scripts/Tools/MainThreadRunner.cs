using CustomTools;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Baron.Tools
{
	public class MainThreadRunner : MonoBehaviour
	{
		[Flags]
		public enum MainThreadTaskType
		{
			Default = 0x01,
			CloseAllViews = 0x02,
			All = 0xFF
		}
		private static MainThreadRunner _instance;
		private readonly List<Couple<MainThreadTaskType, Action>> _tasks = new List<Couple<MainThreadTaskType, Action>>();
		private Thread _mainThread;

		public static bool IsBackGround { get; private set; }

		public static bool IsMainThread
		{
			get { return Thread.CurrentThread == _instance._mainThread; }
		}

		// ReSharper disable once UnusedMember.Local
		private void Awake()
		{
			CustomLogger.Log(CustomLogger.LogComponents.Test, string.Format("Screen.width = {0} Screen.height = {1}", Screen.width, Screen.height));
			Application.targetFrameRate = 60;
			if (_instance != null)
				throw new Exception("Duplicating CoroutineStarter");
			_instance = this;
			_instance._mainThread = Thread.CurrentThread;
		}

		public static event Action<bool> ApplicationPaused;

		private static void OnApplicationPaused(bool flag)
		{
			Action<bool> handler = ApplicationPaused;
			if (handler != null) handler(flag);
		}

		// ReSharper disable once UnusedMember.Local
		private void OnApplicationPause(bool flag)
		{
			CustomLogger.Log(CustomLogger.LogComponents.Test, string.Format(" MainThreadRunner OnApplicationPause{0}", flag));
			IsBackGround = flag;
			//if (!flag) // if we restored from BackGround we need to clear list of action to avoid the calling actualize application twice ()
			// _lobbyController.OnTableCloseRequested(); can perform  before  _gameController.OnReconnectSucceeded(); finished (it consist from few MainThread.AddTask block)

			// but we need not to clear list of action when after leaving the GameTableView
			// Clear();
			OnApplicationPaused(flag);

		}

		public static void AddTask(Action task, MainThreadTaskType type = MainThreadTaskType.Default)
		{
			if (IsMainThread && _instance._tasks.Count == 0)
				task();
			else
				lock (_instance._tasks)
					_instance._tasks.Add(new Couple<MainThreadTaskType, Action>(type, task));
		}

		// ReSharper disable once UnusedMember.Local
		private void Update()
		{
			if (_tasks.Count < 1) return;
			try
			{
				//CustomLogger.LogWarning("Run task");
				Action action;
				while (_tasks.Count > 0)
				{
					lock (_tasks)
					{
						action = _tasks[0].Second;
						_tasks.RemoveAt(0);
					}
					action();
				}

			}
			catch (Exception ex)
			{
				CustomLogger.LogException(ex);
			}
		}
		public static void Clear(MainThreadTaskType type = MainThreadTaskType.All)
		{
			if (_instance._tasks.Count > 0)
				try
				{
					CustomLogger.LogError("Main tread clear task");

					lock (_instance._tasks)
					{
						for (int i = 0; i < _instance._tasks.Count; i++)
						{
							if ((_instance._tasks[i].First & type) == _instance._tasks[i].First)
							{
								_instance._tasks.RemoveAt(i);
								i--;
							}
						}
					}
				}
				catch (Exception ex)
				{
					CustomLogger.LogException(ex);
				}
		}

	}
}
