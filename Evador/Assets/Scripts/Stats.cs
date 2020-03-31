using System;
using System.Text;
using System.IO;
using UnityEngine;

public static class Stats
{
    static string path = Application.persistentDataPath + "/data";

    public static bool running = false;
    public static int currentLevel = 1;
    public static int numOfLevels = 4;
    static int maxLevel = 1;
    public static int numOfDeaths = 0;

    public static int MaxLevel { get { return maxLevel; } set { SaveProgress(value); maxLevel = value; } }

    static void SaveProgress(int levelToSave)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Unicode))
            {
                sw.WriteLine(levelToSave);
                sw.Write(numOfDeaths);
                sw.Flush();
                sw.Close();
            }
        }
        catch (Exception) { }
    }

    public static void ReadProgress()
    {
        try
        {
            using (StreamReader sr = new StreamReader(path, Encoding.Unicode))
            {
                maxLevel = int.Parse(sr.ReadLine());
                numOfDeaths = int.Parse(sr.ReadLine());
                sr.Close();
            }
        }
        catch (Exception) { }
    }
}
