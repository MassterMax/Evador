using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    [SerializeField] GameObject middleCopy, eyeHole;
    [SerializeField] float speed;
    Vector3 hole;

    private void Start()
    {
        hole = eyeHole.transform.position;
    }

    void Update()
    {
        if (Vector2.Distance(middleCopy.transform.position, transform.position) > 2)
            transform.position = Vector2.MoveTowards(transform.position, middleCopy.transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(hole, transform.position) > 0.5)
        {
            Vector3 pos = transform.position - hole;
            pos.Normalize();
            pos = pos / 2;
            transform.position = pos + hole;
        }

    }
}
