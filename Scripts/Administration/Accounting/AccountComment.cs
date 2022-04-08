using System;
using System.Xml;

namespace Server.Accounting
{
    public class AccountComment
    {
        private string m_Content;

        public AccountComment(string addedBy, string content)
        {
            AddedBy = addedBy;
            m_Content = content;
            LastModified = DateTime.UtcNow;
        }

        public AccountComment(XmlElement node)
        {
            AddedBy = Utility.GetAttribute(node, "addedBy", "empty");
            LastModified = Utility.GetXMLDateTime(Utility.GetAttribute(node, "lastModified"), DateTime.UtcNow);
            m_Content = Utility.GetText(node, "");
        }

        public string AddedBy { get; }

        public string Content
        {
            get => m_Content;
            set
            {
                m_Content = value;
                LastModified = DateTime.UtcNow;
            }
        }

        public DateTime LastModified { get; private set; }

        public void Save(XmlTextWriter xml)
        {
            xml.WriteStartElement("comment");
            xml.WriteAttributeString("addedBy", AddedBy);
            xml.WriteAttributeString("lastModified", XmlConvert.ToString(LastModified, XmlDateTimeSerializationMode.Utc));
            xml.WriteString(m_Content);
            xml.WriteEndElement();
        }
    }
}
