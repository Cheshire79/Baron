using Baron.Entity;
using Baron.Service;
using CustomTools;
using System;

namespace Baron.Listener
{


	public class AudioPlayedInBranchListener
	{
		private GameBase _gameBase;
		AchievementPointService _aps;

		public AudioPlayedInBranchListener(GameBase gameBase)
		{
			_gameBase = gameBase;
			_aps = new AchievementPointService(_gameBase);
		}


		public void OnReceivestring(string id)
		{
			CustomLogger.Log("AudioPlayedInBranchListener OnReceivestring =============================" + id);

			try
			{
				History.History history = _gameBase.History;
				if (history == null) return;

				//if (context.isStopCalled()) return;

				//final AbstractPresenter presenter = context.getPresenter();
				//if (presenter == null) return;

				//final GameBase gameBase = presenter.getGameBase();
				//if (gameBase == null) return;

				Audio audio = AudioRepository.Find(_gameBase, id);
				if (audio == null) return;


				if (history.AddAudio(id))
				{

					_aps.OnAudioOpened(audio);

					AddFailsToHistoryFor(id);

					if (_aps.UnlockAllAudioIfCompleted())
					{
						AddFailsToHistoryFor("closed");
					}

					//presenter.syncHistory();
				}
			}
			catch (Exception e)
			{
				CustomLogger.Log("IAudioPlayedInBranchListener " + e);
			}
		}
		private void AddFailsToHistoryFor(string baseName)
		{

			History.History history = _gameBase.History;
			if (history == null) return;

			try
			{
				switch (baseName)
				{
					case "closed":
					case "opened":
						break;
					default:

						Audio audio = AudioRepository.Find(_gameBase, baseName);
						foreach (string fail in audio.Fails)
						{
							try
							{
								Audio failAudio = AudioRepository.Find(_gameBase, fail);
								if (history.AddAudio(failAudio.Id))
								{
									_aps.OnAudioOpened(failAudio);
								}

							}
							catch (Exception e)
							{
								CustomLogger.Log("IAudioPlayedInBranchListener " + e);
							}
						}
						break;
				}

				for (int count = 1; count <= 10; count++) //todo?
				{
					String failName = baseName + "_fail_" + count;

					Audio fail = AudioRepository.Find(_gameBase, failName);
					if (fail == null) continue;

					if (history.AddAudio(failName))
					{
						_aps.OnAudioOpened(fail);
					}
				}
			}
			catch (Exception e)
			{
				CustomLogger.Log("IAudioPlayedInBranchListener " + e);
			}
		}

	}
}
