using Baron.Entity;
using CustomTools;
using System;

namespace Baron.Service
{
	public abstract class BackgroundMediaService
	{
		protected GameBase _gameBase;

		public BackgroundMediaService(GameBase gameBase)
		{
			_gameBase = gameBase;
		}

		public void Resume(Scenario scenario//, GameBase gameBase
			)
		{

			try
			{
				Execute(scenario//, gameBase
					);
			}
			catch (Exception e)
			{
				CustomLogger.Log("BackgroundMediaService resumen" + e.Message);
			}
		}

		public abstract void Execute(Scenario scenario//, GameBase gameBase
			);

	}
}
