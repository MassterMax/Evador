using UnityEngine;
using System.Collections;

public class CopyScript : MonoBehaviour
{
    /// <summary>
    /// В случае столкновения проверяем, столкнулись ли со стеной
    /// находимся ли мы в области видимости, началась ли игра
    /// </summary>
    /// <param name="collision"> С кем столкнулись </param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && Mathf.Abs(transform.position.x) < 3 && FindObjectOfType<GameManager>().gameHasStarted)
        {
            FindObjectOfType<GameManager>().GameOver();
            StartCoroutine(HidePlayer());
            StopCoroutine(HidePlayer());
        }
        else if (collision.gameObject.layer == 9) // Если с финишем, то уровень пройден!
        {
            FindObjectOfType<GameManager>().LevelComplete();
        }
    }

    void Start() // Говорим стоп системе частиц!
    {
        GetComponent<ParticleSystem>().Pause();
    }

    /// <summary>
    /// Метод прячет игрока при смерти(IEnumerator нужен только чтобы сделать задержку)
    /// </summary>
    /// <returns></returns>
    IEnumerator HidePlayer()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(.15f);
        GetComponent<SpriteRenderer>().color = Color.clear;
        GetComponent<ParticleSystem>().Play();
    }
}