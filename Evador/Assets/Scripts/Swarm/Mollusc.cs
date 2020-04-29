using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mollusc : TriggerHandler
{
    [SerializeField] GameObject upJaw;
    [SerializeField] GameObject lowJaw;
    [SerializeField] GameObject rotPoint;

    Vector3 upV, lowV;
    Vector3 rotUp, rotLow;
    Vector3 rotV;
    float angle;
    float a = 90f;
    bool canEat;

    public override void DefaultSettings()
    {
        canEat = false;
        upJaw.transform.position = upV;
        lowJaw.transform.position = lowV;

        upJaw.transform.localEulerAngles = rotUp;
        lowJaw.transform.localEulerAngles = rotLow;

        //if (gameObject.name == "Mollusc" || gameObject.name == "Mollusc0")
        //    Debug.Log(gameObject.name + " default " + Time.time + " " + canEat);
    }

    public override void OnTrigger()
    {
        canEat = true;
        //if (gameObject.name == "Mollusc" || gameObject.name == "Mollusc0")
        //    Debug.Log(gameObject.name + " trigger " + Time.time + " " + canEat);
    }

    void Start()
    {
        upV = upJaw.transform.position;
        lowV = lowJaw.transform.position;
        rotV = rotPoint.transform.position;

        rotUp = upJaw.transform.localEulerAngles;
        rotLow = lowJaw.transform.localEulerAngles;

        angle = (rotUp.z + rotLow.z) / 2f + 180f;

        canEat  = false;
    }

    void Update()
    {
        if (canEat)
        {
            if (angle - lowJaw.transform.localEulerAngles.z > 0.01)
            {
                upJaw.transform.RotateAround(rotV, Vector3.back, a * Time.deltaTime);
                lowJaw.transform.RotateAround(rotV, Vector3.forward, a * Time.deltaTime);
            }
        }

    }
}
