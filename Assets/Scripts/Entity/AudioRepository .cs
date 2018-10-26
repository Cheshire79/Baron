using Baron.Service;
using System;

namespace Baron.Entity
{
	public class AudioRepository
	{
		public static Audio Find(GameBase gameBase, String id)
		{
			if (id == null) return null;

			foreach (Audio image in gameBase.AudioRegistry)
			{
				if (id.Equals(image.Id))
				{
					return image;
				}
			}

			return null;
		}
	}
}
