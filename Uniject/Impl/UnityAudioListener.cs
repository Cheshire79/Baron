using UnityEngine;

namespace Uniject.Impl
{
    public class UnityAudioListener : TestableComponent, IAudioListener
    {
        public UnityAudioListener(TestableGameObject parent, GameObject obj) : base(parent)
        {
            var listener = obj.GetComponent<AudioListener>();
            if (null == listener)
            {
                obj.AddComponent<AudioListener>();
            }
        }

        public void noOp()
        {
        }
    }
}