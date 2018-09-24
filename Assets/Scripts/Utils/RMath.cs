using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baron.Utils
{
	public  class RMath
	{
		private static Random _random=new Random((int) DateTime.Now.Ticks);

		
		public static T Random<T>(T[] items) where T : class
		{
			if (items.Length == 0) return null;
			float randomValue = _random.Next(0, 10000 )/10000.0f;
			int randomIndex = (int)(randomValue * items.Length);
			return items[randomIndex];
		}
	}
}
