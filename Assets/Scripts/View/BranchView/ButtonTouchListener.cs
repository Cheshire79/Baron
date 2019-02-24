using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Baron.View.BranchView
{
	public class ButtonTouchListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		public Action ActionUp;
		public Action ActionDown;
		public void OnPointerDown(PointerEventData eventData)
		{
			if (ActionDown != null)
				ActionDown();
		}
		public void OnPointerUp(PointerEventData eventData)
		{
			if (ActionUp != null)
				ActionUp();
		}
	}
}
