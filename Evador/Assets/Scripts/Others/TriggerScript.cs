using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField] TriggerHandler hnd; // Обработчик триггера

    /// <summary>
    /// В случае коллизии проверяем коллизию с игроком и тот факт, что игра началась 
    /// (последнее нужно, чтобы не активировать триггер после смерти)
    /// </summary>
    /// <param name="collision"> Объект, с которым происходит столкновение</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 0 && FindObjectOfType<GameManager>().gameHasStarted) //player
        {
            hnd.OnTrigger();
        }
    }
}


