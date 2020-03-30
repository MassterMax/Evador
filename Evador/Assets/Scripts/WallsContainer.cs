using System.Collections.Generic;
using UnityEngine;

public class WallsContainer : MonoBehaviour
{
    [SerializeField] List<Wall> walls;

    public void DoDefaultSettings()
    {
        foreach (Wall w in walls)
            if (w != null)
            {
                w.DefaultSettings();
                w.moving = false;
            }
    }

    public void LaunchWalls()
    {
        foreach (Wall w in walls)
            if (w != null)
                w.moving = true;
    }
}
