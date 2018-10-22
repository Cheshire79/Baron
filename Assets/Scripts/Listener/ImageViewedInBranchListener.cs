using Baron.Entity;
using Baron.Service;
using CustomTools;
using System;

namespace Baron.Listener
{
	public class ImageViewedInBranchListener
	{
		protected GameBase _gameBase;

		public ImageViewedInBranchListener(GameBase gameBase)
		{
			_gameBase = gameBase;
		}

		public void OnReceive(string id)
		{
			try
			{
				History.History history = _gameBase.History;
				if (history == null) return;

				Image image = ImageRepository.find(_gameBase, id);
				if (image == null) return;

				if (history.AddImage(image))
				{
					AchievementPointService aps = new AchievementPointService(_gameBase);
					aps.OnImageOpened(image);
					aps.UnlockAllImagesIfCompleted();

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
