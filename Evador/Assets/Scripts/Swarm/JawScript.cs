using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawScript : Wall
{
    [SerializeField] GameObject head;
    [SerializeField] float MaxDifference = 15f;
    float startAngle, minAngle, maxAngle;
    float currentAngle;
    Vector3 pos;
    Quaternion rot;
    public bool phase = false;

    public override void DefaultSettings() //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
    {
        transform.position = pos;
        transform.rotation = rot;
        phase = false;
        moving = false;
    }

    void Start()
    {
        pos = transform.position;
        rot = transform.rotation;

        startAngle = transform.rotation.eulerAngles.z;

        minAngle = startAngle - MaxDifference;
        if (minAngle >= 180) minAngle = minAngle - 360;

        maxAngle = startAngle + MaxDifference;
        if (maxAngle >= 180) maxAngle = maxAngle - 360;
    }

    void Update()
    {
        if (moving && !phase)
        {
            currentAngle = transform.rotation.eulerAngles.z;
            currentAngle = currentAngle <= 180 ? currentAngle : currentAngle - 360f;

            //Debug.Log(minAngle+ " " + currentAngle + " " + (startAngle + MaxDifference));

            if (currentAngle < minAngle)
                transform.RotateAround(head.transform.position, Vector3.forward, Random.Range(0, 90f) * Time.deltaTime);
            else if (currentAngle > maxAngle)
                transform.RotateAround(head.transform.position, Vector3.forward, Random.Range(-90f, 0) * Time.deltaTime);
            else
                transform.RotateAround(head.transform.position, Vector3.forward, Random.Range(-90f, 90f) * Time.deltaTime);
        }
        else if (moving && phase)
        {
            currentAngle = transform.rotation.eulerAngles.z;
            currentAngle = currentAngle <= 180 ? currentAngle : currentAngle - 360f;

            if (-45 - currentAngle > 0.01)
                transform.RotateAround(head.transform.position, Vector3.forward, 60 * Time.deltaTime);
            else if (currentAngle + 35 > 0.1)
                transform.RotateAround(head.transform.position, Vector3.forward, -60 * Time.deltaTime);
        }
    }
}
