using System;
using System.Text;
using System.IO;
using UnityEngine;

public static class Stats
{
    static string path = Application.persistentDataPath + "/data";

    public static bool running = false;
    public static int currentLevel = 1;
    public static int numOfLevels = 3; // Количество сделанных уровней
    static int maxLevel = 1; // Максимальный открытый уровень
    public static int numOfDeaths = 0;
    public static bool music = true;

    public static float horizontalSpeed = 7f; // Записываем скорость по горизонтали.

    public static int MaxLevel { get { return maxLevel; } set { SaveProgress(value); maxLevel = value; } }

    public static void SaveProgress(int levelToSave)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Unicode))
            {
                sw.WriteLine(levelToSave);
                sw.WriteLine(numOfDeaths);
                sw.WriteLine(horizontalSpeed);
                sw.Write(music);
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
                horizontalSpeed = float.Parse(sr.ReadLine());
                music = bool.Parse(sr.ReadLine());
                sr.Close();
            }
        }
        catch (Exception) { }
    }
}
