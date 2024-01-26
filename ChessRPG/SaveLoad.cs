using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ChessRPG
{
    public static class SaveLoad
    {
        public static void Save<T>(T obj, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StreamWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, obj);
            writer.Close();
        }

        public static T Load<T>(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StreamReader reader = new StreamReader(path);
            var obj =  (T)serializer.Deserialize(reader);
            reader.Close();
            return obj;
        }
    }
}
