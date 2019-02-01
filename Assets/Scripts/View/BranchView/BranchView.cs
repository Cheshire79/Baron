
using Baron.Entity;
using Baron.Service;
using Baron.Tools;
using CustomTools;
using System;
using System.Collections.Generic;
using Uniject;
using Uniject.Impl;
using UnityEngine;
using UnityEngine.UI;

namespace Baron.View.BranchView
{
	public class BranchView : TestableView, IBranchView
	{
		private int _screenHeight;
		private void Awake()
		{
			CustomLogger.Log(CustomLogger.LogComponents.Test, string.Format("Screen.width = {0} Screen.height = {1}", Screen.width, Screen.height));
			_screenHeight = Screen.height;
		}
		private readonly IInstancesCache _OptionsCache;
		private Text _info;
		private GridLayoutGroup _optionsList;
		private List<OptionItem> _optionItems = new List<OptionItem>();
		private Action<string> _optionClicked;
		private Action<float> _onClickedAnotherPosition;
		private Action _onStartDebuging;
		private UnityEngine.UI.Image _image;
		private UnityEngine.UI.Image _hover;
		private Slider _slider;

		private Transform _topOverlay;
		private Transform _bottomOverlay;
		private Test _Test;
		private bool _IsAutomaticValueChange = false;
		public void Init(Action<string> optionClicked, Action<float> OnClickedAnotherPosition, Action OnStartDebuging)
		{
			_optionClicked = optionClicked;
			_onClickedAnotherPosition = OnClickedAnotherPosition;
			_onStartDebuging = OnStartDebuging; ;
		}
		BranchChildernReference refer;
		RectTransform _RectTransform;
		public BranchView([Resource("prefabs/view/BranchView")] TestableGameObject obj, IInstancesCache instancesCache)
			: base(obj)
		{
			var references = ((UnityGameObject)obj).obj.GetComponent<BranchChildernReference>();
			//  EventDelegate.Add(references.PlayNowButton.onClick, OnPlayNowButtonClicked);
			//if (references.StartButtonGUI != null)
			//	references.StartButtonGUI.onClick.AddListener(OnPlayNowButtonClicked);
			refer = references;
			_info = references.Info;
			_OptionsCache = instancesCache;
			_optionsList = references.OptionsList;
			_image = references.MainImage;
			_hover = references.Hover;
			_topOverlay = references.TopOverlay;
			_bottomOverlay = references.BottomOverlay;
			_Test = references.Test;
			_slider = references.Slider;



			_slider.onValueChanged.AddListener(OnClickedAnotherPosition);

			_Test.EndDraging += OnEndDrugging;
			_Test.StartDraging += OnStartDrugging;
			_RectTransform = references.canvasScaler;
		}

		private void OnClickedAnotherPosition(float value)
		{
			if (!_Test.IsBeingDragged() && !_IsAutomaticValueChange)
			{
				Reset();
				if (_onClickedAnotherPosition != null)
					_onClickedAnotherPosition(value);

				CustomLogger.Log("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=" + _Test.IsBeingDragged()
					+ " " + value);
			}
		}
		private void OnEndDrugging()
		{
			CustomLogger.Log("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= value ="
				+ " " + _slider.value);

			if (_onClickedAnotherPosition != null)
				_onClickedAnotherPosition(_slider.value);
		}
		private void OnStartDrugging()
		{
			Reset();
			if (_onStartDebuging != null)
				_onStartDebuging();
			CustomLogger.Log("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= value ="
				+ " " + _slider.value);
		}
		public void UpdateDisplayedData(string text)
		{
			_info.text = _info.text + "\n" + text + " ";
		}

		ResourceRequest resourceRequest;
		public void SetImage(string image)
		{
			Resources.UnloadUnusedAssets();

			string path = "drawable/" + image;
			resourceRequest = Resources.LoadAsync<Sprite>(path);
			resourceRequest.completed += onImageLoadCompleted;
		}

