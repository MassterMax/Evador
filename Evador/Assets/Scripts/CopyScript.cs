using UnityEngine;

public class CopyScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<GameManager>().GameOver();
    }
}
