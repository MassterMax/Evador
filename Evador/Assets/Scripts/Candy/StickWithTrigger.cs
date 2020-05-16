using UnityEngine;

public class StickWithTrigger : TriggerHandler
{
    [SerializeField] StickFalling stick;

    public override void DefaultSettings()
    {
        stick.DefaultSettings();
    }

    public override void OnTrigger()
    {
        stick.fallen = false;
    }
}