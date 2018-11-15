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
	public class Test : MonoBehaviour,	IBeginDragHandler,  IEndDragHandler
	{
		private bool isBeingDragged = false;

		public Action StartDraging;
		public Action EndDraging;
		public bool IsBeingDragged()
		{
			return isBeingDragged;
		}

		public void OnEndDrag(PointerEventData data)
		{
			isBeingDragged = false;		
			CustomLogger.Log("drag end" + data.position);
			isBeingDragged = false;
			if (EndDraging != null)
				EndDraging();

		}

		public void OnBeginDrag(PointerEventData data)
		{
			CustomLogger.Log("drag star" + data.position);
			isBeingDragged = true;
			if (StartDraging != null)
				StartDraging();
		}

	}

}
