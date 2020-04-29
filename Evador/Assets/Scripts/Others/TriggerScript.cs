using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] TriggerHandler hnd;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 0 && FindObjectOfType<GameManager>().gameHasStarted) //player
        {
            hnd.OnTrigger();
            //Debug.Log(collision.gameObject.name + " " + Time.time + " " + collision.gameObject.GetComponent<SpriteRenderer>().color);
        }
    }
}
