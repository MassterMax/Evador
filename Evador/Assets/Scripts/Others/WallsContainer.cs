using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контейнер для стен, которые надо перезапускать
/// </summary>
public class WallsContainer : MonoBehaviour
{
    [SerializeField] List<Wall> walls;
    /// <summary>
    /// Вызываем настройки по умолчанию у всех детей из списка
    /// </summary>
    public void DoDefaultSettings()
    {
        foreach (Wall w in walls)
            if (w != null)
            {
                w.DefaultSettings();
                w.moving = false;
            }
    }
    /// <summary>
    /// Запускаем стены
    /// </summary>
    public void LaunchWalls()
    {
        foreach (Wall w in walls)
            if (w != null)
                w.Restart();
    }
}

