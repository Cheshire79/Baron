using Baron.Controller;
using Baron.Entity;
using CustomTools;
using System;

namespace Baron.Service
{
	public abstract class BackgroundMediaService
	{
		protected GameBase _gameBase;
		protected IBranchViewController _branchViewController;

		public BackgroundMediaService(GameBase gameBase, IBranchViewController branchViewController)
		{
			_gameBase = gameBase;
			_branchViewController = branchViewController;
		}

		//public void Resume(Scenario scenario)
		//{

		//	try
		//	{
		//		Execute(scenario);
		//	}
		//	catch (Exception e)
		//	{
		//		CustomLogger.Log("BackgroundMediaService resume" + e.Message);
		//	}
		//}

		public abstract void Execute(Scenario scenario);
		public void Pause()
		{

		}

	}
}
