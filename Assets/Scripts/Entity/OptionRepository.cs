using Baron.Service;
using System;

namespace Baron.Entity
{
	public class OptionRepository
	{
		public static Option Find(GameBase gameBase, String id)
		{
			if (id == null) return null;

			foreach (Option o in gameBase.OptionRegistry)
			{
				if (o.Id.Equals(id))
				{
					return o;
				}
			}

			return null;
		}
	}
}
