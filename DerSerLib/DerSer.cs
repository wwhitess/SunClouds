using Newtonsoft.Json;

namespace DerSerLib
{
    public class jsonclass
    {
        private static string rootFolder = Path.GetFullPath(@"..\..\..\");

        public static void JsonSer<T>(T type, string fileName)
        {
            string JsonWrite = JsonConvert.SerializeObject(type);
            File.WriteAllText(rootFolder + "\\" + fileName, JsonWrite);
        }
        public static T JsonDeser<T>(string fileName)
        {
            T Mytype = JsonConvert.DeserializeObject<T>(fileName);
            return Mytype;
        }
    }
}