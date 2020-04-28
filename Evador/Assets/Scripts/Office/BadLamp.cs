using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadLamp : TriggerHandler
{
    [SerializeField] GameObject badLight;
    [SerializeField] bool isAct;
    [SerializeField] GameObject littleButton;
    [SerializeField] float size;

    [SerializeField] GameObject finishOb;
    Vector3 finishPos;
    Vector3 startPos;

    bool moving1 = false;
    float speed = 1f;

    public override void DefaultSettings()
    {
        littleButton.transform.position = startPos;
        Start();
    }

    public override void OnTrigger()
    {
        badLight.SetActive(!isAct);
        moving1 = true;
    }

    void Start()
    {
        startPos = littleButton.transform.position;
        moving1 = false;
        badLight.SetActive(isAct);
        finishPos = finishOb.transform.position;
    }

    void Update()
    {
        if (moving1)
        {
            littleButton.transform.position = Vector3.MoveTowards(littleButton.transform.position, finishPos, speed * Time.deltaTime);

            if (Finished(littleButton.transform.position, finishPos))
                moving1 = false;
        }
    }

    bool Finished(Vector3 p1, Vector3 p2)
    {
        return Vector3.Distance(p1, p2) < 0.01f;
    }
}
