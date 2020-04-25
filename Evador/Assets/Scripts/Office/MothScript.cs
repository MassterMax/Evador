using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothScript : MonoBehaviour
{
    [SerializeField] float speed = 0.4f;
    public bool canFly = false;

    Vector2 startPos;
    const float border = 2.5f;

    public void DefaultSettings()
    {
        transform.position = startPos;
        canFly = false;
    }

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (canFly)
        {
            transform.Translate(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * speed * Time.deltaTime);
            if (transform.position.x < -border || transform.position.x > border)
                transform.position = new Vector2(border * Mathf.Sign(transform.position.x), transform.position.y);
        }
    }
}
