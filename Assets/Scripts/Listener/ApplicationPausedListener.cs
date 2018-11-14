using Baron.Service;
using CustomTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baron.Listener
{

	public class ApplicationPausedListener
	{
		private GameBase _gameBase;

		public ApplicationPausedListener(GameBase gameBase)
		{
			_gameBase = gameBase;
			
		}

		public void onReceive()
		{
			
		}

					
	}
}
