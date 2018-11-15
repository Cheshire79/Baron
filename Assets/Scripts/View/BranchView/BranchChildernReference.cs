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

	}
}
