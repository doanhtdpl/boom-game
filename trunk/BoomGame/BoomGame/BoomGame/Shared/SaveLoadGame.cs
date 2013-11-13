using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;
using System.IO;

namespace BoomGame.Shared
{
    public static class SaveLoadGame
    {
        public static void Load_Level(String fileName, out int result)
        {
            result = 0;
            using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (savegameStorage.FileExists(fileName))
                {
                    using (IsolatedStorageFileStream fs = savegameStorage.OpenFile(fileName, System.IO.FileMode.Open))
                    {
                        if (fs != null)
                        {
                            // Reload the saved high-score data.
                            byte[] saveBytes = new byte[4];
                            int count = fs.Read(saveBytes, 0, 4);
                            if (count > 0)
                            {
                                result = System.BitConverter.ToInt32(saveBytes, 0);
                            }
                        }
                    }
                }
            }
        }

        public static void Save_Level(String fileName, int value)
        {
            using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fs = savegameStorage.OpenFile(fileName, System.IO.FileMode.Truncate))
                {
                    if (fs != null)
                    {
                        // Saved high-score data.
                        byte[] saveBytes = System.BitConverter.GetBytes(value);
                        fs.Write(saveBytes, 0, saveBytes.Length);
                    }
                }
            }
        }
    }
}
