using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickWithTrigger : TriggerHandler
{
    [SerializeField] StickFalling stick;

    public override void DefaultSettings()
    {
        stick.Invoke("DefaultSettings", 0f);
    }

    public override void OnTrigger()
    {
        stick.fallen = false;
    }
}
