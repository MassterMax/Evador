using UnityEngine;

public class CopyScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 9)
            FindObjectOfType<GameManager>().GameOver();
        else
            FindObjectOfType<GameManager>().LevelComplete();
    }
}
