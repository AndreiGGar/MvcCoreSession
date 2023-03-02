using System.Runtime.Serialization.Formatters.Binary;

namespace MvcCoreSession.Helpers
{
    public class HelperBinarySession
    {
        public static byte[] ObjectToByte (Object objeto)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, objeto);
                return ms.ToArray();
            }
        }
        public static Object ByteToObject(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(data, 0, data.Length);
                ms.Seek(0, SeekOrigin.Begin);
                Object objeto = (Object)formatter.Deserialize(ms);
                return objeto;
            }
        }
    }
}
