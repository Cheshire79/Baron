using UnityEngine;

namespace Baron.Service
{
	class AudioContainer : MonoBehaviour
	{
		public AudioClip Play;
		public AudioClip Pause;

		public static AudioContainer Instance { get; private set; }

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
		}
	}
}
