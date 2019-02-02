using Baron.Entity;
using Baron.Listener;
using Baron.Tools;
using CustomTools;
using System;
using System.Collections;
using UnityEngine;


namespace Baron.Service
{
	public class BackgroundAudioService : BackgroundMediaService
	{
		ResourceRequest resourceRequest;
		
		public event Action<string> OnShowMessage;
		private AudioPlayedInBranchListener _audioPlayedInBranchListener;
		private string _currentImage = "";
		public BackgroundAudioService(GameBase gameBase) : base(gameBase)
		{
			_audioPlayedInBranchListener = new AudioPlayedInBranchListener(gameBase);
		}

		public void Release()
		{

		}

		public override void Pause()
		{	
			try
			{
				Option option = _gameBase.FindCurrentOption(false);
				if (option == null) return;

				foreach (TrackAudio trackAudio in option.TrackAudio)
				{
					trackAudio.IsLocked = false;
					AudioService.StopAudioClip(trackAudio.Id);
				}
			}
			catch (Exception e)
			{
				CustomLogger.LogException(e);
			}
		}


		public override void Execute(Scenario scenario)
		{
		//	CustomLogger.Log(CustomLogger.LogComponents.Audio, "------------ audio----------- BackgroundImageService =Execute ");
			TrackBranch trackBranch = scenario.CurrentTrackBranch;
			int milliseconds = scenario.Progress;
			int trackDuration = scenario.Duration;

			if (trackBranch == null || milliseconds >= trackDuration)
			{
				return;
			}

			History.History history = _gameBase.History;
			

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

				CustomLogger.Log("------------ audio----------- BackgroundImageService =" + currentTrackAudio.Id + ", ");

				if (OnShowMessage != null)
				{
					MainThreadRunner.AddTask(() => OnShowMessage("audio " + currentTrackAudio.Id));
				}
			}
			pauseScenario(scenario, currentTrackAudio);// ??

			if (currentTrackAudio.IsLocked || currentTrackAudio.IsCompleted)
			{
				//NOTE let audio keep playing
				return;
			}

			int delay = currentTrackAudio.Progress - currentTrackAudio.StartsAt;

			if (currentTrackAudio.IsPrepared && !currentTrackAudio.IsLocked	)
			{

				currentTrackAudio.IsLocked = true;

				//CustomLogger.Log(CustomLogger.LogComponents.Audio,"BackgroundAudioService execute " + currentTrackAudio
				//		+ " progress=" + scenario.Progress + "/" + scenario.Duration
				//		+ " delay=" + delay);

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
						MainThreadRunner.AddTask(() => AudioService.Play(currentTrackAudio.Id, delay/1000.0f));
					}
					else
					{
						MainThreadRunner.AddTask(() => AudioService.Play(currentTrackAudio.Id));
					}
				}
			}
		}

		public void PreloadScenarioAudio(Scenario scenario//, Task onFirstPrepared
			)
		{
			bool hasAudioInScenario = false;
			foreach (TrackBranch track in scenario.Branches)
			{
				Option option = OptionRepository.Find(_gameBase, track.OptionId);
				if (option == null) continue;

				foreach (TrackAudio trackAudio in option.Audio)
				{

					hasAudioInScenario = true;
					//				final boolean isFirst = option.audio.indexOf(trackAudio) == 0 && scenario.branches.indexOf(track) == 0;

					if (!trackAudio.IsPrepared && !trackAudio.IsPreparing)
					{
						trackAudio.IsPreparing = true;

						Audio audio = AudioRepository.Find(_gameBase, trackAudio.Id);
						

						CustomLogger.Log(CustomLogger.LogComponents.Audio, string.Format(" AudioClip = {0}", audio.File));

						string path = "Audio/" + audio.File;
						resourceRequest = Resources.LoadAsync<AudioClip>(path);	
						resourceRequest.completed += ((AsyncOperation obj) => {

						//	obj.completed -= OnAudioLoadCompleted;// todo ask
							ResourceRequest request = obj as ResourceRequest;
							if (request == null)
								throw new Exception("Cannot load audio file ");
							AudioClip clip = request.asset as AudioClip;

							if (clip != null)
							{
								AudioService.AddSound(request.asset.name, clip);
							}
							else
								throw new Exception("Cannot load audio file " + request.asset.name);
							resourceRequest = null;
							CustomLogger.Log(CustomLogger.LogComponents.Audio, string.Format("loaded  AudioClip = {0}", clip.name));
							trackAudio.IsPrepared = true;
							trackAudio.IsPreparing = false;

						});
						
					}

				}

			}

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

		#region useless
		public void LoadAudio(string name)// useless
		{	
			string path = "Audio/" + name;
			resourceRequest = Resources.LoadAsync<AudioClip>(path);
			resourceRequest.completed += OnAudioLoadCompleted;
		//	resourceRequest.completed += ((AsyncOperation obj) => { });
		}

		private  void OnAudioLoadCompleted(AsyncOperation obj)// useless
		{
			obj.completed -= OnAudioLoadCompleted;
			ResourceRequest request = obj as ResourceRequest;
			if (request == null)
				throw new Exception("Cannot load audio file ");
			AudioClip clip = request.asset as AudioClip;

			if (clip != null)
			{
				AudioService.AddSound(request.asset.name, clip);
			}
			else
				throw new Exception("Cannot load audio file " + request.asset.name);
			resourceRequest = null;
			CustomLogger.Log(CustomLogger.LogComponents.Audio, string.Format("loaded  AudioClip = {0}", clip.name));
		}

		IEnumerator loadFromResourcesFolder()// useless
		{
			//Request data to be loaded
			ResourceRequest loadAsync = Resources.LoadAsync("shipPrefab", typeof(GameObject));

			//Wait till we are done loading
			while (!loadAsync.isDone)
			{
				Debug.Log("Load Progress: " + loadAsync.progress);
				yield return null;
			}

			//Get the loaded data
			GameObject prefab = loadAsync.asset as GameObject;
		}
		#endregion
	}
}
