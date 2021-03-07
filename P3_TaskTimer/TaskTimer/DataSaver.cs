using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TaskTimer
{
    public class DataSaver
    {
        public static T LoadFromFile<T>(string filePath, string encryptionKey = "")
        {
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string fileContents = sr.ReadToEnd();

                    T setting = JsonConvert.DeserializeObject<T>(fileContents);
                    return setting;
                }
            }

            return default(T);
        }
        public static void WriteToFile<T>(T data, string filePath, string encryptionKey = "")
        {
            string json = JsonConvert.SerializeObject(data);
            
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(json);
            }
        }
    }
}
