using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace ActiveDirectory.Models
{
    public class DeletedPerson
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public static byte[] BinarySerialize(object graph)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(stream, graph);

                return stream.ToArray();
            }
        }

        public static object BinaryDeserialize(byte[] buffer)
        {
            using (var stream = new MemoryStream(buffer))
            {
                var formatter = new BinaryFormatter();

                return formatter.Deserialize(stream);
            }
        }
    }

}