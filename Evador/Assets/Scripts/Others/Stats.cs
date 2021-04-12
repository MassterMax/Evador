using System;
using System.Text;
using System.IO;
using UnityEngine;

public static class Stats
{
    public static string path;
    public static string version;
    public static bool running = false;
    public static int currentLevel = 1;
    public static int numOfLevels = 16; // Количество сделанных уровней
    public static int maxLevel = 0; // Количество пройденных уровней
    public static int numOfDeaths = 0;
    public static int numOfShards = 0;
    public static bool[] shards = new bool[6];
    public static bool music = true;

    public static string NoAds = "none";
    public const string CHECK = "19f3tdoy4g5h5hfgmty545j4krsgg5hj65y45nrym5wrj5k8lpdfsdgerhtthhe28"; // НЕ МЕНЯТЬ!!!!!!!!!!!!!
    public static bool AdsRemoved { get => CHECK == NoAds; }

    public static string lng = "eng";

    /// <summary>
    /// Метод для сохранения прогресса
    /// </summary>
    /// <param name="levelToSave">сколько уровней мы прошли</param>
    public static void SaveProgress(int levelToSave)
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
                sw.WriteLine(NoAds);
                sw.WriteLine(lng);

                sw.Flush();
                sw.Close();
            }
        }
        catch (Exception) { }
    }

    /// <summary>
    /// Метод для считывания прогресса
    /// </summary>
    public static void ReadProgress()
    {
        try
        {
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

                NoAds = sr.ReadLine();
                lng = sr.ReadLine();

                sr.Close();
            }
        }
        catch (Exception) { }
    }
}