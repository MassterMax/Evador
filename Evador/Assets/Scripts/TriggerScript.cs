using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] TriggerHandler hnd;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 8) //not a wall 
        {
            hnd.Invoke("OnTrigger", 0f);
        }
    }
}
