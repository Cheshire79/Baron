using UnityEngine;

namespace Uniject.Impl
{
    public class UnityAudioSource : IAudioSource
    {
        private readonly AudioSource source;

        public UnityAudioSource(GameObject obj)
        {
            source = obj.GetComponent<AudioSource>();
            if (source == null)
            {
                source = (AudioSource) obj.AddComponent(typeof (AudioSource));
            }
            source.rolloffMode = AudioRolloffMode.Linear;
        }

        public void loopSound(AudioClip clip)
        {
            source.loop = true;
            source.clip = clip;
            source.Play();
        }

        public void playOneShot(AudioClip clip)
        {
            source.PlayOneShot(clip);
        }

        public void Play()
        {
            source.Play();
        }

        public bool isPlaying
        {
            get { return source.isPlaying; }
        }
    }
}