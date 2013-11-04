using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SSCEngine.Serialization.XmlSerialization
{
    public class XmlSerialization : ISerialization
    {
        private XmlSerialization()
        {
        }

        public static XmlSerialization Instance { get; private set; }

        static XmlSerialization()
        {
            Instance = new XmlSerialization();
        }

        private Dictionary<Stream, XDocument> documents = new Dictionary<Stream,XDocument>();

        public ISerializer Serialize(System.IO.Stream s)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-16", "yes"), new XElement("Root"));
            this.documents.Add(s, doc);

            return new XmlSerializer(doc.Root);
        }

        public IDeserializer Deserialize(System.IO.Stream s)
        {
            XDocument doc = XDocument.Load(s);
            Debug.WriteLine(doc.ToString());

            return new XmlDeserializer(doc.Root);
        }

        public void CompleteSerialization(Stream s)
        {
            if (this.documents.ContainsKey(s))
            {
                XDocument doc = this.documents[s];
                doc.Save(s);

                this.documents.Remove(s);
            }
        }
    }
}
