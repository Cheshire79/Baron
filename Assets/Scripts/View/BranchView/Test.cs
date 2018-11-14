using CustomTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Baron.View.BranchView
{
	public class Test : MonoBehaviour,// IPointerDownHandler, IPointerClickHandler,
	//IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler,
	IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		private bool isBeingDragged = false;
		public bool IsBeingDragged()
		{
			return isBeingDragged;
		}
		public void OnDrag(PointerEventData eventData)
		{
			//CustomLogger.Log("Dragging");
			//Debug.Log("Dragging");
		}

		public void SliderEvent(Slider sl)
		{
			
			CustomLogger.Log("ProgressBarManager Exc" + sl.value);
		}
		public void OnEndDrag(PointerEventData data)
		{
		//	Debug.Log("drag end");
			CustomLogger.Log("drag end" + data.position);
			isBeingDragged = false;
		//	if (EndDrag != null)
		//	{ EndDrag(data); }
		}

		public void OnBeginDrag(PointerEventData data)
		{
			CustomLogger.Log("drag star" + data.position);
			//Debug.Log("drag start");

			isBeingDragged = true;
		}
		//public void OnPointerClick(PointerEventData eventData)
		//{
		//	Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
		//}

		//public void OnPointerDown(PointerEventData eventData)
		//{
		//	Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
		//}

		//public void OnPointerEnter(PointerEventData eventData)
		//{
		//	Debug.Log("Mouse Enter");
		//}
		//public void OnPointerExit(PointerEventData eventData)
		//{
		//	Debug.Log("Mouse Exit");
		//}

		//public void OnPointerUp(PointerEventData eventData)
		//{
		//	Debug.Log("Mouse Up");
		//}
	}

}
