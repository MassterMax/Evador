using UnityEngine;

[ExecuteInEditMode]
public class movingFish : Wall
{
    [SerializeField] protected GameObject wall, stPoint, fPoint;
    [SerializeField] [Range(0f, 1f)] float startPos;
    [SerializeField] protected bool lookingToFinish = true;
    [SerializeField] float speed = 7f;
    [SerializeField] [Range(0f, 90f)] float rotationAngle;

    Vector3 directionV; // Вектор движения.

    bool rememberLooking;

    protected float sqrLen; // Расстояние между точками
    protected float sqrLenToStart, sqrLenToFinish; // Растояние до крайних точек.

    public override void DefaultSettings()
    {
        lookingToFinish = rememberLooking;

        if (wall.transform.localScale.x < 0)
            wall.transform.localScale *= -1;

        Start();
    }

    void Start()
    {
        rememberLooking = lookingToFinish;
        directionV = fPoint.transform.position - stPoint.transform.position;
        sqrLen = directionV.sqrMagnitude;
        wall.transform.position = directionV * startPos + stPoint.transform.position;

        float angle = Mathf.Atan2(directionV.y, directionV.x);
        wall.transform.rotation = Quaternion.Euler(0, 0, rotationAngle + 90 + angle * Mathf.Rad2Deg);
    }

    protected virtual void Update()
    {
        if (!Application.isPlaying)
        {
            Start();
            return;
        }

        if (!moving)
            return;

        sqrLenToStart = (wall.transform.position - stPoint.transform.position).sqrMagnitude;
        sqrLenToFinish = (wall.transform.position - fPoint.transform.position).sqrMagnitude;

        if (lookingToFinish) // Двигаем, если достигли конца, то меняем напрвавление движения и разворачиваем спрайт
        {
            if (sqrLenToStart < sqrLen)
                wall.transform.position = Vector3.MoveTowards(wall.transform.position, fPoint.transform.position, speed * Time.deltaTime);
            else
            {
                lookingToFinish = false;
                wall.transform.localScale *= -1;
            }
        }
        else
        {
            if (sqrLenToFinish < sqrLen)
                wall.transform.position = Vector3.MoveTowards(wall.transform.position, stPoint.transform.position, speed * Time.deltaTime);
            else
            {
                lookingToFinish = true;
                wall.transform.localScale *= -1;
            }
        }
    }
}