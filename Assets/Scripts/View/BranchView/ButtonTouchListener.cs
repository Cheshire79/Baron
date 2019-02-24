using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Baron.View.BranchView
{
	public class ButtonTouchListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
	{
		public event Action OnUp;
		public event Action OnDown;
		public event Action OnClick;
		public void OnPointerDown(PointerEventData eventData)
		{
			if (OnDown != null)
				OnDown();
		}
		public void OnPointerUp(PointerEventData eventData)
		{
			if (OnUp != null)
				OnUp();
		}
		public void OnPointerClick(PointerEventData eventData)
		{
			if (OnClick != null)
				OnClick();
		}
	}
}
