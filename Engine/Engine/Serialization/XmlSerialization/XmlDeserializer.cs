using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SSCEngine.Serialization.XmlSerialization
{
    internal class XmlDeserializer : IDeserializer
    {
        public XmlDeserializer(XElement elem)
        {
            this.elem = elem;
        }

        private XElement elem;

        public void Deserialize(string serName, ISerializable ser)
        {
            XElement subElem = this.elem.Element(serName);

            ser.Deserialize(new XmlDeserializer(subElem));
        }

        public string DeserializeString(string serName)
        {
            return this.elem.Element(serName).Value;
        }

        public int DeserializeInteger(string serName)
        {
            return int.Parse(this.elem.Element(serName).Value);
        }

        public double DeserializeDouble(string serName)
        {
            return double.Parse(this.elem.Element(serName).Value);
        }

        public ICollection<IDeserializer> DeserializeAll(string serName)
        {
            ICollection<IDeserializer> deserializers = new List<IDeserializer>();

            IEnumerable<XElement> elements = this.elem.Elements();

            foreach (var subElem in elements)
            {
                deserializers.Add(new XmlDeserializer(subElem));
            }

            return deserializers;
        }

        public IDeserializer SubDeserializer(string serName)
        {
            return new XmlDeserializer(this.elem.Element(serName));
        }
    }
}
