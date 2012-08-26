using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Json;
using System.Text;

namespace LudumDare24.Tools
{
    public class StreamUtilities
    {
        public static Stream StringToStream(string json)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            return new MemoryStream(byteArray);
        }

        public static string StreamToString(Stream stream)
        {
            string result;
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        public static string Compress<T>(T setting)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                //using (GZipStream compressedStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                //{
                //    serializer.WriteObject(compressedStream, setting);
                //}
                serializer.WriteObject(memoryStream, setting);

                memoryStream.Position = 0;
                using (StreamReader reader = new StreamReader(memoryStream))
                {
                    return reader.ReadToEnd();
                }
            }

            //return compressedBytes;
        }

        public static T Decompress<T>(string compressedStream)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            T restoredData;
            //using (GZipStream decompressedStream = new GZipStream(compressedStream, CompressionMode.Decompress, true))
            //{
            //    restoredData = (T)serializer.ReadObject(decompressedStream);
            //}
            restoredData = (T)serializer.ReadObject(StringToStream(compressedStream));

            return restoredData;
        }
    }
}