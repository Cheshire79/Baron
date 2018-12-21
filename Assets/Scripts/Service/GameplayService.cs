using Baron.Entity;
using CustomTools;
using System;
using System.Collections.Generic;


namespace Baron.Service
{
	public class GameplayService
	{
		private GameBase _gameBase;

		public GameplayService(GameBase gameBase)
		{
			_gameBase = gameBase;
		}
		public void RestartGame()
		{
			CustomLogger.Log("GameplayService restartGame");
			CleanUpBeforeNewGame();
		}

		public void CleanUpBeforeContinueGame()
		{
			CustomLogger.Log("GameplayService cleanUpBeforeContinueGame");
			try
			{
				//	final GameBase gameBase = context.getPresenter().getGameBase();
				//	context.getPresenter().onHistoryAvailable(new Presenter.OnHistoryAvailable() {
				{

					_gameBase.History.ValidateProgress();
					_gameBase.History.DecreaseProgress();
					//option is in History
					TrackBranch trackBranch = _gameBase.History.GetScenario().CurrentTrackBranch;
					if (trackBranch != null)
					{
						Option option = OptionRepository.Find(_gameBase, trackBranch.OptionId);
						option.Init(trackBranch.StartsAt);
					}
					_gameBase.Reset();
				}
			}

			catch (Exception e)
			{
				CustomLogger.Log("GameplayService" + e);
			}

			try
			{
				_gameBase.syncHistory(); 
				
			}
			catch (Exception e)
			{
				CustomLogger.Log("GameplayService" + e);
			}
		}

		private void CleanUpBeforeNewGame()
		{
			CustomLogger.Log("GameplayService cleanUpBeforeNewGame");
			try
			{
				_gameBase.Сlear();
				_gameBase.History.Day = History.History.DEFAULT_DAY;
				_gameBase.History.SetCurrentBackground(null);

				_gameBase.History.ValidateProgress();
				_gameBase.History.DecreaseProgress();

				foreach (Riddle riddle in _gameBase.RiddleRegistry)			
					riddle.Reset();				
				//_gameBase.syncHistory(); //toCheck save

			}
			catch (Exception e)
			{
				CustomLogger.Log("GameplayService" + e);
			}
		}

		public void NewGame()
		{
			CustomLogger.Log(" GameplayService newGame");
			CleanUpBeforeNewGame();
			try
			{
				_gameBase.History.NGplus();				
				Branch initial = _gameBase.History.InitialBranch;
				if (initial != null)
				{
					_gameBase.History.AddStep(initial);
				}
				_gameBase.History.ValidateProgress();
				//_gameBase.History.DecreaseProgress();
				_gameBase.syncHistory();
			}
			catch (Exception e)
			{
				CustomLogger.Log("GameplayService" + e);
			}
		}

		public void RetryGame(BranchScenarioManager scenarioManager)//todo
		{
			CustomLogger.Log("GameplayService retryGame");

			//	try
			//	{
			//		InteractionFactory.destroy();
			//	}
			//	catch (Throwable e)
			//	{
			//		Log.e(tag, e);
			//	}

			try
			{
				//		final AbstractPresenter presenter = context.getPresenter();
				//		if (presenter == null) return;



				_gameBase.History.ValidateProgress();
				_gameBase.History.DecreaseProgress();

				string backgroundImage = _gameBase.History.ActiveSave.LastUnknownBackground;
				if (backgroundImage != null)
				{
					_gameBase.History.ActiveSave.CurrentBackground = backgroundImage;
					_gameBase.History.ActiveSave.LastUnknownBackground = null;

				}

				Stack<string> clicks = _gameBase.History.ActiveSave.ClickedBranches;
				if (clicks.Count > 1)
				{
					// two steps back, otherway we get the same defeat scenario
					clicks.Pop();

					string target = clicks.Pop();

					Scenario altScenario = scenarioManager.CreateScenario(_gameBase, target);

					_gameBase.History.SetScenario(altScenario);

					_gameBase.syncHistory();
				}
				else
				{
					NewGame();
				}


			}



			catch (Exception e)
			{
				CustomLogger.Log("GameplayService" + e);
			}
		}

