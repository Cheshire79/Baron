using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using UnityEngine;
//using UnityEngine.Serialization;
using Newtonsoft.Json;

namespace Assets.Scripts.Entity.Test
{
    [Serializable]
    public class ImageJS_Test
    {
        public string Id;
        public int Duration;
    }
    [Serializable]
    public class AudioJS_Test
    {
		//[JsonProperty(PropertyName = "FooBar")]
		//[SerializeField]
		[JsonIgnore]
		public string Id
		{
			get{ return _id; }
			set { _id = value; }
		}
		
		[JsonProperty(PropertyName = "id")]
		//[SerializeField]
		private string _id;
	//	[JsonProperty(PropertyName = "Dura")]
		public int Duration;
    }

    [Serializable]
    public class OptionJS_Test
    {
        public string id;
        //[NonSerialized]
        public string text;
        public List<ImageJS_Test> images;
        public List<AudioJS_Test> audio;
    }
}
