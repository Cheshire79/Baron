﻿using Baron.Entity;
using Baron.Listener;
using Baron.Tools;
using CustomTools;
using System;


namespace Baron.Service
{
	public class BackgroundImageService : BackgroundMediaService
	{
		private string _currentImage = "";
		private ImageViewedInBranchListener _imageViewedInBranchListener;
		public event Action<string> OnChangeImage;
		public event Action<string> OnShowMessage;
		public BackgroundImageService(GameBase gameBase	) : base(gameBase)
		{
			_imageViewedInBranchListener = new ImageViewedInBranchListener(gameBase);
		}
		public const string PREVIOUS_BACKGROUND = "unknown";

		public override void Execute(Scenario scenario)//+
		{
			try
			{
				TrackBranch trackBranch = scenario.CurrentTrackBranch;
				if (trackBranch == null) return;

				//	GameBase gameBase = presenter.getGameBase();
				if (_gameBase == null) return;

				History.History history = _gameBase.History;
				if (history == null) return;

				Option option = OptionRepository.Find(_gameBase, trackBranch.OptionId);

				if (option == null) return;

				//	int milliseconds = scenario.Progress;

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


				TrackImage previousTrackMedia = (TrackImage)currentTrackMedia.Previous;
				string currentBackground = history.GetCurrentBackground();


				if (!_currentImage.Equals(currentTrackMedia.Id))
				{
					_currentImage = currentTrackMedia.Id;

					CustomLogger.Log("----------image------------ BackgroundImageService =" + currentTrackMedia.Id + ", " + currentTrackMedia.AltId);
				
					if (OnChangeImage != null)
					{
						MainThreadRunner.AddTask(() => OnChangeImage(currentTrackMedia.Id));
					}
					if (OnShowMessage != null)
					{
						MainThreadRunner.AddTask(() => OnShowMessage("image " + currentTrackMedia.Id));
					}
					//public event Action<string> OnChangeImage;// (string image)
					//public event Action<string> OnShowMessage;// (string image)

					history.SetCurrentBackground(currentTrackMedia.Id);// todo ask
					_gameBase.syncHistory();//todo
				}

				//currentTrackMedia.Progress = milliseconds;

				////BranchActivity activity = presenter.getActivity();

				//TrackImage previousTrackMedia = _gameBase.GetPreviousTrackImage();
				//string currentBackground = history.GetCurrentBackground();

				//switch (currentTrackMedia.Id)
				//{
				//	case PREVIOUS_BACKGROUND:
				//		if (previousTrackMedia != null && !previousTrackMedia.Id.Equals(PREVIOUS_BACKGROUND))
				//		{
				//			currentTrackMedia.AltId = previousTrackMedia.Id;
				//		}
				//		else
				//		{
				//			currentTrackMedia.AltId = currentBackground;
				//		}
				//		break;
				//}

				if (PREVIOUS_BACKGROUND.Equals(currentTrackMedia.Id))
				{
					if (previousTrackMedia != null && !PREVIOUS_BACKGROUND.Equals(previousTrackMedia.Id))
					{
						currentTrackMedia.AltId = previousTrackMedia.Id;

						history.ActiveSave.LastUnknownBackground = previousTrackMedia.Id;

					}
					else
					{
						currentTrackMedia.AltId = currentBackground;
					}
				}
				else
				{
					currentTrackMedia.AltId = null;
				}

				///currentTrackMedia.Progress = milliseconds; todo
				// do not save allready visit media

				if (currentTrackMedia.IsLocked) return;

				currentTrackMedia.IsLocked = true;

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

					CustomLogger.Log(" BackgroundImageService execute " + currentTrackMedia.AltId + "    " + scenario.Progress + "/" + scenario.Duration);
					// show picture
					//AbstractTransition currentTransitionIm = TransitionFactory.getCurrentStrategy();
					//if (currentTransitionIm != null)
					//{ 
					//	currentTransitionIm.beforeBuild(currentTrackMedia);
					//}

					//	AbstractTransition transition = TransitionFactory.create(currentTrackMedia, activity);

					//transition.beforeBuild(currentTrackMedia);

					//	transition.build(currentTrackMedia);

					//_gameBase.SetPreviousTrackImage(currentTrackMedia); // what the fack

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
