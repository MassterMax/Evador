using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceScript : TriggerHandler
{
    [SerializeField] BubbleScript b;

    public override void DefaultSettings()
    {
        b.Invoke("DefaultSettings", 0f);
    }

    public override void OnTrigger()
    {
        b.moving = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
