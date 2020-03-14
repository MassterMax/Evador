using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;

    void LateUpdate()
    {
        transform.position = new Vector3(0, target.position.y, 0) + offset;
    }
}
