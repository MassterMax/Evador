using UnityEngine;

public abstract class Wall : MonoBehaviour
{
    public abstract void DefaultSettings();
    [HideInInspector] public bool moving = true;
    public virtual void Restart()
    {
        moving = true;
    }
}
