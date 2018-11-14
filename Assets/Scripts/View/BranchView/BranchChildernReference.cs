using CustomTools;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Baron.View.BranchView
{

	
	public class BranchChildernReference : MonoBehaviour//, IEndDragHandler, IBeginDragHandler
	{
		public GridLayoutGroup OptionsList;
		public Text Info;
		public Image MainImage;

		public Transform TopOverlay;
		public Transform BottomOverlay;

		public Slider Slider;
		public EventTrigger EventTrigger;
		public Test Test;

		public Action<PointerEventData> EndDrag;
		//private bool isBeingDragged = false;
		//public bool IsBeingDragged()
		//{
		//	return isBeingDragged;
		//}

		//public void OnEndDrag(PointerEventData data)
		//{
		//	CustomLogger.Log("drag end ref" + data.position);
		//	Debug.Log("drag end");
		//	isBeingDragged = false;
		//	if (EndDrag != null)
		//	{ EndDrag(data); }
		//}

		//public void OnBeginDrag(PointerEventData data)
		//{
		//	CustomLogger.Log("drag start ref" + data.position);
		//	Debug.Log("drag start");

		//	isBeingDragged = true;
		//}
	}
}
