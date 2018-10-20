using Assets.Scripts.Service;
using Baron.Entity;
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

		private Tree _tree;
		private History.History _history;

		private List<Option> _optionRegistry;
		//private List<Image> imageRegistry;
		//private List<Audio> audioRegistry;
		private List<Item> _itemRegistry;
		//private List<Riddle> riddleRegistry;
		//private List<BeaconItem> beaconItemRegistry;
		private TrackImage _previousTrackImage;
		private TrackAudio currentTrackAudio;

		public GameBase()
		{
			//history = new History();
			_optionRegistry = new List<Option>();
			//imageRegistry = new List<>();
			//audioRegistry = new List<>();
			_itemRegistry = new List<Item>();
			//riddleRegistry = new List<>();
			//beaconItemRegistry = new List<>();
		}

		public List<Option> OptionRegistry
		{
			get { return _optionRegistry; }
			set { _optionRegistry = value; }
		}

		

		//public List<Image> getImageRegistry()
		//{
		//	return imageRegistry;
		//}

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

		//public List<Audio> getAudioRegistryForGallery()
		//{
		//	ArrayList<Audio> filtered = new ArrayList<>(getAudioRegistry().size());
		//	for (Audio i : getAudioRegistry())
		//	{
		//		if (i.isFail())
		//		{
		//			filtered.add(i);
		//		}
		//	}
		//	return filtered;
		//}

		//public List<Audio> getAudioRegistry()
		//{
		//	return audioRegistry;
		//}

		//public void setAudioRegistry(List<Audio> audioRegistry)
		//{
		//	this.audioRegistry = audioRegistry;
		//}

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
			get	{return _history;}
			set	{_history = value;}
		}



		//public History getHistory()
		//{
		//	return history;
		//}

		//public void setHistory(History history)
		//{
		//	this.history = history;
		//}

		//public List<Riddle> getRiddleRegistry()
		//{
		//	return riddleRegistry;
		//}

		//public void setRiddleRegistry(List<Riddle> riddleRegistry)
		//{
		//	this.riddleRegistry = riddleRegistry;
		//}

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

		public void reset()
		{
			CustomLogger.Log(" GAme Base reset");
			
			try
			{
				_previousTrackImage = null;
				currentTrackAudio = null;
				isPaused = false;
				isFinaleReached = false;
				isTrackCompleted = false;
				isBranchesAnimating = false;
				//PlayerFragment.isActionsVisible = false;
				//PlayerFragment.isProgressVisible = false;
			}
			catch (Exception e)
			{
				//Log.e(tag, e);
				CustomLogger.Log(" "+e.Message);
			}
		}

		public TrackImage getPreviousTrackImage()
		{
			return _previousTrackImage;
		}

		public void setPreviousTrackImage(TrackImage trackImage)
		{
			if (trackImage != null && BackgroundImageService.PREVIOUS_BACKGROUND.Equals(trackImage.Id))
				return;

			_previousTrackImage = trackImage;
		}

		//public TrackAudio getCurrentTrackAudio()
		//{
		//	return currentTrackAudio;
		//}

		//public void setCurrentTrackAudio(TrackAudio currentTrackAudio)
		//{
		//	this.currentTrackAudio = currentTrackAudio;
		//}

		//public boolean isAllWinnerOptionsOpened()
		//{
		//	if (history == null) return false;

		//	HashSet<String> options = new HashSet<>(5);
		//	options.add(AchievementPointService.option_a25frak);
		//	options.add(AchievementPointService.option_a32eb_1);
		//	options.add(AchievementPointService.option_a37ccontcl);
		//	options.add(AchievementPointService.option_a37ccontska);
		//	options.add(AchievementPointService.option_a37ccontskb);
		//	options.add(AchievementPointService.option_a37ccontskc);
		//	options.add(AchievementPointService.option_a37ccontskd);
		//	options.add(AchievementPointService.option_a37dcl);
		//	options.add(AchievementPointService.option_a37dsk);

		//	for (String option : options)
		//	{
		//		boolean contains = false;

		//		for (Entry entry : history.getSteps())
		//		{
		//			Branch branch = TreeParser.findBranchByCid(this, entry.getName());
		//			if (branch != null)
		//			{
		//				if (branch.getOption().equals(option))
		//				{
		//					contains = true;
		//					break;
		//				}

		//			}
		//		}

		//		if (!contains) return false;
		//	}

		//	for (InteractionEntry entry : history.getCompletedInteractions())
		//	{
		//		switch (entry.getType())
		//		{
		//			case InteractionFactory.INTERACTION_BEACON:
		//				return entry.isCompleted();
		//		}
		//	}

		//	return false;
		//}
	}
}
