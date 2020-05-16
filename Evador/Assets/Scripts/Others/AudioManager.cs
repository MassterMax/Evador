using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager copy;

    // В awake удаляем ненужный менеджер (они дублируются и я не нашел, как это исправить)
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (copy == null)
            copy = this;
        else
            Destroy(gameObject);
    }

    AudioClip final;
    AudioClip start;
    AudioSource AS;

    /// <summary>
    /// Выключение звука
    /// </summary>
    /// <param name="value"> true - если выключаем, false иначе</param>
    public void Mute(bool value)
    {
        AS.mute = value;
    }

    /// <summary>
    /// Начать играть музыку
    /// </summary>
    public void Play()
    {
        AS.Play();
    }

    /// <summary>
    /// Поставить аудио по кругу
    /// </summary>
    /// <param name="value"> true - да, false иначе</param>
    public void SetLoop(bool value)
    {
        AS.loop = value;
    }

    /// <summary>
    /// Установить финальный трек
    /// </summary>
    public void SetFinal()
    {
        AS.clip = final;
    }

    /// <summary>
    /// Установить стартовый трек
    /// </summary>
    public void SetStart()
    {
        AS.clip = start;
    }

    void Start() // Подгружаем ресурсы при запуске игры
    {
        AS = GetComponent<AudioSource>();
        final = (AudioClip)Resources.Load("Music/final");
        start = (AudioClip)Resources.Load("Music/default");
    }

}