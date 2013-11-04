using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml.Linq;
using System.Xml;

namespace TriUan.SCSEngine.Utils
{
    class XPoint
    {
        public List<Vector2> mListOfVector;

        public Vector2 Center
        {
            get;
            set;
        }

        public Vector2 this[int value]
        {
            get { return mListOfVector[value]; }
        }

        public XPoint()
        {
            mListOfVector = new List<Vector2>();
        }

        public void Add(Vector2 v)
        {
            mListOfVector.Add(v);
        }

        public static Vector2 ConvertToPoint(string t)
        {
            Vector2 temp = Vector2.Zero;
            string[] parts = t.Split(',');
            temp.X = float.Parse(parts[0].Trim());
            temp.Y = float.Parse(parts[1].Trim());

            return temp;
        }

    }

    class XMLReader
    {
        XDocument mXDocument;

        XPoint mListOfPoint;

        public XMLReader(string fileName)
        {
            mXDocument = XDocument.Load(fileName);

            mListOfPoint = new XPoint();
        }

        public XPoint ReadFile()
        {
            var points = mXDocument.Descendants("Points");
            foreach (var p in points.Elements("Point"))
            {
                mListOfPoint.Add(XPoint.ConvertToPoint(p.Value.ToString()));
            }

            foreach (var c in points.Elements("Center"))
            {
                mListOfPoint.Center = XPoint.ConvertToPoint(c.Value.ToString());
            }

            return mListOfPoint;
        }

        public Vector2 GetCenter()
        {
            return mListOfPoint.Center;
        }
    }
}
