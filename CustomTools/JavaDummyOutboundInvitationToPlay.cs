using System.Xml.Serialization;

namespace Poker.Network.Actions
{
    public class JavaDummyOutboundInvitationToPlay<T>
    {
        [XmlElement("OutboundInvitationToPlay")]
        public T Value { get; set; }

        [XmlAttribute("class")]
        public string Class { get; set; }
    }
}