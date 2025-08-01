﻿using UnityEngine;

public class Shard : MonoBehaviour
{
    [SerializeField] int num; // Номер от 0 до 5 определяет цвет

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 0)  // Если коснулись с default (то есть с игроком)
        {
            FindObjectOfType<GameManager>().shardCollected = true;
            FindObjectOfType<GameManager>().shardNumber = num;
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        if (Stats.shards[num])
            gameObject.SetActive(false);
    }
} 