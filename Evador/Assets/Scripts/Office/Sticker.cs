using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticker : TriggerHandler
{
    [SerializeField] GameObject stck;
    [SerializeField] GameObject trigger;
    [SerializeField] float speed;
    [SerializeField] float maxDegree;
    [SerializeField] float ampl;
    Vector3 startPos;
    bool moving1;
    float m = -1; // Определяет, в какую сторону качаться

    public override void DefaultSettings()
    {
        transform.rotation = new Quaternion();
        moving1 = false;
        stck.transform.position = startPos;
    }

    public override void OnTrigger()
    {
        moving1 = true;
    }

    void Start()
    {
        moving1 = false;
        startPos = stck.transform.position;
    }

    void Update()
    {
        if (moving1) // Если можно двигаться, то двигаемся
        {
            stck.transform.position = Vector3.MoveTowards(stck.transform.position, trigger.transform.position, speed * Time.deltaTime);

            if (Finished(stck.transform.position, trigger.transform.position))
                moving1 = false;

            transform.Rotate(Vector3.forward * m * Random.Range(0, ampl) * Time.deltaTime); // Поворачиваемся

            if (transform.eulerAngles.z > maxDegree && transform.eulerAngles.z < 180)
            {
                transform.eulerAngles = new Vector3(0, 0, maxDegree);
                m *= -1;
            }
            else if (transform.eulerAngles.z < 360f - maxDegree && transform.eulerAngles.z > 180)
            {
                transform.eulerAngles = new Vector3(0, 0, 360f - maxDegree);
                m *= -1;
            }
        }
    }
}