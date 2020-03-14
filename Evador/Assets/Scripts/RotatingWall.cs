using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingWall : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 30f; // Угол поворота в секунду

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }
}
