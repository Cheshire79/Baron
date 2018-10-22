using Baron.Entity;
using Baron.History;
using System;
using System.Collections.Generic;

namespace Baron.Service
{
	public class AchievementPointService//todo
	{
		public static string IMAGE = "image";
		public static string ALL_IMAGES = "all_images";
		public static string AUDIO = "audio";
		public static string ALL_AUDIO = "all_audio";
		public static string ALL_WINNER = "all_winner";
		public static string OPTION = "option";
		public static string INTERACTION = "interaction";

		public static int TOTAL_AP_RU = 1417;
		public static int TOTAL_AP_UK = 1253;
		public static int TOTAL_AP = TOTAL_AP_RU;// BuildConfig.locale.equals("uk") ? TOTAL_AP_UK : TOTAL_AP_RU;

		public const string option_a25frak = "a25frak";
		public const string option_a32eb_1 = "a32eb_1";
		public const string option_a37ccontcl = "a37ccontcl";
		public const string option_a37ccontska = "a37ccontska";
		public const string option_a37ccontskb = "a37ccontskb";
		public const string option_a37ccontskc = "a37ccontskc";
		public const string option_a37ccontskd = "a37ccontskd";
		public const string option_a37dcl = "a37dcl";
		public const string option_a37dsk = "a37dsk";

		public const string option_ru_a3b_more_20_5 = "a3b_more_20_5";
		public const string option_ru_a1a_bonusb = "a1a_bonusb";

		private GameBase _gameBase;

		public AchievementPointService(GameBase gameBase)
		{
			_gameBase = gameBase;
		}

		public void OnImageOpened(Image image)
		{
			History.History history = _gameBase.History;
			if (history == null) return;

			if (image.IsBonus)
			{
				history.AddAchievement(new AchievementEntry(IMAGE, 5, image.Id));
			}
			else
			{
				history.AddAchievement(new AchievementEntry(IMAGE, 1, image.Id));
			}
		}

		public void OnAudioOpened(Audio audio)
		{
			History.History history = _gameBase.History;
			if (history == null) return;

			history.AddAchievement(new AchievementEntry(AUDIO, 2, audio.Id));
		}

		public void OnInteractionCompleted(String name)
		{
			History.History history = _gameBase.History;
			if (history == null) return;

			switch (name)
			{
				case InteractionFactory.INTERACTION_MAP:
					history.AddAchievement(new AchievementEntry(INTERACTION, 0, name));
					break;
				case InteractionFactory.INTERACTION_HELM:
					history.AddAchievement(new AchievementEntry(INTERACTION, 10, name));
					break;
				case InteractionFactory.INTERACTION_RIDDLE:
				case InteractionFactory.INTERACTION_MUSIC_FILE:
					history.AddAchievement(new AchievementEntry(INTERACTION, 30, name));
					break;
				case InteractionFactory.INTERACTION_PROTRACTOR:
				case InteractionFactory.INTERACTION_BEACON:
				case InteractionFactory.INTERACTION_COMPAS:
				case InteractionFactory.INTERACTION_ABRUPT_EXIT:
				case InteractionFactory.INTERACTION_SPARK:
				case InteractionFactory.INTERACTION_VIDEO:
				case InteractionFactory.INTERACTION_PASSWORD:
					history.AddAchievement(new AchievementEntry(INTERACTION, 50, name));
					break;
			}
		}

		public void CheckIfOptionCompleted(String option)
		{
			HashSet<string> completed = _gameBase.History.CompletedOptions;
			bool contains;
			switch (option)
			{
				case option_a37ccontcl:

					contains = completed.Contains(AchievementPointService.option_a37ccontska)
							|| completed.Contains(AchievementPointService.option_a37ccontskb)
							|| completed.Contains(AchievementPointService.option_a37ccontskc)
							|| completed.Contains(AchievementPointService.option_a37ccontskd)
					;

					if (!contains)
					{
						OnOptionCompleted(option);
					}
					break;
				case option_a37ccontska:

					contains = completed.Contains(AchievementPointService.option_a37ccontcl)
							|| completed.Contains(AchievementPointService.option_a37ccontskb)
							|| completed.Contains(AchievementPointService.option_a37ccontskc)
							|| completed.Contains(AchievementPointService.option_a37ccontskd)
					;

					if (!contains)
					{
						OnOptionCompleted(option);
					}
					break;
				case option_a37ccontskb:

					contains = completed.Contains(AchievementPointService.option_a37ccontcl)
							|| completed.Contains(AchievementPointService.option_a37ccontska)
							|| completed.Contains(AchievementPointService.option_a37ccontskc)
							|| completed.Contains(AchievementPointService.option_a37ccontskd)
					;

					if (!contains)
					{
						OnOptionCompleted(option);
					}
					break;
				case option_a37ccontskc:

					contains = completed.Contains(AchievementPointService.option_a37ccontcl)
							|| completed.Contains(AchievementPointService.option_a37ccontska)
							|| completed.Contains(AchievementPointService.option_a37ccontskb)
							|| completed.Contains(AchievementPointService.option_a37ccontskd)
					;

					if (!contains)
					{
						OnOptionCompleted(option);
					}
					break;
				case option_a37ccontskd:

					contains = completed.Contains(AchievementPointService.option_a37ccontcl)
							|| completed.Contains(AchievementPointService.option_a37ccontska)
							|| completed.Contains(AchievementPointService.option_a37ccontskb)
							|| completed.Contains(AchievementPointService.option_a37ccontskc)
					;

					if (!contains)
					{
						OnOptionCompleted(option);
					}
					break;

				//Either a37dcl or a37dsk should be added to progress
				case option_a37dcl:
					contains = completed.Contains(AchievementPointService.option_a37dsk);

					if (!contains)
					{
						OnOptionCompleted(option);
					}

					break;
				case option_a37dsk:

					contains = completed.Contains(AchievementPointService.option_a37dcl);

					if (!contains)
					{
						OnOptionCompleted(option);
					}

					break;

				default:
					OnOptionCompleted(option);
					break;
			}
		}

