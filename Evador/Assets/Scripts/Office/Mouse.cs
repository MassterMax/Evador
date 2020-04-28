using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : TriggerHandler
{
    [SerializeField] GameObject coursor;
    [SerializeField] GameObject trigger;
    [SerializeField] float speed;
    Vector3 startPos;
    bool moving1;


    public override void DefaultSettings()
    {
        moving1 = false;
        coursor.transform.position = startPos;
    }

    public override void OnTrigger()
    {
        moving1 = true;
    }

    void Start()
    {
        moving1 = false;
        startPos = coursor.transform.position;
    }

    void Update()
    {
        if (moving1)
        {
            coursor.transform.position = Vector3.MoveTowards(coursor.transform.position, trigger.transform.position, speed * Time.deltaTime);

            if (Finished(coursor.transform.position, trigger.transform.position))
                moving1 = false;
        }
    }

    bool Finished(Vector3 p1, Vector3 p2)
    {
        return Vector3.Distance(p1, p2) < 0.01f;
    }
}
