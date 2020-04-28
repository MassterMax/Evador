using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] float ampl;
    [SerializeField] float maxDegree;
    int m = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //m *= -1;

        transform.Rotate(Vector3.forward * m * Random.Range(0, ampl) * Time.deltaTime);

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

        //Debug.Log(transform.eulerAngles.z);
    }
}
