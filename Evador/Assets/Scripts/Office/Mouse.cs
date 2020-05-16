using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : TriggerHandler
{
    [SerializeField] GameObject coursor; // Сам курсор, который мы двигаем
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
        if (moving1) // Если можно двигать, то двигаемся к тригеру
        {
            coursor.transform.position = Vector3.MoveTowards(coursor.transform.position, trigger.transform.position, speed * Time.deltaTime);

            if (Finished(coursor.transform.position, trigger.transform.position))
                moving1 = false;
        }
    }
}