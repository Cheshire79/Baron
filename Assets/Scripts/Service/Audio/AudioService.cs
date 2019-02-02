using Baron.Tools;
using CustomTools;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Baron.Service
{
	public class AudioService : MonoBehaviour
	{

		public enum SoundType
		{
			Play,
			Pause			
		}
		private static List<AudioClip> _sounds = new List<AudioClip>();

		public static void AddSound(string name, AudioClip audio)
		{
			if (_sounds.Find(r => r.name == name) == null)
				_sounds.Add(audio);
			else
				CustomLogger.Log(CustomLogger.LogComponents.Audio, string.Format(" Already load sound with name = {0}", name));
			CustomLogger.Log(CustomLogger.LogComponents.Audio, string.Format(" _sounds {0}", _sounds.Count));
		}

		public static void ClearSound()
		{
			_sounds.Clear();
		}

		public static void Play(string name, float time = 0f)
		{
			AudioClip audio = _sounds.Find(r => r.name == name);
			if (audio != null && audio != null)
			{
				CustomLogger.Log(CustomLogger.LogComponents.Audio, "Start playing id " + audio.name);
				PlaySoundAudioClip(audio, false, 0, 1, time);
			}
			else
				throw new ArgumentException("There is no sound as " + name);
		}

		public static void PlayCommonSound(SoundType type)
		{
			switch (type)
			{
				case SoundType.Play:
					PlaySoundAudioClip(AudioContainer.Instance.Play);
					break;
				case SoundType.Pause:
					PlaySoundAudioClip(AudioContainer.Instance.Pause);
					break;

				default:
					break;
			}
		}

		private const float NORMAL_VOLUME = 1f;
		private const float NORMAL_SPEED = 1f;
		private const float HIT_BLOCK_VOLUME = 0.2f;
		private const float HIT_BLOCK_SPEED = 0.65f;
		private const float STEP_BLOCK_VOLUME = 0.2f;
		private const float STEP_BLOCK_SPEED = 1f;
		private static readonly List<AudioSource> SoundSources = new List<AudioSource>();
		private static AudioService _instance;
		private GameObject _audioPoolContainer;

		#region Unity Methods

		private void Awake()
		{
			if (_instance != null && _instance != this)
			{
				Destroy(gameObject);
				Debug.LogWarning("Audio has been destroyed!");
				return;
			}
			_instance = this;
			_audioPoolContainer = new GameObject("AudioPoolContainer");
			_audioPoolContainer.gameObject.transform.parent = transform;
			_audioPoolContainer.transform.position = transform.position;
			SoundSources.Clear();
		}

		#endregion

		/// <summary>
		///     Is given audio clip playing for now
		/// </summary>
		/// <param name="clip"></param>
		/// <returns></returns>
		public static bool IsPlaying(AudioClip clip)
		{
			return SoundSources.Find(c => c.clip.name == clip.name && c.isPlaying) != null;
		}

		//todo
		/// <summary>
		///     Is given audio clip playing for now
		/// </summary>
		/// <param name="clip"></param>
		/// <returns></returns>
		private void PlayAudioClip(AudioClip clip, bool isLoop = false, float volume = 1f, float pitch = 1f, float startFromPosition = 0f)
		{//todo refactoring
			AudioSource existingClip = SoundSources.Find(r => r.clip.name == clip.name);
			// if audio was paused it is the same as it never exist (paused video source cuold be rewrite by another audio)

			//if (existingClip != null && existingClip.clip.name == clip.name)
			//{
			//	if (startFromPosition == 0)
			//	{
			//		CustomLogger.Log(CustomLogger.LogComponents.Audio, " started the same audio" + clip.name);
			//	//	return;
			//	}
			//	else
			//	{
			//		CustomLogger.Log(CustomLogger.LogComponents.Audio, " will moved current position in playing  audio" + clip.name);
			//		// do this further
			//	}
			//}
			bool isStartTheSame = !isLoop && startFromPosition == 0;
			bool isResetTheSame = startFromPosition > 0;
			if (existingClip == null || isStartTheSame)
			{
				GameObject go = null;
				AudioSource source = SoundSources.Find(r => !r.isPlaying); // will create the next same sound only if previos is playing
				if (source == null)
				{
					go = new GameObject("OneShotAudio");
					go.transform.parent = _audioPoolContainer.transform;
					go.transform.position = _audioPoolContainer.transform.position;
					source = go.AddComponent<AudioSource>();
				}
			
				source.volume = volume;
				source.loop = isLoop;
				source.pitch = pitch;
				source.clip = clip;
				if (startFromPosition < clip.length)
				{
					source.time = startFromPosition;
				}
				else
				{
					CustomLogger.Log(CustomLogger.LogComponents.Audio, " Cannot play " + clip.name + " from position" + source.time);
				}
				source.rolloffMode = AudioRolloffMode.Custom;
				source.maxDistance = 999999999;
				source.Play();
				
				SoundSources.Add(source);

			}
			else
			{
				if (!existingClip.isPlaying)
					existingClip.Play();
				if (isResetTheSame)
				{
					if (startFromPosition < existingClip.clip.length)
					{
						existingClip.Pause();
						existingClip.time = startFromPosition;
						existingClip.Play();
						CustomLogger.Log(CustomLogger.LogComponents.Audio, "  Play " + clip.name + " from position" + existingClip.time);
					}
					else
					{
						CustomLogger.Log(CustomLogger.LogComponents.Audio, " Cannot play " + clip.name + " from position" + existingClip.time);
						startFromPosition = 0;
					}
				}
			}
		}

		public static void StopAudioClip(string name) 
		{
			AudioSource instance = SoundSources.Find(r => r.clip.name == name && r.isPlaying);
			if (instance == null) return;
			instance.Stop();
		}

		public static void SetPitch(AudioClip clip, float pitch)
		{
			AudioSource source = SoundSources.Find(r => r.clip.name == clip.name);
			if (source != null)
				source.pitch = pitch;
		}

		#region Music

		//public static float MusicVolume
		//{
		//	get { return _instance.audio.volume; }
		//	set
		//	{
		//		if (Mathf.Approximately(_instance.audio.volume, 0) && value > 0)
		//			_instance.audio.Play();
		//		_instance.audio.volume = Mathf.Clamp01(value);
		//	}
		//}

		//public static void PlayMusic(float fadeTime = 1f)
		//{
		//	_instance.audio.Play();
		//	_instance.audio.volume = 0;
		//	HOTween.To(_instance.audio, fadeTime,
		//		new TweenParms().Prop("volume", 1.0f).UpdateType(UpdateType.TimeScaleIndependentUpdate));
		//}

		//public static void PlayMusicAudioClip(AudioClip clip)
		//{
		//	AudioSource audio = _instance.audio;
		//	if (audio.clip == null || audio.clip.name != clip.name)
		//	{
		//		audio.clip = clip;
		//		audio.loop = true;
		//	}
		//	//  if (GameParameters.MusicEnabled && !audio.isPlaying)
		//	//      PlayMusic();
		//}

		//public static void StopMusic(float fadeTime = 1f)
		//{
		//	HOTween.To(_instance.audio, fadeTime,
		//		new TweenParms().Prop("volume", 0)
		//			.UpdateType(UpdateType.TimeScaleIndependentUpdate)
		//			.OnComplete(() => _instance.audio.Stop()));
		//}

		#endregion

		#region Sound

		public static void PauseSounds()
		{
			foreach (AudioSource item in SoundSources)
			{
				if (item.isPlaying)
					item.Pause();
			}
		}

		public static float PlaySoundAudioClip(AudioClip clip, bool isLoop = false, float delay = 0, float pitch = 1f, float startFromPosition = 0f)
		{
			return PlaySoundAudioClip(clip, 1.0f, isLoop, delay, pitch, startFromPosition);
		}

		public static float PlaySoundAudioClip(AudioClip clip, float volume, bool isLoop = false, float delay = 0,	float pitch = 1f, float startFromPosition = 0f)
		{
			//if (!Preferences.SoundEnabled || clip == null) //todo settings
			//	return 0;
			if (delay > 0)
				_instance.DelayedStart(() => _instance.PlayAudioClip(clip, isLoop, volume, pitch, startFromPosition), delay);
			else
				_instance.PlayAudioClip(clip, isLoop, volume, pitch, startFromPosition);
			return clip.length;
		}

		public static void StopSounds()
		{
			foreach (AudioSource item in SoundSources)
				if (item.isPlaying)
					item.Stop();
		}

		//private static AudioClip GetRandomSound(List<AudioClip> sounds)
		//{
		//	if (!sounds.Any()) return null;

		//	int randomIndex = Random.Range(0, sounds.Count - 1);
		//	return sounds[randomIndex];
		//}

		#endregion
	}
}

