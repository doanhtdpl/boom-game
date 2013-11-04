using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.Serialization
{
    public interface ISerializer
    {
        void SerializeString(string name, object obj);
        void SerializeObject(string name, ISerializable serObject);

        ISerializer SubSerializer(string name);
    }
}