using Baron.Controller;
using Baron.Entity;
using CustomTools;
using System;

namespace Baron.Service
{
	public abstract class BackgroundMediaService
	{
		protected GameBase _gameBase;
	
		public BackgroundMediaService(GameBase gameBase	)
		{
			_gameBase = gameBase;
		}


		public abstract void Execute(Scenario scenario);

		public virtual void Pause()
		{
		}

	}
}
