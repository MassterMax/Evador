using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingChups : Wall
{
    [SerializeField] GameObject rotPoint;
    [SerializeField] float A;

    Vector3 startPos, rotPos;

    public override void DefaultSettings()
    {
        transform.position = startPos;
        transform.rotation = new Quaternion();
    }

    void Start()
    {
        startPos = transform.position;
        rotPos = rotPoint.transform.position;
    }

    void Update()
    {
        if (moving)
        {
            transform.RotateAround(rotPos, Vector3.forward, A * Time.deltaTime);
        }
    }
}