		public void ResumeGameAndStartScenario()
		{
			//	if (!BranchPresenter.isCreated()) return;

			CustomLogger.Log("GameplayService resumeGameAndStartScenario");

			try
			{
				ResumeGame();


				_gameBase.History.ValidateProgress();


				_gameBase.syncHistory();
			}
			catch (Exception e)
			{
				CustomLogger.Log("GameplayService" + e);
			}
			//todo
			//        if (InteractionFactory.hasInteractionStrategy()) {
			//            Log.w(tag, "resumeGameAndStartScenario is ignored due to interaction: "
			//                    + InteractionFactory.getCurrentInteractionType());
			//            return;


			try
			{
				//            presenter.getScenarioManager().resumeScenario();
			}
			catch (Exception e)
			{
				CustomLogger.Log("GameplayService" + e);
			}
		}

		public void ResumeGame()
		{
			//	if (!BranchPresenter.isCreated()) return;

			CustomLogger.Log("GameplayService resumeGame");

			//	final BranchPresenter presenter = BranchPresenter.getInstance();

			GameBase.isPaused = false;

			//	try
			//	{
			//		presenter.getActivity().runOnUiThread(new Runnable() {
			//				@Override

			//				public void run()
			//		{
			//			try
			//			{
			//				if (presenter.isPlayerFragmentMounted())
			//				{
			//					presenter.getPlayerFragment().toggleControls();

			//					presenter.setPlayerFragmentPlayButtonEnabled(true);
			//					presenter.setPlayerFragmentPauseButtonEnabled(true);
			//				}
			//			}
			//			catch (Throwable e)
			//			{
			//				Log.e(tag, e);
			//			}
			//		}
			//	});
			//} catch (Throwable e) {
			//            Log.e(tag, e);
			//        }

		}

		public void PauseGame()
		{
			//todo
			//	if (!BranchPresenter.isCreated()) return;

			CustomLogger.Log("GameplayService pauseGame");

			GameBase.isPaused = true;

			//	final BranchPresenter presenter = BranchPresenter.getInstance();

			//	try
			//	{
			//		TransitionFactory.stop();
			//	}
			//	catch (Throwable e)
			//	{
			//		Log.e(tag, e);
			//	}

			try
			{
				//		TrackService trackService = presenter.getTrackService();
				//		if (trackService != null)
				//			trackService.pause();
			}
			catch (Exception e)
			{
				CustomLogger.Log("GameplayService" + e);
			}

			//	try
			//	{
			//		ProgressBarManager manager = presenter.getProgressBarManager();
			//		if (manager != null)
			//			manager.stop();
			//	}
			//	catch (Throwable e)
			//	{
			//		Log.e(tag, e);
			//	}

			//	try
			//	{
			//		if (presenter.isPlayerFragmentMounted())
			//		{
			//			presenter.getActivity().runOnUiThread(new Runnable() {
			//					@Override

			//					public void run()
			//			{
			//				try
			//				{
			//					presenter.getPlayerFragment().toggleControls();
			//				}
			//				catch (Throwable e)
			//				{
			//					Log.e(tag, e);
			//				}
			//			}
			//		});
			//	}

			//		} catch (Throwable e) {
			//            Log.e(tag, e);
			//        }

			//        try {
			//            presenter.onHistoryAvailable(new Presenter.OnHistoryAvailable() {
			//                @Override
			//				public void onSuccess(History history)
			//{
			//	TrackBranch trackBranch = history.getScenario().currentBranch;
			//	if (trackBranch != null)
			//	{
			//		trackBranch.isLocked = false;
			//	}

			//	Option option = presenter.findCurrentOption(false);
			//	if (option != null)
			//	{
			//		if (option.currentAudio != null)
			//		{
			//			option.currentAudio.isLocked = false;
			//		}
			//		if (option.currentImage != null)
			//		{
			//			option.currentImage.isLocked = false;
			//		}
			//	}
			//}

			//@Override
			//				public void onError()
			//{

			//}
			//            });

			//        } catch (Throwable e) {
			//            Log.e(tag, e);
			//        }
			//    }
		}
	}
}
