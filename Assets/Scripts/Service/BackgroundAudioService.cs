using Baron.Controller;
using Baron.Entity;
using Baron.Listener;
using Baron.Tools;
using CustomTools;

namespace Baron.Service
{
	public class BackgroundAudioService : BackgroundMediaService
	{
		
		private AudioPlayedInBranchListener _audioPlayedInBranchListener;
		private string _currentImage = "";
		public BackgroundAudioService(GameBase gameBase, IBranchViewController branchViewController) : base(gameBase,  branchViewController)
		{
			_audioPlayedInBranchListener = new AudioPlayedInBranchListener(gameBase);
		}

		public void Destroy()
		{

		}

		public void Pause()
		{

			//BranchPresenter presenter = BranchPresenter.getInstance();

			//try
			//{

			//	Option option = _brunchController.FindCurrentOption(false);
			//	if (option == null) return;

			//	foreach (TrackAudio trackAudio in option.TrackAudio)
			//	{

			//		trackAudio.IsLocked = false;

			//		//	if (trackAudio.Player != null && trackAudio.player.isPlaying())
			//		//	{
			//		//		trackAudio.player.seekTo(0);
			//		//		trackAudio.player.pause();
			//		//	}
			//	}

			//}
			//catch (Exception e)
			//{
			//	CustomLogger.Log("BackgroundAudioService " + e);
			//}
		}


		public override void Execute(Scenario scenario)
		{

			//	if (!canSchedule()) return;

			TrackBranch trackBranch = scenario.CurrentTrackBranch;
			int milliseconds = scenario.Progress;
			int trackDuration = scenario.Duration;

			if (trackBranch == null || milliseconds >= trackDuration)
			{
				return;
			}

			//BranchPresenter presenter = BranchPresenter.getInstance();
			//	GameBase gameBase = presenter.getGameBase();

			History.History history = _gameBase.History;
			if (history == null) return;

			Option option = OptionRepository.Find(_gameBase, trackBranch.OptionId);
			if (option == null || option.IsProxy)
			{
				return;
			}

		
			TrackAudio currentTrackAudio = option.CurrentAudio;
			if (currentTrackAudio == null) return;

			if (!_currentImage.Equals(currentTrackAudio.Id))
			{
				_currentImage = currentTrackAudio.Id;

				CustomLogger.Log("------------ audio----------- BackgroundImageService =" + currentTrackAudio.Id + ", " );
				MainThreadRunner.AddTask(() => _branchViewController.UpdateDisplayedData("audio " + currentTrackAudio.Id));

			}
			pauseScenario(scenario, currentTrackAudio);

			if (currentTrackAudio.IsLocked || currentTrackAudio.IsCompleted)
			{
				//NOTE let audio keep playing
				return;
			}

			int delay = currentTrackAudio.Progress - currentTrackAudio.StartsAt;

			if (currentTrackAudio.IsPrepared && !currentTrackAudio.IsLocked)
			{

				currentTrackAudio.IsLocked = true;

				CustomLogger.Log("BackgroundAudioService execute " + currentTrackAudio
						+ " progress=" + scenario.Progress + "/" + scenario.Duration
						+ " delay=" + delay);

				if (!history.ContainsInAudioHistory(currentTrackAudio.Id))
				{
					//presenter.dispatch(Event.AUDIO_LISTENED, currentTrackAudio.Id);
					//todo
					_audioPlayedInBranchListener.OnReceivestring(currentTrackAudio.Id);
				}

				if (delay < currentTrackAudio.Duration)
				{
					if (delay > 400)
					{
						//	currentTrackAudio.Player.seekTo(delay);
					}

					//currentTrackAudio.player.start();
				}
			}
		}

		public void PreloadScenarioAudio(Scenario scenario//, Task onFirstPrepared
			)
		{

			//BranchPresenter presenter = BranchPresenter.getInstance();
			//	 AudioService audioService = presenter.getAudioService();
			//	GameBase gameBase = presenter.getGameBase();
			//	final Handler handler = presenter.getHandler();

					bool hasAudioInScenario = false;

					foreach (TrackBranch track in scenario.Branches)
					{

						Option option = OptionRepository.Find(_gameBase, track.OptionId);
						if (option == null) continue;

						foreach ( TrackAudio trackAudio in option.Audio)
						{

							hasAudioInScenario = true;
			//				final boolean isFirst = option.audio.indexOf(trackAudio) == 0 && scenario.branches.indexOf(track) == 0;

							if (!trackAudio.IsPrepared && !trackAudio.IsPreparing)
							{
								trackAudio.IsPreparing = true;

								Audio audio = AudioRepository.Find(_gameBase, trackAudio.Id);

			//					presenter.getHandler().post(new Runnable() {
			//					@Override

			//					public void run()
			//					{
			//						trackAudio.startedLoadingAt = System.nanoTime();
			//						MediaPlayer player = AudioService.getPlayerAsync(activity, audio);

			//						player.setOnPreparedListener(new MediaPlayer.OnPreparedListener() {
			//							@Override

			//							public void onPrepared(MediaPlayer mediaPlayer)
			//						{

			//							if (isFirst)
			//							{
			//								handler.post(onFirstPrepared);
			//							}

			//							trackAudio.completedLoadingAt = System.nanoTime();
										trackAudio.IsPrepared = true;
										trackAudio.IsPreparing = false;
			//							trackAudio.player = mediaPlayer;

			//							Log.i(tag, trackAudio + " prepared in "
			//									+ TimeUnit.MILLISECONDS.convert(trackAudio.completedLoadingAt - trackAudio.startedLoadingAt, TimeUnit.NANOSECONDS) + "ms");

									//	audioService.addTrack(mediaPlayer);todo
									}
			//					});
				}
			//});
			//               } else if (isFirst) {
			//                   handler.post(onFirstPrepared);
			//               }
			          }
			//       }

			//  if (!hasAudioInScenario) {
			//       handler.post(onFirstPrepared);
			//  }
		}

		public void pauseScenario(Scenario scenario, TrackAudio currentTrackAudio)
		{

			//BranchPresenter presenter = BranchPresenter.getInstance();
			//GameBase gameBase = presenter.getGameBase();

			//for (TrackBranch track : scenario.branches)
			//{

			//	Option option = OptionRepository.find(gameBase, track.option);
			//	if (option == null) continue;

			//	for (final TrackAudio trackAudio : option.audio)
			//	{

			//		if (trackAudio.isPrepared)
			//		{
			//			if (trackAudio != currentTrackAudio)
			//			{
			//				if (trackAudio.player.isPlaying())
			//				{

			//					trackAudio.isLocked = false;

			//					trackAudio.player.seekTo(0);
			//					trackAudio.player.pause();
			//				}
			//			}
			//		}
			//	}
			//}
		}
	}
}
