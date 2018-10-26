using Baron.Service;
using System.Collections.Generic;

namespace Baron.Entity
{
	public class ImageRepository
	{

		public static Image Find(GameBase gameBase, string id)
		{
			if (id == null) return null;

			foreach (Image image in gameBase.ImageRegistry)
			{
				if (id.Equals(image.Id))
				{
					return image;
				}
			}
			return null;
		}

		public static List<Image> getNotCompleteBonusesForGallery(GameBase gameBase)
		{
			List<Image> bonuses = new List<Image>();

			foreach (Image image in gameBase.ImageRegistry)
			{
				if (!image.IsCompleteBonus && !image.IsHidden)
				{
					bonuses.Add(image);
				}
			}
			return bonuses;
		}

		public static List<Image> getCompleteBonuses(GameBase gameBase)
		{
			List<Image> bonuses = new List<Image>();

			foreach (Image image in gameBase.ImageRegistry)
			{
				if (image.IsCompleteBonus)
				{
					bonuses.Add(image);
				}
			}
			return bonuses;
		}

		public static List<Image> getSpecialBonuses(GameBase gameBase)
		{
			List<Image> bonuses = new List<Image>();
			foreach (Image image in gameBase.ImageRegistry)
			{
				if (image.IsSpecialBonus)
				{
					bonuses.Add(image);
				}
			}
			return bonuses;
		}

		public static List<Image> getSuperBonuses(GameBase gameBase)
		{
			List<Image> bonuses = new List<Image>();

			foreach (Image image in gameBase.ImageRegistry)
			{
				if (image.IsSuperBonus)
				{
					bonuses.Add(image);
				}
			}
			return bonuses;
		}

		public static List<Image> getGallerySimpleImages(GameBase gameBase)
		{
			List<Image> images = new List<Image>();
			foreach (Image image in gameBase.ImageRegistry)
			{
				if (image.IsBonus || image.IsHidden) continue;

				images.Add(image);
			}
			return images;
		}

		public static List<Image> getGalleryBonuses(GameBase gameBase)
		{
			List<Image> images = new List<Image>();
			foreach (Image image in gameBase.ImageRegistry)
			{
				if (!image.IsHidden)
				{
					if (image.IsBonus)
					{
						images.Add(image);
					}
				}
			}
			return images;
		}
	}
}
