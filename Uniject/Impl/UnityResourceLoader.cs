using System;
using System.Xml.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Uniject.Impl
{
    public class UnityResourceLoader : IResourceLoader
    {
        public AudioClip loadClip(string path)
        {
            var result = (AudioClip) Resources.Load(path);
            if (null == result)
            {
                throw new NullReferenceException();
            }

            return result;
        }

        public Material loadMaterial(string path)
        {
            return (Material) Resources.Load(path);
        }

        public XDocument loadDoc(string path)
        {
            var textAsset = (TextAsset) Resources.Load(path);
            return XDocument.Parse(textAsset.text);
        }

        public TestableGameObject instantiate(string path)
        {
            var obj = (GameObject) Object.Instantiate(Resources.Load(path));
            return new UnityGameObject(obj);
        }

        public T loadResource<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public System.IO.TextReader openTextFile(string path)
        {
            return new System.IO.StringReader(((TextAsset)Resources.Load(path, typeof(TextAsset))).text);
        }
    }
}