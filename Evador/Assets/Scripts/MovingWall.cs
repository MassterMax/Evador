using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MovingWall : MonoBehaviour
{
    [SerializeField] GameObject wall, stPoint, fPoint;
    [SerializeField] [Range(0f, 1f)] float startPos;
    [SerializeField] bool lookingToFinish = true;
    [SerializeField] float speed = 7f;
    [SerializeField] [Range(0f, 90f)] float rotationAngle;

    Vector3 directionV; // Вектор движения.

    float sqrLen; // Расстояние между точками
    float sqrLenToStart, sqrLenToFinish; // Растояние до крайних точек.

    void Start()
    {
        directionV = fPoint.transform.position - stPoint.transform.position;
        sqrLen = directionV.sqrMagnitude;
        wall.transform.position = directionV * startPos + stPoint.transform.position;

        float angle = Mathf.Atan2(directionV.y, directionV.x);
        wall.transform.rotation = Quaternion.Euler(0, 0, rotationAngle + 90 + angle * Mathf.Rad2Deg);
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            Start();
            return;
        }

        sqrLenToStart = (wall.transform.position - stPoint.transform.position).sqrMagnitude;
        sqrLenToFinish = (wall.transform.position - fPoint.transform.position).sqrMagnitude;

        if (lookingToFinish)
        {
            if (sqrLenToStart < sqrLen)
                wall.transform.position = Vector3.MoveTowards(wall.transform.position, fPoint.transform.position, speed * Time.deltaTime);
            else
                lookingToFinish = false;
        }
        else
        {
            if (sqrLenToFinish < sqrLen)
                wall.transform.position = Vector3.MoveTowards(wall.transform.position, stPoint.transform.position, speed * Time.deltaTime);
            else
                lookingToFinish = true;
        }
    }
}
