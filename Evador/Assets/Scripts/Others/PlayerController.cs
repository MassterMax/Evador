using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject leftCopy, middleCopy, rightCopy;
    [SerializeField] float verticalSpeed = 10f; // Вертикальная скорость.
    float horizontalSpeed = 7f; // Горизонтальная скорость.

    Vector2 direction; // Направление движения по оси OX.
    Vector2 deltaX; // Расстояние половины экрана.
    float deadY;
    float defautHor, defaultVer;

    float dK = 0.1f;
    float K = 0.1f; // Надо, чтобы симулировать "разгон игрока"

    void Start()
    {
        deadY = FindObjectOfType<GameManager>().deadY;
        deltaX = FindObjectOfType<GameManager>().deltaPos;
        deltaX.y = 0;
        TeleportCopies();
        defaultVer = verticalSpeed;
        defautHor = horizontalSpeed;
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
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) // Управление мышкой.
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                direction += Vector2.left;
            if (Input.GetKey(KeyCode.RightArrow))
                direction += Vector2.right;
        }

        direction = direction.normalized;

        if (direction.x != 0)
            K += dK;
        else
            K = 0.1f;

        K = K > 1 ? 1 : K;

        transform.Translate((Vector2.up * verticalSpeed + direction * horizontalSpeed * K) * Time.deltaTime);
    }

    Vector2 OnTouch(Touch t)
    {
        if (TouchInDeadArea(t))
            return new Vector2();

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

    public bool TouchInDeadArea(Touch t) // Если нажимаем на элемент интерфейса
    {
        return (Camera.main.ScreenToWorldPoint(t.position) - middleCopy.transform.position - new Vector3(0, 3, 0)).y >= deadY;
    }

    public void ResetSpeed()
    {
        verticalSpeed = 0f;
        horizontalSpeed = 0f;
    }

    public void DefaultParams()
    {
        leftCopy.GetComponent<SpriteRenderer>().color = Color.white;
        middleCopy.GetComponent<SpriteRenderer>().color = Color.white;
        rightCopy.GetComponent<SpriteRenderer>().color = Color.white;

        verticalSpeed = defaultVer;
        horizontalSpeed = defautHor;
    }
}
