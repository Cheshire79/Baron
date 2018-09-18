using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public string Id;
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
