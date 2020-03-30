using UnityEngine;

public class DummyPlayerContoller : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    public bool canMove = false;

    void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, -3, 0), speed * Time.deltaTime);
        }
    }
}
