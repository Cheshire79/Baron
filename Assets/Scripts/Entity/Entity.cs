using System;
using Newtonsoft.Json;

namespace Baron.Entity
{
    [Serializable]
    public class Entity
    {
		[JsonProperty(PropertyName = "id")]
		protected string _id;

        public Entity()
        {
        }

        public Entity(string id)
        {
            _id = id;
        }

		[JsonIgnore]
		public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        // public string getId()
        // {return id;}

        //public void setId(String id)
        //{this.id = id;}


        public override string ToString()
        {
            return base.ToString() + "#" + _id;
        }
    }
}
