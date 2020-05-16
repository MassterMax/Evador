using UnityEngine;

public class LolipopScript : Wall
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float realizationTime; // Время полного роста
    [SerializeField] float maxScale; // Максимальный размер
    [SerializeField] Vector3 startScale;
    bool grow = true; // Статус (растет/уменьшается)

    public override void DefaultSettings()
    {
        transform.localScale = startScale;
        transform.rotation = new Quaternion();
    }

    void Start()
    {
        transform.localScale = startScale;
        maxScale -= transform.localScale.x;
    }

    void Update()
    {
        if (moving)
        {
            // Крутим, если можно
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));

            if (grow)  // Увеличиваем/уменьшаем объект в зависимости от стадии
            {
                transform.localScale += new Vector3(maxScale / realizationTime, maxScale / realizationTime, 0) * Time.deltaTime;

                if (transform.localScale.x >= maxScale + startScale.x)
                    grow = false;
            }
            else
            {
                transform.localScale -= new Vector3(maxScale / realizationTime, maxScale / realizationTime, 0) * Time.deltaTime;

                if (transform.localScale.x <= startScale.x)
                    grow = true;
            }

        }  
    }
}