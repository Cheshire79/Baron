using System;
using System.Collections;
using UnityEngine;

namespace Baron.Tools
{
	public static class MonoBehaviourExtensions
	{
		/// <summary>
		/// Executes lambda with starting delay
		/// </summary>
		/// <param name="obj"> </param>
		/// <param name="action">Action to perform</param>
		/// <param name="delay">Starting delay</param>
		public static void DelayedStart(this MonoBehaviour obj, Action action, float delay)
		{
			obj.StartCoroutine(DelayedStartCoroutine(action, delay));
		}

		/// <summary>
		/// Executes lambda with starting delay
		/// </summary>
		/// <param name="obj"> </param>
		/// <param name="action">Action to perform</param>
		/// <param name="delay">Starting delay</param>
		public static void DelayedStart(this MonoBehaviour obj, Action action, YieldInstruction delay)
		{
			obj.StartCoroutine(DelayedStartCoroutine(action, delay));
		}

		static IEnumerator DelayedStartCoroutine(Action action, float delay)
		{
			yield return new WaitForSeconds(delay * Time.timeScale);
			action();
		}

		static IEnumerator DelayedStartCoroutine(Action action, YieldInstruction delay)
		{
			yield return delay;
			action();
		}

	}

}
