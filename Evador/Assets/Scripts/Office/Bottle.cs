using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] float ampl;
    [SerializeField] float maxDegree;
    int m = -1; // Отвечает, в какую сторону вращать

    void Update()
    {
        transform.Rotate(Vector3.forward * m * Random.Range(0, ampl) * Time.deltaTime);

        // Если перешли границу вращения, то возвращаем к границе и начинаем в другую сторону
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