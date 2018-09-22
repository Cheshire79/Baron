using System;
using Newtonsoft.Json;

namespace Baron.Entity
{
	public class Item : Entity
	{
		[JsonIgnore]
		public static string VIGVAM = "Vigvam";
		[JsonIgnore]
		public static string BUILDING = "Building";
		[JsonIgnore]
		public static string BLACK_MARK = "Frak";
		[JsonIgnore]
		public static string A = "ma";
		[JsonIgnore]
		public static string B = "mb";
		[JsonIgnore]
		public static string C = "mc";
		[JsonIgnore]
		public static string D = "md";
		[JsonIgnore]
		public static string E2 = "me2";
		[JsonIgnore]
		public static string F = "mf";
		[JsonIgnore]
		public static string G = "mg";
		[JsonIgnore]
		public static string H = "mh";
		[JsonIgnore]
		public static string I = "mi";
		[JsonIgnore]
		public static string K = "mk";
		[JsonIgnore]
		public static string L = "ml";
		[JsonIgnore]
		public static string Y = "my";
		[JsonIgnore]
		public static string SKIN = "Skin";

		[JsonIgnore]
		private string _text;

		public String Text
		{
			get { return _text; }
		}

		public bool IsGlobal
		{
			get	{ return _isGlobal;	}
			set	{	_isGlobal = value;}
		}

		[JsonProperty(PropertyName = "isGlobal")]
		private bool _isGlobal;

		public Item():base()
		{
			IsGlobal = false;
		}

	}
}
