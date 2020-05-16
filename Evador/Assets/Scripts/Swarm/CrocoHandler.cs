using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocoHandler : TriggerHandler
{
    [SerializeField] JawScript jaw1, jaw2; // Челюсти, с которыми мы делаем все операции

    public override void DefaultSettings()
    {
        jaw1.Invoke("DefaultSettings", 0f);
        jaw2.Invoke("DefaultSettings", 0f);
    }

    public override void OnTrigger()
    {
        jaw1.phase = true;
        jaw2.phase = true;
    }

    public override void Restart()
    {
        moving = true;
        jaw1.moving = true;
        jaw2.moving = true;
    }
}