		private void onImageLoadCompleted(AsyncOperation obj)
		{
			obj.completed -= onImageLoadCompleted;
			ResourceRequest request = obj as ResourceRequest;
			if (request == null)
				throw new Exception("Cannot load image file ");
			_image.sprite = request.asset as Sprite;
			resourceRequest = null;


			//_image.sprite = Resources.LoadAsync<Sprite>(path);
			_image.SetNativeSize();// todo
			int imageHeight = (int)(_image.sprite.rect.height);

			CustomLogger.Log(CustomLogger.LogComponents.Messages, string.Format(" Screen.width = {0}", Screen.width));
			CustomLogger.Log(CustomLogger.LogComponents.Messages, string.Format(" Screen.height = {0}", Screen.height));
			CustomLogger.Log(CustomLogger.LogComponents.Messages, string.Format(" imageHeight = {0}, {1}, {2}", imageHeight, (imageHeight * _RectTransform.localScale.y), Screen.height));

			_screenHeight = Screen.width;
			if ((imageHeight * _RectTransform.localScale.y) < Screen.height)
			{
				CustomLogger.Log(CustomLogger.LogComponents.Messages, string.Format(" ==================== imageHeight = {0}, {1}, {2}", imageHeight, (imageHeight * _RectTransform.localScale.y), Screen.height));

				int top = //imageHeight 
					+imageHeight / 2//+170/2
					;
				//_screenHeight = Screen.height;
				_topOverlay.localPosition =
					//position = 
					new Vector3(_topOverlay.localPosition.x,
					top
					// _topOverlay.position.y
					, _topOverlay.localPosition.z);


				int bottom = -(imageHeight) / 2;
				_bottomOverlay.localPosition = new Vector3(_bottomOverlay.localPosition.x,
					 bottom
					// _topOverlay.position.y
					, _bottomOverlay.localPosition.z);
				_topOverlay.gameObject.SetActive(true);
				_bottomOverlay.gameObject.SetActive(true);
			}
			else
			{
				CustomLogger.Log(CustomLogger.LogComponents.Warnings, string.Format(" Hide border ="));
				CustomLogger.Log(CustomLogger.LogComponents.Exceptions, string.Format(" Hide border ="));
				_topOverlay.gameObject.SetActive(false);
				_bottomOverlay.gameObject.SetActive(false);
			}
			_hover.gameObject.SetActive(false);// after forst time loading mager need yo hide hover
		}
		public void PlaceOptions(GameBase gameBase) // need to move at controler((
		{
			Branch currentBranch = gameBase.FindCurrentBranch(false);

			CustomLogger.Log("BranchViewController onCreate for " + currentBranch);

			InventoryBranch currentInventoryBranch = TreeParser.FindInventoryBranch(gameBase, currentBranch);

			if (currentInventoryBranch != null)
			{
				List<Branch> _branches;
				_branches = currentInventoryBranch.Branches;
				// 	adapter = new BranchesAdapter(activity, currentInventoryBranch, this);

				for (int i = 0; i < _branches.Count; i++)
				{

					Branch branch = _branches[i];
					Option option = OptionRepository.Find(gameBase, branch.OptionId);
					CustomLogger.Log("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
					CustomLogger.Log("BranchViewController Cid=" + branch.Cid + " Text=" + option.Text);
					var newOption = _OptionsCache.Get<OptionItem>();
					newOption.Init(branch.Cid, option.Text, _optionClicked);
					_optionItems.Add(newOption);
					newOption.transform.parent = _optionsList.transform;
					newOption.transform.localScale = new Vector3(1, 1, 1);
					newOption.Show();

				}
				//	updatePosition();
			}
		}

		public void InitSlider(int max)
		{ }
		public void ChangeSliderPosition(int pos, int max)
		{
			_IsAutomaticValueChange = true;
			_slider.maxValue = max;
			_slider.value = pos;
			_IsAutomaticValueChange = false;


		}
		public void Reset()
		{
			foreach (var item in _optionItems)
			{
				_optionsList.transform.DetachChildren();
				item.Hide();
				//friend.StateChange -= OnStateChange;
				//friend.InviteToggle.value = false;
				item.Reset();
				_OptionsCache.Put(item);
			}
			_optionItems.Clear();
			_info.text = _info.text + "\n" + "-------------------------------------------------------------" + "\n";
		}

	}
}