		public void unlockWinnerIfCompleted()
		{

			History.History history = _gameBase.History;
			if (history == null) return;

			if (!history.IsAllWinnerOptionsOpened && _gameBase.IsAllWinnerOptionsOpened())
			{
				OnAllWinnerOptionsOpened();

				history.IsAllWinnerOptionsOpened = true;
			}
		}

		public void UnlockAllImagesIfCompleted()
		{

			History.History history = _gameBase.History;
			if (history == null) return;

			if (IsAllImagesViewed() && !history.IsAllImagesOpened)
			{

				foreach (Image bonus in ImageRepository.getCompleteBonuses(_gameBase))
				{
					if (history.AddImage(bonus))
					{
						OnImageOpened(bonus);
					}
				}

				OnAllImageOpened();

				history.IsAllImagesOpened = true;
			}

		}

		public bool unlockAllAudioIfCompleted()
		{

			History.History history = _gameBase.History;
			if (history == null) return false;

			bool isAllFailsInHistory = true;
			foreach (Audio a in _gameBase.GetAudioRegistryForGallery())
			{
				if (!history.ContainsInAudioHistory(a))
				{
					isAllFailsInHistory = false;
					break;
				}
			}

			if (isAllFailsInHistory && !history.IsAllAudioOpened)
			{

				OnAllAudioOpened();

				history.IsAllAudioOpened = true;

				return true;
			}

			return false;
		}

		private void OnAllImageOpened()
		{
			History.History history = _gameBase.History;
			if (history == null) return;

			history.AddAchievement(new AchievementEntry(ALL_IMAGES, 50));
		}

		private void OnAllAudioOpened()
		{
			History.History history = _gameBase.History;
			if (history == null) return;

			history.AddAchievement(new AchievementEntry(ALL_AUDIO, 50));
		}

		private void OnAllWinnerOptionsOpened()
		{
			History.History history = _gameBase.History;
			if (history == null) return;

			history.AddAchievement(new AchievementEntry(ALL_WINNER, 100));
		}

		private void OnOptionCompleted(String name)
		{
			History.History history = _gameBase.History;
			if (history == null) return;

			switch (name)
			{
				case BranchDecisionManager.BRANCH_A29FAA:
					history.AddAchievement(new AchievementEntry(OPTION, 50, name));
					break;
				case option_a25frak:
				case option_a32eb_1:
				//Group 1
				case option_a37ccontcl:
				case option_a37ccontska:
				case option_a37ccontskb:
				case option_a37ccontskc:
				case option_a37ccontskd:
				//Group 2
				case option_a37dcl:
				case option_a37dsk:
					history.AddAchievement(new AchievementEntry(OPTION, 30, name));
					break;
				case option_ru_a1a_bonusb:
				case option_ru_a3b_more_20_5:
					//if (BuildConfig.locale.equals("ru"))
					//todo
					{
						history.AddAchievement(new AchievementEntry(OPTION, 30, name));
					}
					break;
			}
		}

		private bool IsAllImagesViewed()
		{

			History.History history = _gameBase.History;
			if (history == null) return false;

			List<ImageEntry> viewedImages = history.Images;

			List<Image> images = ImageRepository.getGallerySimpleImages(_gameBase);
			List<Image> bonuses = ImageRepository.getSuperBonuses(_gameBase);
			List<Image> specialBonuses = ImageRepository.getSpecialBonuses(_gameBase);

			HashSet<string> viewedRegistry = new HashSet<string>();
			foreach (ImageEntry entry in viewedImages)
			{
				viewedRegistry.Add(entry.Name);
			}

			bool isAllImagesViewed = false;
			bool isAllBonusesViewed = false;
			bool isAllSpecialBonusesViewed = false;

			foreach (Image image in images)
			{
				isAllImagesViewed = viewedRegistry.Contains(image.Id);
				if (!isAllImagesViewed)
				{
					break;
				}
			}

			foreach (Image image in bonuses)
			{
				isAllBonusesViewed = viewedRegistry.Contains(image.Id);
				if (!isAllBonusesViewed)
				{
					break;
				}
			}

			foreach (Image image in specialBonuses)
			{
				isAllSpecialBonusesViewed = viewedRegistry.Contains(image.Id);
				if (!isAllSpecialBonusesViewed)
				{
					break;
				}
			}

			return isAllImagesViewed && isAllBonusesViewed && isAllSpecialBonusesViewed;
		}
	}
}
