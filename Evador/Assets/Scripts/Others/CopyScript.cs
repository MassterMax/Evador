using UnityEngine;
using System.Collections;

public class CopyScript : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 && Mathf.Abs(transform.position.x) < 3) //wall
        {
            //Debug.Log(transform.position.x);
            FindObjectOfType<GameManager>().GameOver();
            StartCoroutine(HidePlayer());
            StopCoroutine(HidePlayer());
        }
        else if (collision.gameObject.layer == 9)
        {
            FindObjectOfType<GameManager>().LevelComplete();
        }
    }

    void Start()
    {
        GetComponent<ParticleSystem>().Pause();
    }

    IEnumerator HidePlayer()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(.15f);
        GetComponent<SpriteRenderer>().color = Color.clear;
        GetComponent<ParticleSystem>().Play();
    }
}
