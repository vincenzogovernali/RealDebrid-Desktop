using System.IO;
using System.IO.IsolatedStorage;
using System.Text;


namespace RealDebrid.service
{
    public class CacheService
    {

        private static IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();


        public static void save(String key, String value)
        {
            storage.CreateFile(key).WriteAsync(Encoding.ASCII.GetBytes(value));
        }

        public static string retrieve(String key)
        {
            if (storage.FileExists(key))
            {
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(key, FileMode.Open, FileAccess.Read, FileShare.Read, storage))
                {
                    using (StreamReader reader = new StreamReader(isoStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            return null;
        }
    }
}
