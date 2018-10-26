using Baron.Entity;
using Baron.Service;
using CustomTools;
using System;

namespace Baron.Listener
{
	public class ImageViewedInBranchListener
	{
		private GameBase _gameBase;
		AchievementPointService _aps;

		public ImageViewedInBranchListener(GameBase gameBase)
		{
			_gameBase = gameBase;
			_aps = new AchievementPointService(_gameBase);
		}

		public void OnReceive(string id)
		{
			CustomLogger.Log("ImageViewedInBranchListener OnReceivestring=============================== " + id);
			try
			{
				History.History history = _gameBase.History;
				if (history == null) return;

				Image image = ImageRepository.Find(_gameBase, id);
				if (image == null) return;

				if (history.AddImage(image))
				{
					
					_aps.OnImageOpened(image);
					_aps.UnlockAllImagesIfCompleted();

					//todo syncHistory();
					history.ValidateProgress();
				}

			}
			catch (Exception e)
			{
				CustomLogger.Log("ImageViewedInBranchListener " + e);
			}
		}
	}
}
