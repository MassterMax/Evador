﻿using UnityEngine;

[ExecuteInEditMode]  // Нужно, чтобы в редакторе юнити отображались изменения
public class MovingWall : Wall
{
    [SerializeField] GameObject wall, stPoint, fPoint;
    [SerializeField] [Range(0f, 1f)] float startPos;
    [SerializeField] bool lookingToFinish = true;
    [SerializeField] float speed = 7f;
    [SerializeField] [Range(0f, 90f)] float rotationAngle;

    Vector3 directionV; // Вектор движения

    bool rememberLooking; // Сохраняем стартовое направление

    float sqrLen; // Расстояние между точками
    float sqrLenToStart, sqrLenToFinish; // Растояние до крайних точек

    public override void DefaultSettings()
    {
        lookingToFinish = rememberLooking;
        Start();
    }

    void Start() // Подготовка перед запуском
    {
        rememberLooking = lookingToFinish;
        directionV = fPoint.transform.position - stPoint.transform.position;
        sqrLen = directionV.sqrMagnitude;
        wall.transform.position = directionV * startPos + stPoint.transform.position;

        float angle = Mathf.Atan2(directionV.y, directionV.x);
        wall.transform.rotation = Quaternion.Euler(0, 0, rotationAngle + 90 + angle * Mathf.Rad2Deg);
    }

    void Update()
    {
        if (!Application.isPlaying) // Нужно, чтобы в редакторе юнити отображались изменения
        {
            Start();
            return;
        }

        if (!moving) // Остановка во время смерти
            return;

        sqrLenToStart = (wall.transform.position - stPoint.transform.position).sqrMagnitude;
        sqrLenToFinish = (wall.transform.position - fPoint.transform.position).sqrMagnitude;

        if (lookingToFinish) // Основное движение
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