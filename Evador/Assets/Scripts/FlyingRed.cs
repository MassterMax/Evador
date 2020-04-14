using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRed : Wall
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float realizationTime;
    [SerializeField] float maxScale;
    SpriteRenderer sr;

    public override void DefaultSettings()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 1);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        transform.rotation = new Quaternion();
    }

    void Start()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 1);
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        maxScale -= transform.localScale.x;
    }

    void Update()
    {
        if (sr.color.a < 1 && moving)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
            transform.localScale += new Vector3(maxScale / realizationTime, maxScale / realizationTime, 0) * Time.deltaTime;
            sr.color += new Color(0, 0, 0, 1 / realizationTime * Time.deltaTime);
        }
    }
}
