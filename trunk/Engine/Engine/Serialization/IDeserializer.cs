using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSCEngine.Serialization
{
    public interface IDeserializer
    {
        void Deserialize(string serName, ISerializable ser);
        string DeserializeString(string serName);
        int DeserializeInteger(string serName);
        double DeserializeDouble(string serName);
        ICollection<IDeserializer> DeserializeAll(string serName);
        IDeserializer SubDeserializer(string serName);
    }
}
