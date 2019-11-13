using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Gauss
{
    class Serializer
    {
        public static void Pack(Object obj, String matrixName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(matrixName, FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, obj);
            stream.Close();
        }

        public static void PackMatrix(Object obj, String matrixName)
        {
            Pack(obj, matrixName + ".matrix");
        }

        public static void PackVector(Object obj, String matrixName)
        {
            Pack(obj, matrixName + ".vector");
        }

        public static Object Unpack(String matrixName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(matrixName + ".matrix", FileMode.Open, FileAccess.Read);
            return formatter.Deserialize(stream);
        }

    }
}
