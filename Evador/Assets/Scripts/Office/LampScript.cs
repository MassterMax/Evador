using UnityEngine;

public class LampScript : TriggerHandler
{
    [SerializeField] GameObject lght;
    [SerializeField] MothScript[] Moths;
    bool triggered = false;
    float dT = 0;

    void Start()
    {
        lght.SetActive(false);
    }

    public override void DefaultSettings()
    {
        foreach (MothScript m in Moths) // Успокаиваем кажду мушку
            m.DefaultSettings();
        lght.SetActive(false);
        triggered = false;
    }

    public override void OnTrigger()
    {
        if (!triggered) // Если нажали первый раз, то включаем свет
        {
            dT = Time.time;
            lght.SetActive(true);
            foreach (MothScript m in Moths)
                m.canFly = true;
            triggered = true;
        }
        else if (dT - Time.time > 1f) // Чтобы нельзя было сразу же выключить свет
        {
            lght.SetActive(false);
            foreach (MothScript m in Moths)
                m.canFly = false;
        }
    }
}