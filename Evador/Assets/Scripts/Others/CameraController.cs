using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    const int offset = 3;

    void LateUpdate()
    {
        transform.position = new Vector3(0, target.position.y + offset, -10); // Просто передвигаемся за игроком
    }
}