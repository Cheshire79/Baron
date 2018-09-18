using UnityEngine;

namespace Uniject
{
    public interface IAudioSource
    {
        bool isPlaying { get; }
        void Play();
        void loopSound(AudioClip clip);
        void playOneShot(AudioClip clip);
    }
}