using Baron.Entity;
using Baron.History;
using CustomTools;
using System;
using System.Collections.Generic;


namespace Baron.Service
{
	public class GameBase
	{
		public static string INITIAL_BRANCH = "BEGIN_1";
		//  private static string tag = GameBase.class.getSimpleName();

		public static bool isPaused = false;
		public static bool isMute = false;
		public static bool isBranchesAnimating = false;
		public static bool isTrackCompleted = false;
		public static bool isLoaded = false;
		public static bool isFinaleReached = false;
		private IHistoryManager _historyManager;
		public void Init( IHistoryManager historyManager)
		{
			_historyManager = historyManager;
			_history = historyManager.FetchHistory();
			SetInitialBranch();

		}

		private Tree _tree;
		private History.History _history;

		private List<Option> _optionRegistry;
		private List<Image> _imageRegistry;
		private List<Audio> _audioRegistry;
		private List<Item> _itemRegistry;
		private List<Riddle> _riddleRegistry;
		//private List<BeaconItem> beaconItemRegistry;
//		private TrackImage _previousTrackImage;
	//	private TrackAudio currentTrackAudio;

		public GameBase()
		{
			//history = new History();
			_optionRegistry = new List<Option>();
			_imageRegistry = new List<Image>();
			_audioRegistry = new List<Audio>();
			_itemRegistry = new List<Item>();
			_riddleRegistry = new List<Riddle>();
			//beaconItemRegistry = new List<>();
		}

		public List<Audio> AudioRegistry
		{
			get { return _audioRegistry; }
			set { _audioRegistry = value; }
		}

		public List<Option> OptionRegistry
		{
			get { return _optionRegistry; }
			set { _optionRegistry = value; }
		}

		public List<Image> ImageRegistry
		{
			get { return _imageRegistry; }
			set { _imageRegistry = value; }
		}


		//public void setImageRegistry(List<Image> imageRegistry)
		//{
		//	this.imageRegistry = imageRegistry;
		//}

		//public List<Image> getImageRegistryForGallery()
		//{
		//	ArrayList<Image> filtered = new ArrayList<>(getImageRegistry().size());
		//	for (Image i : getImageRegistry())
		//	{
		//		if (!i.isHidden())
		//		{
		//			filtered.add(i);
		//		}
		//	}
		//	return filtered;
		//}

		//public List<Image> getOpenedImageRegistryForGallery()
		//{
		//	List<Image> source = getImageRegistryForGallery();
		//	List<Image> filtered = new ArrayList<>(source.size());
		//	for (Image i : source)
		//	{
		//		if (history.containsInImageHistory(i))
		//		{
		//			filtered.add(i);
		//		}
		//	}
		//	return filtered;
		//}

		public List<Audio> GetAudioRegistryForGallery()
		{
			List<Audio> filtered = new List<Audio>(AudioRegistry.Count);
			foreach (Audio i in AudioRegistry)
			{
				if (i.IsFail)
				{
					filtered.Add(i);
				}
			}
			return filtered;
		}

		public List<Item> ItemRegistry
		{
			get { return _itemRegistry; }
			set { _itemRegistry = value; }
		}


		//public HashSet<String> getInventory()
		//{
		//	HashSet<String> registry = new HashSet<>(itemRegistry.size());
		//	for (Item item : itemRegistry)
		//	{
		//		registry.add(item.getId());
		//	}
		//	return registry;
		//}

		public Tree Tree
		{
			get { return _tree; }
			set { _tree = value; }
		}

		public History.History History
		{
			get { return _history; }
		//	set { _history = value; }
		}

		public List<Riddle> RiddleRegistry
		{
			get { return _riddleRegistry; }
			set { value = _riddleRegistry; }
		}

		//public boolean hasHistory()
		//{
		//	return history != null
		//			&& history.getSteps().size() > 1;
		//}

		//public List<BeaconItem> getBeaconItemRegistry()
		//{
		//	return beaconItemRegistry;
		//}

		//public void setBeaconItemRegistry(List<BeaconItem> beaconItemRegistry)
		//{
		//	this.beaconItemRegistry = beaconItemRegistry;
		//}

		public void Reset()
		{
			CustomLogger.Log(" GAme Base reset");

			try
			{
				//_previousTrackImage = null;
				//currentTrackAudio = null;
				isPaused = false;
				isFinaleReached = false;
				isTrackCompleted = false;
				isBranchesAnimating = false;
				//PlayerFragment.isActionsVisible = false;
				//PlayerFragment.isProgressVisible = false;
			}
			catch (Exception e)
			{
				CustomLogger.LogException(e);
			}
		}

