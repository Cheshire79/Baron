using System.Collections.Generic;
using System.Xml.Serialization;

namespace Poker.Tools
{
    public class JavaDummy<T>
    {
        [XmlAttribute("value")]
        public T Value { get; set; }

        [XmlAttribute("class")]
        public string Class { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T>.Default.GetHashCode(Value)*397) ^ (Class != null ? Class.GetHashCode() : 0);
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (JavaDummy<T>)) return false;
            var other = obj as JavaDummy<T>;
            if (other == null) return false;
            return EqualityComparer<T>.Default.Equals(Value, other.Value) && string.Equals(Class, other.Class);
        }
    }
}