using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRed : Wall
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float realizationTime;
    [SerializeField] float maxScale;
    [SerializeField] Vector3 startScale;
    bool grow = true;

    public override void DefaultSettings()
    {
        transform.localScale = startScale;
        transform.rotation = new Quaternion();
    }

    void Start()
    {
        transform.localScale = startScale;
        maxScale -= transform.localScale.x;
    }

    void Update()
    {
        if (moving)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));

            if (grow)
            {
                transform.localScale += new Vector3(maxScale / realizationTime, maxScale / realizationTime, 0) * Time.deltaTime;

                if (transform.localScale.x >= maxScale + startScale.x)
                    grow = false;
            }
            else
            {
                transform.localScale -= new Vector3(maxScale / realizationTime, maxScale / realizationTime, 0) * Time.deltaTime;

                if (transform.localScale.x <= startScale.x)
                    grow = true;
            }

        }  
    }
}
