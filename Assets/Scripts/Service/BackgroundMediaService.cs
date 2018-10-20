using Baron.Entity;
using Baron.Service;
using CustomTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Service
{
	public abstract class BackgroundMediaService
	{
		public void Resume(Scenario scenario, GameBase gameBase)
		{

			try
			{
				Execute(scenario, gameBase);
			}
			catch (Exception e)
			{
				CustomLogger.Log("BackgroundMediaService resumen" + e.Message);
			}
		}

		public abstract void Execute(Scenario scenario, GameBase gameBase);

	}
}
