using System;
using System.Text;

namespace Baron.Service
{
	public class StringUtils
	{
		private static string SALTCHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

		public static String Cid()
		{
			StringBuilder salt = new StringBuilder();
			Random rnd = new Random();
			while (salt.Length < 5)
			{
				int index = (int)(rnd.NextDouble() * SALTCHARS.Length);
				salt.Append(SALTCHARS[index]);
			}
			return salt.ToString();
		}
	}
}
