using UnityEngine;

public class Pencil : TriggerHandler
{
    [SerializeField] [Range(0f, 1f)] float finishPoint; // На какой части пути надо остановиться
    [SerializeField] GameObject pencil;
    [SerializeField] float speed;
    Vector3 startPos, finishPos;
    bool moving1;

    public override void DefaultSettings()
    {
        moving1 = false;
        pencil.transform.position = startPos;
    }

    public override void OnTrigger()
    {
        moving1 = true;
    }

    void Start()
    {
        moving1 = false;
        startPos = pencil.transform.position;
        finishPos = new Vector3(startPos.x * (1 - finishPoint), startPos.y, startPos.z);
    }

    void Update()
    {
        if (moving1)
        {
            pencil.transform.position = Vector3.MoveTowards(pencil.transform.position, finishPos, speed * Time.deltaTime);

            if (Finished(pencil.transform.position, finishPos))
                moving1 = false;
        }
    }
}