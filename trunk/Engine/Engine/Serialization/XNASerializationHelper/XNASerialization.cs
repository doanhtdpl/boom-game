using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.Serialization.XNASerializationHelper
{
    public class XNASerialization
    {
        private XNASerialization()
        {
        }

        public static XNASerialization Instance { get; private set; }

        static XNASerialization()
        {
            XNASerialization.Instance = new XNASerialization();
        }

        #region Tags
        const string tagX = "X";
        const string tagY = "Y";
        const string tagLeft = "X";
        const string tagTop = "Y";
        const string tagWidth = "Width";
        const string tagHeight = "Height";
        #endregion

        #region Serializes
        public void Serialize(ISerializer serializer, string name, Vector2 v)
        {
            ISerializer subSer = serializer.SubSerializer(name);

            subSer.SerializeString(tagX, v.X);
            subSer.SerializeString(tagY, v.Y);
        }

        public void Serialize(ISerializer serializer, string name, Rectangle r)
        {
            ISerializer subSer = serializer.SubSerializer(name);

            subSer.SerializeString(tagLeft, r.X);
            subSer.SerializeString(tagTop, r.Y);
            subSer.SerializeString(tagWidth, r.Width);
            subSer.SerializeString(tagHeight, r.Height);
        }
        #endregion


        #region Deseralizes
        public Vector2 DeserializeVector2(IDeserializer deserializer, string serName)
        {
            IDeserializer subDeser = deserializer.SubDeserializer(serName);

            return new Vector2((float) subDeser.DeserializeDouble(tagX), (float) subDeser.DeserializeDouble(tagY));
        }

        public Rectangle DeserializeRectangle(IDeserializer deserializer, string serName)
        {
            IDeserializer subDeser = deserializer.SubDeserializer(serName);

            return new Rectangle(subDeser.DeserializeInteger(tagLeft), subDeser.DeserializeInteger(tagTop), subDeser.DeserializeInteger(tagWidth), subDeser.DeserializeInteger(tagHeight));
        }
        #endregion
    }
}