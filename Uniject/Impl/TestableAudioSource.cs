using UnityEngine;

namespace Uniject.Impl
{
    public class TestableAudioSource
    {
        private readonly IMaths maths;

        public TestableAudioSource(IAudioSource audio, IMaths maths)
        {
            this.audio = audio;
            this.maths = maths;
        }

        public IAudioSource audio { get; private set; }

        public void playOneShot(AudioClip clip)
        {
            audio.playOneShot(clip);
        }

        public void loopSound(AudioClip clip)
        {
            audio.loopSound(clip);
        }

        public void playRandom(AudioClip[] clips)
        {
            audio.playOneShot(clips[maths.randZeroToNMinusOne(clips.Length)]);
        }
    }
}