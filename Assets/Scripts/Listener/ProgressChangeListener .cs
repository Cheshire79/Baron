using Baron.Entity;
using Baron.Service;
using CustomTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baron.Listener
{
	public class ProgressChangeListener // useless
	{
		private GameBase _gameBase;
		private TrackService _trackService;
		bool isDragging;
		public static bool isRemotelyChanged = false; // todo ??
		public ProgressChangeListener(GameBase gameBase)
		{
			_gameBase = gameBase;
		}

		public void OnStartTrackingTouch()
		{
			CustomLogger.Log("onStartTrackingTouch");
			//if (seekBar == null) return;

			isDragging = true;

			try
			{
				Scenario scenario = _gameBase.History.GetScenario();

				if (scenario.CurrentTrackBranch == null) return;

				Option option = OptionRepository.Find(_gameBase, scenario.CurrentTrackBranch.OptionId);
				if (option != null)
				{
					option.Init(scenario.CurrentTrackBranch.StartsAt);
				}

				_trackService.Pause();



				//manager.
				TStartScroll();
			}

			catch (Exception e)
			{
				//Log.e(tag, e);
				CustomLogger.Log(" ProgressChangeListener" + e.Message);
			}


		}
		public void TStartScroll()
		{
			//presenter.dispatch(new Intent(Event.APPLICATION_PAUSED));

			// call public void TPause()

			/*
						final MediaPlayer player = AudioService.getPlayer(presenter.getActivity(), "sfx_audio_scroll_start");
						player.setOnCompletionListener(new MediaPlayer.OnCompletionListener() {
							@Override

							public void onCompletion(MediaPlayer mp)
						{
							audioService.removeTrack(player);
						}
					});

						audioService.addTrack(player);
						player.start();

						audioService.removeTrack(scrollPlayer);
						scrollPlayer = AudioService.getPlayer(presenter.getActivity(), "sfx_audio_scroll_end");
						scrollPlayer.setLooping(true);

						audioService.addTrack(scrollPlayer);
						scrollPlayer.start();
						*/
		}

		public void OnProgressChanged(int progress, bool fromUser)
		{ // can pass this
		  //if (!isDragging && !isRemotelyChanged) return;
		  //	final BranchPresenter presenter = BranchPresenter.getInstance();
		  //	Scenario scenario = history.getScenario();
		  //	presenter.getScenarioManager().updateScenario(presenter.getGameBase(),
		  //			scenario, progress, scenario.duration);		

		}

		public void OnStopTrackingTouch(int progress)
		{
			CustomLogger.Log(" onStopTrackingTouch " + progress);
			isDragging = false;
			Scenario scenario = _gameBase.History.GetScenario();
			//presenter.getScenarioManager().updateScenario(presenter.getGameBase(),
			//		scenario, progress, scenario.Duration);
			TstopScroll();
		}

		public void TstopScroll()
		{

			CustomLogger.Log(" stopScroll");
			// 	BranchPresenter presenter = BranchPresenter.getInstance();
			//presenter.getAudioService().removeTrack(scrollPlayer);
			//presenter.setPlayerFragmentPlayButtonEnabled(true);
			//presenter.dispatch(new Intent(Event.APPLICATION_RESUMED));
		}
	}
}
