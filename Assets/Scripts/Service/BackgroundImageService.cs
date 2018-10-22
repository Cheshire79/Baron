using Baron.Entity;
using Baron.Listener;
using CustomTools;
using System;


namespace Baron.Service
{
	public class BackgroundImageService: BackgroundMediaService
	{
		private ImageViewedInBranchListener _imageViewedInBranchListener;
		public BackgroundImageService(GameBase gameBase) :base(gameBase)
		{
			_imageViewedInBranchListener = new ImageViewedInBranchListener(gameBase);
		}
		public const string PREVIOUS_BACKGROUND = "unknown";

		public override void Execute(Scenario scenario//, GameBase gameBase
			)
		{

			// if (!BranchPresenter.isCreated()) return;

			//BranchPresenter presenter = BranchPresenter.getInstance();

			try
			{

				TrackBranch trackBranch = scenario.CurrentBranch;
				if (trackBranch == null) return;

			//	GameBase gameBase = presenter.getGameBase();
				if (_gameBase == null) return;

				History.History history = _gameBase.History;
				if (history == null) return;

				Option option = OptionRepository.find(_gameBase, trackBranch.OptionId);

				if (option == null) return;

				int milliseconds = scenario.Progress;

				if (option.IsProxy)
				{

					if (option.CurrentImage != null)
					{
						if (option.CurrentImage.IsLocked) return;
					}

					option.CurrentImage = new TrackImage();
					option.CurrentImage.Id = history.GetCurrentBackground();
					option.CurrentImage.Duration = option.Duration;
				}

				TrackImage currentTrackMedia = option.CurrentImage;
				if (currentTrackMedia == null) return;

				currentTrackMedia.Progress=milliseconds;

				//BranchActivity activity = presenter.getActivity();

				TrackImage previousTrackMedia = _gameBase.GetPreviousTrackImage();
				String currentBackground = history.GetCurrentBackground();

				switch (currentTrackMedia.Id)
				{
					case PREVIOUS_BACKGROUND:
						if (previousTrackMedia != null && !previousTrackMedia.Id.Equals(PREVIOUS_BACKGROUND))
						{
							currentTrackMedia.AltId = previousTrackMedia.Id;
						}
						else
						{
							currentTrackMedia.AltId = currentBackground;
						}
						break;
				}

				currentTrackMedia.Progress=milliseconds;

				if (!history.ContainsInImageHistory(currentTrackMedia.Id))
				{
					// я так понимаю что тут я должен отметить что видел єту картинку
					//presenter.dispatch(@Event.IMAGE_VIEWED, currentTrackMedia.Id);
					// need to save progre and history
					_imageViewedInBranchListener.OnReceive(currentTrackMedia.Id);

				}

				try
				{
					if (currentTrackMedia.IsLocked || currentTrackMedia.IsCompleted) return;

					currentTrackMedia.IsLocked = true;

					CustomLogger.Log(" BackgroundImageService execute " + currentTrackMedia + " " + scenario.Progress + "/" + scenario.Duration);
					// show picture
					//AbstractTransition currentTransitionIm = TransitionFactory.getCurrentStrategy();
					//if (currentTransitionIm != null)
					//{
					//	currentTransitionIm.beforeBuild(currentTrackMedia);
					//}

				//	AbstractTransition transition = TransitionFactory.create(currentTrackMedia, activity);

					//transition.beforeBuild(currentTrackMedia);

				//	transition.build(currentTrackMedia);

					_gameBase.SetPreviousTrackImage(currentTrackMedia);

				}
				catch (Exception e)
				{
					CustomLogger.Log("BackgroundImageService " + e);
				}

			}
			catch (Exception e)
			{
				CustomLogger.Log("BackgroundImageService " + e);
			}
		}
	}
}
