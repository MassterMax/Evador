using UnityEngine;

public class RotatingChups : Wall
{
    [SerializeField] GameObject rotPoint; // Точка, вокруг которой вращаем и скорость (в угле вращения альфа)
    [SerializeField] float A;

    Vector3 startPos, rotPos;

    public override void DefaultSettings()
    {
        transform.position = startPos;
        transform.rotation = new Quaternion();
    }

    void Start()
    {
        startPos = transform.position;
        rotPos = rotPoint.transform.position;
    }

    void Update()
    {
        if (moving) // Просто вращаем вокруг заданной точки
        {
            transform.RotateAround(rotPos, Vector3.forward, A * Time.deltaTime);
        }
    }
}