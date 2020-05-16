using UnityEngine;

public abstract class Wall : MonoBehaviour
{
    /// <summary>
    /// Метод, который применяет настройки стены по умолчанию
    /// </summary>
    public abstract void DefaultSettings();
    [HideInInspector] public bool moving = true;

    // Метод для запуска стены заново
    public virtual void Restart()
    {
        moving = true;
    }

    /// <summary>
    /// Метод, который определяет, находятся ли два объекта в одной точке(с небольшой погрешностью)
    /// </summary>
    /// <param name="p1"> Первое положение </param>
    /// <param name="p2"> Второе положени </param>
    /// <returns> Объекты в одной точке?</returns>
    static protected bool Finished(Vector3 p1, Vector3 p2)
    {
        return Vector3.Distance(p1, p2) < 0.01f;
    }
}