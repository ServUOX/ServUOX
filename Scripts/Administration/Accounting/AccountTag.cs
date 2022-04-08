using System.Xml;

namespace Server.Accounting
{
    public class AccountTag
    {
        public AccountTag(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public AccountTag(XmlElement node)
        {
            Name = Utility.GetAttribute(node, "name", "empty");
            Value = Utility.GetText(node, "");
        }

        public string Name { get; set; }
        public string Value { get; set; }

        public void Save(XmlTextWriter xml)
        {
            xml.WriteStartElement("tag");
            xml.WriteAttributeString("name", Name);
            xml.WriteString(Value);
            xml.WriteEndElement();
        }
    }
}
