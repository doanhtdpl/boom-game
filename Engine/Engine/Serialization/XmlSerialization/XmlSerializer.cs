using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace SSCEngine.Serialization.XmlSerialization
{
    internal class XmlSerializer : ISerializer
    {
        public XmlSerializer(XElement elem)
        {
            this.elem = elem;
        }

        private XElement elem;

        public void SerializeString(string name, object obj)
        {
            XElement subElem = new XElement(name);
            subElem.Value = obj.ToString();

            elem.Add(subElem);
        }

        public void SerializeObject(string name, ISerializable serObject)
        {
            XElement subElem = new XElement(name);
            serObject.Serialize(new XmlSerializer(subElem));

            elem.Add(subElem);
        }

        public ISerializer SubSerializer(string name)
        {
            XElement subElem = new XElement(name);
            elem.Add(subElem);

            return new XmlSerializer(subElem);
        }
    }
}
