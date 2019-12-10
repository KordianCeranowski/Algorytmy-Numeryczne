using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace RecommenderSystem
{
    internal class Serializer
    {
        public static readonly String FileExtension = ".rmat";
        public static readonly String Path = "../../../data/";

        public static void PackRMatrix(RMatrix rMatrix)
        {
            Pack(rMatrix, "p" + rMatrix.p + "u" + rMatrix.u);
        }

        private static void Pack(Object obj, String FileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path + FileName + FileExtension, FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, obj);
            stream.Close();
        }

        public static RMatrix UnpackRMatrix(string FileName)
        {
            return (RMatrix)Unpack(FileName);
            //return (RMatrix)Unpack("p" + p + "u" + u);
        }

        private static Object Unpack(String FileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path + FileName + FileExtension, FileMode.Open, FileAccess.Read);
            return formatter.Deserialize(stream);
        }

    }
}
