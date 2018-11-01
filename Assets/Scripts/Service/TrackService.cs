﻿using Baron.Entity;
using CustomTools;
using System;

namespace Baron.Service
{
	public class TrackService
	{
		BackgroundImageService _imageService;
		BackgroundAudioService _audioService;

		public TrackService(BackgroundImageService imageService, BackgroundAudioService audioService)
		{
			_imageService = imageService;
			_audioService = audioService;

		}
		public void pause()
		{
			CustomLogger.Log(" TrackService pause");

			//if (!BranchPresenter.isCreated()) return;

			//BranchPresenter presenter = BranchPresenter.getInstance();

			try
			{
				_imageService.Pause();
			}
			catch (Exception e)
			{
				CustomLogger.Log("TrackService " + e);
			}

			try
			{
				_audioService.Pause();
			}
			catch (Exception e)
			{
				CustomLogger.Log("TrackService " + e);
			}

		}

		public void Resume(Scenario scenario)
		{

			//if (!BranchPresenter.isCreated()) return;

			//BranchPresenter presenter = BranchPresenter.getInstance();


			if (GameBase.isPaused)
			{
				pause();
				return;
			}

			try
			{
				_imageService.Resume(scenario);
			}
			catch (Exception e)
			{
				CustomLogger.Log("TrackService " + e);
			}


			try
			{
				_audioService.Resume(scenario);
			}
			catch (Exception e)
			{
				CustomLogger.Log("TrackService " + e);
			}

		}

		public void dispose()
		{
			//Log.i(tag, "dispose");

			//if (!BranchPresenter.isCreated()) return;

			//try
			//{
			//	BranchPresenter presenter = BranchPresenter.getInstance();

			//	presenter.getBackgroundImageService().destroy();
			//	presenter.getBackgroundAudioService().destroy();
			//	presenter.getInteractionService().destroy();
			//}
			//catch (Throwable e)
			//{
			//	Log.e(tag, e);
			//}
		}

		public void PreloadScenarioAudio(Scenario scenario)
		{
			_audioService.PreloadScenarioAudio(scenario);
		}
	}
}