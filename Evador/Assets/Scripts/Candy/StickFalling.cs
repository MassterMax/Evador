using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickFalling : Wall
{
    [SerializeField] float time;
    [SerializeField] bool clockwise = true;
    public bool fallen = false;
    float dangle;
    float fangle;
    Vector3 pos;

    public override void DefaultSettings()
    {
        fallen = true;
        transform.rotation = new Quaternion();
        transform.position = pos;
    }

    void Start()
    {
        pos = transform.position;
        fallen = true;
        dangle = 90 / time;
        fangle = 90;

        if (clockwise)
        {
            dangle = -dangle;
            fangle = 270;
        }
    }

    void Update()
    {
        if (!fallen)
        {
            transform.RotateAround(transform.parent.position, Vector3.forward, dangle * Time.deltaTime);

            if (clockwise && transform.rotation.eulerAngles.z <= fangle)
                fallen = true;
            if (!clockwise && transform.rotation.eulerAngles.z >= fangle)
                fallen = true;
        }
    }
}
