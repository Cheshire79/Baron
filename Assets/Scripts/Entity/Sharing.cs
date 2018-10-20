using Newtonsoft.Json;
using System;

namespace Baron.Entity
{
	[Serializable]
	public class Sharing
	{
		public static string FB = "facebook";
		public static string TW = "twitter";
		public static string INST = "instagram";
		public static string VK = "vk";

		[JsonProperty(PropertyName = "type")]
		public string Type;
		[JsonProperty(PropertyName = "url")]
		public string Url;
		[JsonProperty(PropertyName = "uri")]
		public string Uri;
	}
}
