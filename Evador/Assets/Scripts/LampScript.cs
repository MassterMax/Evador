﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampScript : TriggerHandler
{
    [SerializeField] GameObject lght;
    [SerializeField] MothScript[] Moths;
    bool triggered = false;

    void Start()
    {
        lght.SetActive(false);
    }

    public override void DefaultSettings()
    {
        foreach (MothScript m in Moths)
            m.Invoke("DefaultSettings", 0f);
        lght.SetActive(false);
        triggered = false;
    }

    public override void OnTrigger()
    {
        if (!triggered)
        {
            lght.SetActive(true);
            foreach (MothScript m in Moths)
                m.canFly = true;
            triggered = true;
        }
        else
        {
            lght.SetActive(false);
            foreach (MothScript m in Moths)
                m.canFly = false;
        }
    }
}
