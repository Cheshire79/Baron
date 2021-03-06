﻿
using Baron.Entity;
using Baron.Service;
using Baron.Tools;
using CustomTools;
using System;
using System.Collections.Generic;
using Uniject;
using Uniject.Impl;
using UnityEngine;
using UnityEngine.EventSystems;
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
		private ButtonTouchListener _previousButtonTouchListener;
		private bool _IsAutomaticValueChange = false;

		private event Action StartButtonClicked;
		private event Action PreviousButtonClicked;
		private event Action PauseButtonClicked;
		private event Action PlayButtonClicked;
		private event Action EndButtonClicked;



		public void Init(Action<string> optionClicked, Action<float> OnClickedAnotherPosition, Action OnStartDebuging, Action OnPauseGame, Action ResumeGameAndStartScenario, Action MoveToStartScenarioPoint, Action MoveToEndtScenarioPoint)
		{
			_optionClicked = optionClicked;
			_onClickedAnotherPosition = OnClickedAnotherPosition;
			_onStartDebuging = OnStartDebuging;
			PauseButtonClicked += OnPauseGame;
			PlayButtonClicked += ResumeGameAndStartScenario;
			StartButtonClicked += MoveToStartScenarioPoint;
			EndButtonClicked += MoveToEndtScenarioPoint;
			PreviousButtonClicked += null;
		}

		BranchChildernReference _reference;
		RectTransform _RectTransform;
		public BranchView([Resource("prefabs/view/BranchView")] TestableGameObject obj, IInstancesCache instancesCache)
			: base(obj)
		{
			_reference = ((UnityGameObject)obj).obj.GetComponent<BranchChildernReference>();
			//  EventDelegate.Add(references.PlayNowButton.onClick, OnPlayNowButtonClicked);

			_info = _reference.Info;
			_OptionsCache = instancesCache;
			_optionsList = _reference.OptionsList;
			_image = _reference.MainImage;
			_hover = _reference.Hover;
			_topOverlay = _reference.TopOverlay;
			_bottomOverlay = _reference.BottomOverlay;
			_Test = _reference.Test;
			_slider = _reference.Slider;
			_previousButtonTouchListener = _reference.MovepreviousButtonTouchListener;


			_slider.onValueChanged.AddListener(OnClickedAnotherPosition);

			_Test.EndDraging += OnEndDrugging;
			_Test.StartDraging += OnStartDrugging;
			_RectTransform = _reference.canvasScaler;

			if (_reference.MoveToEndButton != null)
				_reference.MoveToEndButton.OnClick += OnEndButtonClicked;

			if (_reference.MoveToStartButton != null)
				_reference.MoveToStartButton.OnClick += OnStartButtonClicked;


			if (_reference.MovePreviousButton != null)
				_reference.MovePreviousButton.OnClick += OnPreviosButtonClicked;

			if (_reference.PauseButton != null)
				_reference.PauseButton.OnClick += OnPauseButtonClicked;

			if (_reference.PlayButton != null)
				_reference.PlayButton.OnClick += OnPlayButtonClicked;

			//if (_reference.MovePreviousButton != null)
			//	_reference.PlayButton.OnPointerUp.AddListener(PreviousButtonUp);

			_previousButtonTouchListener.OnDown += PreviousButtonDown;
			_previousButtonTouchListener.OnUp += PreviousButtonUp;
		}

		private void PreviousButtonUp()
		{
			CustomLogger.Log(CustomLogger.LogComponents.Branch, string.Format(" PreviousButtonUp  "));
		}
		private void PreviousButtonDown()
		{
			CustomLogger.Log(CustomLogger.LogComponents.Branch, string.Format(" PreviousButtonDown  "));
		}
		private void OnPreviosButtonClicked()
		{
			AudioService.PlayCommonSound(AudioService.SoundType.Play);
			Action handler = PreviousButtonClicked;
			if (handler != null) handler();
		}
		private void OnEndButtonClicked()
		{
			AudioService.PlayCommonSound(AudioService.SoundType.Play);
			Action handler = EndButtonClicked;
			if (handler != null) handler();
		}

		private void OnStartButtonClicked()
		{
			AudioService.PlayCommonSound(AudioService.SoundType.Play);
			Action handler = StartButtonClicked;
			if (handler != null) handler();
		}

		private void OnPauseButtonClicked()
		{
			AudioService.PlayCommonSound(AudioService.SoundType.Pause);
			Action handler = PauseButtonClicked;
			if (handler != null) handler();
		}

		private void OnPlayButtonClicked()
		{
			AudioService.PlayCommonSound(AudioService.SoundType.Play);
			Action handler = PlayButtonClicked;
			if (handler != null) handler();
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


		//-------
		public void ToggleControls()
		{
			if (GameBase.isPaused)
			{
				_reference.PlayButton.gameObject.SetActive(true);
				_reference.PauseButton.gameObject.SetActive(false);
			}
			else
			{
				_reference.PlayButton.gameObject.SetActive(false);
				_reference.PauseButton.gameObject.SetActive(true);
			}
		}
		//----
	}
}
