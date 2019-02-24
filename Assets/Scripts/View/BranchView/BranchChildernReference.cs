using UnityEngine;
using UnityEngine.UI;
using Baron.Tools.GUI;

namespace Baron.View.BranchView
{

	public class BranchChildernReference : MonoBehaviour
	{
		public GridLayoutGroup OptionsList;
		public Text Info;
		public Image MainImage;

		public Transform TopOverlay;
		public Transform BottomOverlay;

		public Slider Slider;
	
		public Test Test;

		public RectTransform canvasScaler;

		public Image Hover;

		public ButtonTouchListener MoveToStartButton;
		public ButtonTouchListener MovePreviousButton;
		public ButtonTouchListener MovepreviousButtonTouchListener;
		public ButtonTouchListener PauseButton;
		public ButtonTouchListener PlayButton;
		public ButtonTouchListener MoveToEndButton;
	}
}
