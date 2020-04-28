using System;
using System.Text;
using System.IO;
using UnityEngine;

public static class Stats
{
    public static string path = Application.persistentDataPath + "/.data";

    public static bool running = false;
    public static int currentLevel = 1;
    public static int numOfLevels = 12; // Количество сделанных уровней
    public static int maxLevel = 0; // Количество пройденных уровней
    public static int numOfDeaths = 0;
    public static int numOfShards = 0;
    public static bool[] shards = new bool[6];
    public static bool music = true;

    public static void SaveProgress(int levelToSave)//lvl to save - сколько уровней мы прошли
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Unicode))
            {
                sw.WriteLine(levelToSave);
                sw.WriteLine(numOfDeaths);
                sw.WriteLine(music);

                string s = "";
                foreach (bool b in shards)
                    if (b)
                        s += "1";
                    else
                        s += "0";

                sw.WriteLine(s);
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
            //Debug.Log(path);
            using (StreamReader sr = new StreamReader(path, Encoding.Unicode))
            {
                maxLevel = int.Parse(sr.ReadLine());
                numOfDeaths = int.Parse(sr.ReadLine());
                music = bool.Parse(sr.ReadLine());
                string s = sr.ReadLine();
                int i = 0;
                foreach (char c in s)
                {
                    if (c == '1')
                        shards[i] = true;
                    i++;
                }

                sr.Close();
            }
        }
        catch (Exception) { }
    }
}
