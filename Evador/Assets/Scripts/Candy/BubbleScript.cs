using UnityEngine;

public class BubbleScript : Wall
{
    [SerializeField] float dR; // Изменение радиуса в секунду
    Vector3 StartPos;

    public override void DefaultSettings()
    {
        transform.position = StartPos;
        transform.localScale = new Vector3(0, 0, 1);
        moving = false;
    }

    void Start()
    {
        StartPos = transform.position;
        moving = false;
    }

    void Update()
    {
        if (moving) // Скрипт роста
        {
            transform.Translate(-dR * Time.deltaTime, 0, 0);
            transform.localScale += new Vector3(dR * Time.deltaTime, dR * Time.deltaTime, 0);

            if (transform.localScale.x >= 2f)
                moving = false;
        }
    }
}