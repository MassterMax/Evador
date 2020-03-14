using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject leftCopy, middleCopy, rightCopy;
    [SerializeField] float verticalSpeed = 10f; // Вертикальная скорость.
    [SerializeField] float horizontalSpeed = 10f; // Горизонтальная скорость.

    Vector2 direction; // Направление движения по оси OX.
    Vector2 deltaX; // Расстояние половины экрана.

    void Start()
    {
        deltaX = FindObjectOfType<GameManager>().deltaPos;
        deltaX.y = 0;
        TeleportCopies();
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x) >= 2 * deltaX.x) // Если центральная копия ушла далеко за экран, то тпшим назад.
        {
            transform.position = new Vector3((transform.position.x + 2 * deltaX.x) % deltaX.x, transform.position.y, 0);
            TeleportCopies();
        }

        direction = new Vector2();

        if (Input.touchCount > 0) // Управление пальцами.
        {
            direction = OnTouch(Input.GetTouch(0));

            if (Input.touchCount > 1)
                direction += OnTouch(Input.GetTouch(1));
        }
        else if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)) // Управление мышкой.
        {
            if (Input.GetKey(KeyCode.Mouse0))
                direction += Vector2.left;
            if (Input.GetKey(KeyCode.Mouse1))
                direction += Vector2.right;
        }

        direction = direction.normalized;
        transform.Translate((Vector2.up * verticalSpeed + direction * horizontalSpeed) * Time.deltaTime);
    }

    Vector2 OnTouch(Touch t)
    {
        Vector2 tPos = Camera.main.ScreenToWorldPoint(t.position);
        if (tPos.x < 0)
            return Vector2.left;
        else
            return Vector2.right;
    }

    void TeleportCopies()
    {
        leftCopy.transform.position = (Vector2)transform.position - 2 * deltaX;
        middleCopy.transform.position = transform.position;
        rightCopy.transform.position = (Vector2)transform.position + 2 * deltaX;
    }
}
