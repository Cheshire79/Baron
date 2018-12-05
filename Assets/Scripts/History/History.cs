using Baron.Entity;
using Baron.Entity.Chrono;
using Baron.Service;
using CustomTools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Baron.History // wait for check
{
	[Serializable]
	public class History//todo fields
	{
		public static int DEFAULT_DAY = 1;
		public static int SAVE_LIMIT = 4;


		/**
		 * Unique completed options on all iterations
		 */
		[JsonProperty(PropertyName = "completedOptions")]
		private HashSet<string> _completedOptions;

		public HashSet<string> CompletedOptions
		{
			get { return _completedOptions; }
		}
		public bool AddCompletedOption(string id)
		{
			if (_completedOptions.Contains(id)) return false;

			_completedOptions.Add(id);

			return true;
		}
		/**
		 * Unique items the player found on all interactions
		 */

		[JsonProperty(PropertyName = "globalInventory")]
		public HashSet<String> GlobalInventory; //todo what the fuck
												/**
												 * Version of the history
												 */
		[JsonProperty(PropertyName = "version")]
		private int _version;
		public int Version
		{
			get { return _version; }
			set { _version = value; }
		}
		/**
		 * Identifier of the history. If empty - history is new
		 */
		[JsonProperty(PropertyName = "cid")]
		private string _cid;
		public string Cid
		{
			get { return _cid; }
			set { _cid = value; }
		}
		/**
		 * Create date of the history
		 */

		[JsonProperty(PropertyName = "createdAt")]
		private string _createdAt;
		public string CreatedAt
		{
			get { return _createdAt; }
			set { _createdAt = value; }
		}
		/**
		 * Saves for player
		 */
		[JsonProperty(PropertyName = "saves")]
		private List<Save> _saves;

		public List<Save> Saves
		{
			get { return _saves; }
			set
			{
				_saves.Clear();
				_saves.AddRange(value);
			}
		}
		/**
		 * Opened images on all iterations
		 */
		[JsonProperty(PropertyName = "achievements")]
		private List<AchievementEntry> _achievements;
		/**
		 * Opened images on all iterations
		 */
		[JsonProperty(PropertyName = "images")]
		private List<ImageEntry> _images;

		public List<ImageEntry> Images
		{
			get { return _images; }
		}
		/**
		 * Opened audio on all iterations
		 */
		[JsonProperty(PropertyName = "audio")]
		private List<AudioEntry> _audio;
		/**
		 * Collection of all events for chrono
		 */
		[JsonProperty(PropertyName = "playerEvents")]
		private List<PlayerEvent> _playerEvents;
		public List<PlayerEvent> PlayerEvents
		{
			get { return _playerEvents; }
		}
		/**
		 * Collection of events for chrono, which where not synced with server
		 */
		[JsonProperty(PropertyName = "unsyncedPlayerEvents")]
		private List<PlayerEvent> _unsyncedPlayerEvents;

		public List<PlayerEvent> UnsyncedPlayerEvents
		{
			get { return _unsyncedPlayerEvents; }
		}
		/**
		 * Opened interactions on all iterations
		 */

		[JsonProperty(PropertyName = "completedInteractions")]
		private List<InteractionEntry> _completedInteractions;

		public List<InteractionEntry> CompletedInteractions
		{
			get { return _completedInteractions; }
		}
		/**
		 * Completed riddles on all iterations
		 */
		[JsonProperty(PropertyName = "completedRiddles")]
		private List<string> _completedRiddles;

		public List<string> CompletedRiddles
		{
			get { return _completedRiddles; }
		}

		/**
		 * Update date of the history
		 */
		[JsonProperty(PropertyName = "updatedAt")]
		public string UpdatedAt; // where we need it ?
								 /**
								  * General information about the player
								  */
		[JsonProperty(PropertyName = "player")]
		private Player _player;
		public Player Player
		{
			get { return _player; }
		}
		/**
		 * Game iteration. "New Game"
		 */
		[JsonProperty(PropertyName = "ng")]
		private int _ng;

		public int Ng
		{
			get { return _ng; }
			set
			{
				_ng = value;
			}
		}

		public void NGplus()
		{
			_ng++;
		}

		/**
		 * Is all audio opened
		 */
		[JsonProperty(PropertyName = "isAllAudioOpened")]
		public bool IsAllAudioOpened;
		/**
		 * Is all images opened
		 */
		[JsonProperty(PropertyName = "isAllWinnerOptionsOpened")]
		public bool IsAllWinnerOptionsOpened;
		/**
		 * Is all images opened
		 */
		[JsonProperty(PropertyName = "isAllImagesOpened")]
		public bool IsAllImagesOpened;
		/**
		 * Sum of achievement points on all iterations
		 */
		[JsonProperty(PropertyName = "achievementPoints")]
		public int AchievementPoints;

		[JsonProperty(PropertyName = "activeSave")]
		private Save _activeSave;
		/**
		 * Current save
		 */
		public Save ActiveSave
		{
			get { return _activeSave; }
			set { _activeSave = value; }
		}

		/**
		 * Branch the scenario starts from. Found in runtime
		 */

		[JsonIgnore]
		private Branch _initialBranch;
		//private Scenario _scenario;
		public History()
		{
			_saves = new List<Save>(SAVE_LIMIT);
			_audio = new List<AudioEntry>(10);
			_images = new List<ImageEntry>(10);
			_completedInteractions = new List<InteractionEntry>(5);
			GlobalInventory = new HashSet<string>();

			//_scenario = new Scenario();//todo


			_completedOptions = new HashSet<string>();
			_completedRiddles = new List<string>();
			_unsyncedPlayerEvents = new List<PlayerEvent>();
			_playerEvents = new List<PlayerEvent>();
			_achievements = new List<AchievementEntry>();
			_ng = 0;
			AchievementPoints = 1;

			//ActiveSave save
			_activeSave = new Save();
			_player = new Player();
		}


		public void SetScenario(Scenario scenario) //todo
		{
			_activeSave.Scenario = scenario;
		}
		public Branch InitialBranch
		{
			get { return _initialBranch; }
			set { _initialBranch = value; }
		}
		public string GetCurrentBackground()
		{
			Save save = _activeSave;
			String value = save.CurrentBackground;

			if (BackgroundImageService.PREVIOUS_BACKGROUND.Equals(value)) return null;

			return value;
		}
		public void SetCurrentBackground(string currentBackground)
		{

			if (currentBackground != null)
			{
				switch (currentBackground)
				{
					case BackgroundImageService.PREVIOUS_BACKGROUND:
					case "black":
						return;
				}
			}
			
			_activeSave.CurrentBackground = currentBackground;
		}
		public bool ContainsInImageHistory(string id)
		{
			foreach (Entry item in _images)

			{
				if (item.Name.Equals(id))
				{
					return true;
				}
			}
			return false;
		}

		public bool ContainsInImageHistory(Image image)
		{
			return ContainsInImageHistory(image.Id);
		}

		public bool AddItem(Item item)
		{

			if (item.IsGlobal)
			{
				GlobalInventory.Add(item.Id);
			}

			return AddItem(item.Id);
		}

		public bool AddItem(string id)
		{
			Save save = _activeSave;
			foreach (Entry item in save.Inventory)
			{
				if (id.Equals(item.Name))
				{
					return false;
				}
			}

			CustomLogger.Log("History addItem:" + id);
			save.Inventory.Add(new Entry(id));
			return true;
		}

		public void RemoveItem(string id)
		{
			Save save = _activeSave;
			foreach (Entry item in save.Inventory)
			{
				if (item.Name.Equals(id))
				{
					save.Inventory.Remove(item);
					return;
				}
			}
			CustomLogger.Log("History Inventory contains not:" + id);

		}

		public void AddAchievement(AchievementEntry achievement)
		{
			CustomLogger.Log("History addAchievement" + achievement);
			_achievements.Add(achievement);
			AchievementPoints += achievement.Reward;
			CustomLogger.Log("History achievementPoints " + AchievementPoints);
		}

		public bool AddImage(Image image)
		{
			return AddImage(image.Id);
		}

		public bool AddImage(string id)
		{
			foreach (ImageEntry item in _images)
			{
				if (id.Equals(item.Name))
				{
					return false;
				}
			}

			CustomLogger.Log("History addImage: " + id);

			ImageEntry entry = new ImageEntry(id);

			_images.Add(entry);

			switch (id)
			{
				case "a32e_1sk":
				case "a32e_1cl":
					AddAudio("rocket");
					break;
			}

			return true;
		}

		public bool AddAudio(string id)
		{
			foreach (Entry item in _audio)
			{
				if (id.Equals(item.Name))
				{
					return false;
				}
			}

			CustomLogger.Log("History addAudio: " + id);

			AudioEntry entry = new AudioEntry(id);

			_audio.Add(entry);

			return true;
		}

		public List<AudioEntry> Audio
		{
			get { return _audio; }
		}

		public ImageEntry GetEntry(Image image)
		{
			ImageEntry entry = null;
			foreach (ImageEntry item in _images)
			{
				if (image.Id.Equals(item.Name))
				{
					entry = item;
					break;
				}
			}

			return entry;
		}

		public AudioEntry GetEntry(Audio audio)
		{
			AudioEntry entry = null;
			foreach (AudioEntry item in _audio)
			{
				if (audio.Id.Equals(item.Name))
				{
					entry = item;
					break;
				}
			}
			return entry;
		}

		public bool ContainsInAudioHistory(Audio audio)
		{
			return ContainsInAudioHistory(audio.Id);
		}

		public bool ContainsInAudioHistory(String id)
		{
			foreach (Entry item in _audio)
			{
				if (item.Name.Equals(id))
				{
					return true;
				}
			}
			return false;
		}

		private void AddTotalPlayerEvent(PlayerEvent @event)
		{
			foreach (PlayerEvent item in _playerEvents)
			{
				if (@event.Name.Equals(item.Name) && @event.Type.Equals(item.Type))
				{
					return;
				}
			}
			_playerEvents.Add(@event);
		}

		private void AddUnsyncedPlayerEvent(PlayerEvent @event)
		{

			foreach (PlayerEvent item in _unsyncedPlayerEvents)
			{
				if (@event.Name.Equals(item.Name) && @event.Type.Equals(item.Type))
				{
					return;
				}
			}

			_unsyncedPlayerEvents.Add(@event);
		}

		public void AddPlayerEvent(string name, string type)
		{
			if (name == null || type == null) return;

			PlayerEvent @event = new PlayerEvent();
			@event.Name = name;

			@event.Type = type;

			AddUnsyncedPlayerEvent(@event);
			AddTotalPlayerEvent(@event);
		}

		public void AddCompletedInteraction(string name, string type, bool isCompleted)
		{
			InteractionEntry entry = new InteractionEntry(name);
			entry.Type = type;
			entry.IsCompleted = isCompleted;

			_completedInteractions.Add(entry);
		}

		public void AddCompletedRiddle(string id)
		{
			CustomLogger.Log("History addCompletedRiddle: " + id);
			_completedRiddles.Add(id);
		}

		public void ResetBranches(Branch root)
		{
			if (root == null) return;

			foreach (InventoryBranch inventoryBranch in root.InventoryBranches)

			{
				foreach (Branch branch in inventoryBranch.Branches)
				{
					ResetBranches(branch);
				}
			}
		}

		public List<Entry> GetInventory()
		{
			return _activeSave.Inventory;
		}

		public void SetInventory(List<Entry> items)
		{
			_activeSave.Inventory = items;
		}

		public HashSet<String> GetGlobalInventory()
		{
			HashSet<string> uniqueItems = new HashSet<string>();// _activeSave.inventory.Count);
			foreach (Entry entry in _activeSave.Inventory)
			{
				uniqueItems.Add(entry.Name);
			}
			return uniqueItems;
		}

		public int Day
		{
			get { return _activeSave.Day; }
			set { _activeSave.Day = value; }
		}
		public void incrementDay()
		{
			_activeSave.Day += 1;
			CustomLogger.Log("History Increment day: " + _activeSave.Day);
		}

		public bool ContainsInOptionActionHistory(Branch branch, String action)
		{

			foreach (OptionActionEntry entry in _activeSave.CompletedOptionActions)

			{
				if (entry.Name.Equals(action) && branch.Cid.Equals(entry.BranchCid))
				{
					return true;
				}
			}
			return false;
		}

		public HashSet<String> GetDisabledBranchCID()
		{
			return _activeSave.DisabledBranchCID;
		}

		public HashSet<String> GetDisabledOptions()
		{
			return _activeSave.DisabledOptions;
		}

		//--
		public void AddDisabledBranchCID(string cid)
		{
			CustomLogger.Log("History addDisabledBranchCID: " + cid);
			_activeSave.DisabledBranchCID.Add(cid);
		}

		public void AddDisabledOption(String option)
		{
			CustomLogger.Log("History  addDisabledOption: " + option);
			_activeSave.DisabledOptions.Add(option);
		}

		public void AddCompletedInteraction(Interaction interaction, string type, bool isCompleted)
		{
			AddCompletedInteraction(interaction.Name, type, isCompleted);
		}

		public void AddCompletedOptionAction(String cid, String action)
		{
			OptionActionEntry entry = new OptionActionEntry(action);
			entry.BranchCid = cid;


			_activeSave.CompletedOptionActions.Add(entry);
		}

		public bool AddStep(Branch branch)
		{
			Save save = _activeSave;

			String cid = branch.Cid;
			if (save.Steps.Count > 0)
			{
				Entry last = save.Steps.Peek();     //.Peek(save.steps.Count - 1);
				if (last.Name.Equals(cid))
					return false;
			}
			save.Steps.Push(new Entry(cid));

			return true;
		}

		public bool HasBranch(Branch branch)
		{
			Save save = _activeSave;
			foreach (Entry step in save.Steps)
			{
				if (step.Name.Equals(branch.Cid))
				{
					return true;
				}
			}

			return false;
		}

		public Stack<Entry> GetSteps()
		{
			return _activeSave.Steps;
		}

		public Scenario GetScenario()
		{
			return _activeSave.Scenario;
		}
		public void ValidateProgress()
		{

			int max = (int)GetScenario().Duration;
			int progress = GetScenario().Progress;
			if (max > 0 && progress > max)
			{
				GetScenario().Update(0, max);
			}
		}

		public void DecreaseProgress()
		{
			int max = (int)GetScenario().Duration;
			if (max > 0)
			{
				int progress;
				if (max > 2000)
				{
					progress = Math.Max(0, GetScenario().Progress - 2000);
				}
				else
				{
					progress = 0;
				}

				GetScenario().Update(progress, max);
			}
		}
	}

}
