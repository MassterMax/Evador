using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadorScript : TriggerHandler
{
    Color32[] clrs;
    [SerializeField] List<GameObject> parts;

    int count = 0;
    bool shouldReset;

    void Start()
    {
        count = 0;
        shouldReset = true;
        clrs = new Color32[] { new Color32(255, 255, 255, 255), new Color32(255, 222, 62, 255), new Color32(255, 142, 26, 255)};

        for (int i = 0; i < 3; ++i)
        {
            parts[i].GetComponent<SpriteRenderer>().color = clrs[0];
            parts[i].SetActive(true);
        }
    }

    public override void OnTrigger() // Здесь мы делаем чуть больше - меняем батарейку и телепортируем игрока
    {
        shouldReset = false;
        count++;

        if (count < 3)
            FindObjectOfType<GameManager>().Teleport();
        else
            shouldReset = true;

        for (int i = 0; i < 3; ++i)
        {
            if (i < 3 - count)
            {
                parts[i].GetComponent<SpriteRenderer>().color = clrs[count];
            }
            else
            {
                parts[i].SetActive(false);
            }
        }
    }

    public override void DefaultSettings() // Здесь тоже чуть сложнее, сбрасываем только если должны
    {
        if (shouldReset)
        {
            count = 0;
            for (int i = 0; i < 3; ++i)
            {
                parts[i].GetComponent<SpriteRenderer>().color = clrs[0];
                parts[i].SetActive(true);
            }
        }
        shouldReset = true;
    }
}