using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SSCEngine.Serialization
{
    public interface ISerialization
    {
        ISerializer Serialize(Stream s);
        IDeserializer Deserialize(Stream s);

        void CompleteSerialization(Stream s);
    }
}