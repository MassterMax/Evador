using UnityEngine;

public class FaceScript : TriggerHandler
{
    [SerializeField] BubbleScript b; //Пузырь, который содержит в себе голова

    public override void DefaultSettings()
    {
        b.DefaultSettings();
    }

    public override void OnTrigger()
    {
        b.moving = true;
    }
}