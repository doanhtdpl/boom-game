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
        static public String SOUND = "Sound.txt";
        static public String GAME_SCORE_BASIC = "GAME_SCORE_BASIC.txt";
        static public String GAME_SCORE_TIME = "GAME_SCORE_TIME.txt";
        static public String GAME_SCORE_BOMB = "GAME_SCORE_BOMB.txt";
        static public String GAME_LEVEL = "Level.txt";

        static public void SaveSoundVolume(bool isMute)
        {
#if WINDOWS_PHONE
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication();
#else
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForDomain();
#endif
            if (savegameStorage.FileExists(SOUND))
            {
                using (IsolatedStorageFileStream fs = savegameStorage.OpenFile(SOUND, System.IO.FileMode.Truncate))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.WriteLine(isMute);
                    }
                }
            }
            else
            {
                IsolatedStorageFileStream fs = null;
                using (fs = savegameStorage.CreateFile(SOUND))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.WriteLine(isMute);
                    }
                }
            }
        }

        static public bool LoadSoundVolume()
        {
#if WINDOWS_PHONE
            using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication())
#else
            using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForDomain())
#endif
            {
                if (savegameStorage.FileExists(SOUND))
                {
                    using (IsolatedStorageFileStream fs = savegameStorage.OpenFile(SOUND, System.IO.FileMode.Open))
                    {
                        if (fs != null)
                        {
                            using (StreamReader reader = new StreamReader(fs))
                            {
                                bool isMute = Convert.ToBoolean(reader.ReadLine());
                                return isMute;
                            }
                        }
                    }
                }
                else
                    return false;
            }
            return false;
        }

        static public void SaveGameScore(String gameType, int map, double score)
        {
            string currentMap = "Map_" + map;
            string allText = "";
#if WINDOWS_PHONE
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication();
#else
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForDomain();
#endif
            if (savegameStorage.FileExists(gameType))
            {
                using (IsolatedStorageFileStream fs = savegameStorage.OpenFile(gameType, System.IO.FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        allText = reader.ReadToEnd();
                    }
                }
                using (IsolatedStorageFileStream fs = savegameStorage.OpenFile(gameType, System.IO.FileMode.Truncate))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        bool hasGameType = false;
                        string[] lines = allText.Split('\n');
                        for (int i = 0; i < lines.Length; ++i)
                        {
                            if (lines[i].Contains(currentMap))
                            {
                                lines[i] = currentMap + ":" + score;
                                hasGameType = true;
                            }
                            writer.WriteLine(lines[i]);
                        }
                        if (!hasGameType)
                        {
                            writer.WriteLine( currentMap + ":" + score);
                        }
                    }
                }
            }
            else
            {
                IsolatedStorageFileStream fs = null;
                using (fs = savegameStorage.CreateFile(gameType))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.WriteLine(currentMap + ":" + score);
                    }
                }
            }
        }

        static public void LoadGameScore(String gameType, int map, out double score)
        {
            score = 0;

#if WINDOWS_PHONE
            using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication())
#else
            using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForDomain())
#endif
            {
                if (savegameStorage.FileExists(gameType))
                {
                    using (IsolatedStorageFileStream fs = savegameStorage.OpenFile(gameType, System.IO.FileMode.Open))
                    {
                        if (fs != null)
                        {
                            using (StreamReader reader = new StreamReader(fs))
                            {
                                while (!reader.EndOfStream)
                                {
                                    String line = reader.ReadLine();
                                    if (line.Contains("Map_1" + map))
                                    {
                                        string[] arr = line.Split(':');
                                        if(arr.Length >= 2)
                                            score = Convert.ToInt32(arr[1]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void LoadLevel(String gameType, out int result)
        {
            result = 0;
            using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (savegameStorage.FileExists(GAME_LEVEL))
                {
                    using (IsolatedStorageFileStream fs = savegameStorage.OpenFile(GAME_LEVEL, System.IO.FileMode.Open))
                    {
                        if (fs != null)
                        {
                            using (StreamReader reader = new StreamReader(fs))
                            {
                                while (!reader.EndOfStream)
                                {
                                    String line = reader.ReadLine();
                                    if (line.Contains(gameType))
                                    {
                                        string[] arr = line.Split(':');
                                        if (arr.Length >= 2)
                                            result = Convert.ToInt32(arr[1]);
                                    }
                                }
                                reader.Close();
                                fs.Close();
                            }
                        }
                    }
                }
            }
        }

        public static void SaveLevel(String gameType, int value)
        {
            string allText = "";
            using (IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (savegameStorage.FileExists(GAME_LEVEL))
                {
                    using (IsolatedStorageFileStream fs = savegameStorage.OpenFile(GAME_LEVEL, System.IO.FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(fs))
                        {
                            allText = reader.ReadToEnd();
                        }
                    }
                    using (IsolatedStorageFileStream fs = savegameStorage.OpenFile(GAME_LEVEL, System.IO.FileMode.Truncate))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            bool hasGameType = false;
                            string[] lines = allText.Split('\n');
                            for (int i = 0; i < lines.Length; ++i)
                            {
                                if (lines[i].Contains(gameType))
                                {
                                    lines[i] = gameType + ":" + value;
                                    hasGameType = true;
                                }
                                writer.WriteLine(lines[i]);
                            }
                            if (!hasGameType)
                            {
                                writer.WriteLine(gameType + ":" + value);
                            }
                        }
                    }
                }
                else
                {
                    IsolatedStorageFileStream fs = null;
                    using (fs = savegameStorage.CreateFile(GAME_LEVEL))
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            writer.WriteLine(gameType + ":" + value);
                            writer.Close();
                            fs.Close();
                        }
                    }
                }
            }
        }
    }
}
