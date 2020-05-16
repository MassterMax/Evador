using UnityEngine;

public class RotatingWall : Wall
{
    [SerializeField] float rotationSpeed = 30f; // Угол поворота в секунду

    public override void DefaultSettings()
    {
        transform.rotation = new Quaternion();
    }

    void Update()
    {
        if (!moving)
            return;

        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }
}