		//public TrackImage GetPreviousTrackImage()
		//{
		//	return _previousTrackImage;
		//}

		//public void SetPreviousTrackImage(TrackImage trackImage)
		//{
		//	if (trackImage != null && BackgroundImageService.PREVIOUS_BACKGROUND.Equals(trackImage.Id))
		//		return;

		//	_previousTrackImage = trackImage;
		//}

		//public TrackAudio getCurrentTrackAudio()
		//{
		//	return currentTrackAudio;
		//}

		//public void setCurrentTrackAudio(TrackAudio currentTrackAudio)
		//{
		//	this.currentTrackAudio = currentTrackAudio;
		//}

		public bool IsAllWinnerOptionsOpened()
		{
			if (_history == null) return false;

			HashSet<string> options = new HashSet<string>();
			options.Add(AchievementPointService.option_a25frak);
			options.Add(AchievementPointService.option_a32eb_1);
			options.Add(AchievementPointService.option_a37ccontcl);
			options.Add(AchievementPointService.option_a37ccontska);
			options.Add(AchievementPointService.option_a37ccontskb);
			options.Add(AchievementPointService.option_a37ccontskc);
			options.Add(AchievementPointService.option_a37ccontskd);
			options.Add(AchievementPointService.option_a37dcl);
			options.Add(AchievementPointService.option_a37dsk);


			foreach (string option in options)
			{
				bool contains = false;

				foreach (Entry entry in _history.GetSteps())
				{
					Branch branch = TreeParser.FindBranchByCid(this, entry.Name);
					if (branch != null)
					{
						if (branch.OptionId.Equals(option))
						{
							contains = true;
							break;
						}

					}
				}

				if (!contains) return false;
			}

			foreach (InteractionEntry entry in _history.CompletedInteractions)
			{
				switch (entry.Type)
				{
					case InteractionFactory.INTERACTION_BEACON:
						return entry.IsCompleted;
				}
			}

			return false;
		}

		private void SetInitialBranch()// old name 
		{

			Tree tree = Tree;
			if (tree == null) return;
			if (_history == null)
				throw new ArgumentNullException("GameBase:  history is null");
			Branch initial = TreeParser.FindBranchByOption(this, GameBase.INITIAL_BRANCH, true, null);//checked 10_09_18
			if (initial == null) return;
			_history.InitialBranch = initial;

		}

		public void Сlear()
		{
			CustomLogger.Log("History clear");
			_history.ActiveSave.Reset();

			_history.ResetBranches(Tree);

			SetInitialBranch();

			_history.ActiveSave.Scenario.Cid = null;
		}

		public void syncHistory()
		{
			_historyManager.Sync(_history);
			
		}





		//-------------------- Navigate on Branch and Option

		public Branch FindCurrentBranch(bool enabledOnly)
		{
			
			TrackBranch trackBranch = _history.GetScenario().CurrentTrackBranch;
			if (trackBranch == null)
			{
				return _history.InitialBranch;
			}

			string id = trackBranch.Id;

			if (enabledOnly)
			{
				//HashSet<String> disabledBranches = history.getDisabledBranchCID();
				//if (disabledBranches.Contains(id))
				//{
				//	CustomLogger.Log(" BrunchController Cid " + id + " is disabled");
				//	return null;
				//}
			}

			return TreeParser.FindBranchByCid(this, id);
		}

		public Option FindCurrentOption(bool enabledOnly)
		{
			try
			{
				History.History history = _history;
				if (history == null) return null;

				Branch currentBranch = FindCurrentBranch(enabledOnly);
				if (currentBranch == null) return null;

				Option currentOption = OptionRepository.Find(this, currentBranch.OptionId);
				if (currentOption == null) return null;

				if (enabledOnly)
				{
					if (history.GetDisabledOptions().Contains(currentOption.Id))
					{
						CustomLogger.Log("BrunchController Option " + currentOption.Id + " is disabled");
						return null;
					}
				}
				return currentOption;
			}
			catch (Exception e)
			{
				CustomLogger.LogException(e);
			}
			return null;
		}
		public Branch GetStartBranch()
		{

			History.History history = _history;
			if (history == null) return null;


			Branch defaultBranch = history.InitialBranch;
			Branch currentBranch;
			try
			{
				currentBranch = FindCurrentBranch(false);
				if (currentBranch == null)
				{
					currentBranch = defaultBranch;
				}

			}
			catch (Exception e)
			{
				CustomLogger.LogException(e);
				currentBranch = defaultBranch;
			}

			return currentBranch;
		}
	}
}
