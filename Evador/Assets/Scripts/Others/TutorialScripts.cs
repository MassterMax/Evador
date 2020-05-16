using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScripts : MonoBehaviour
{
    [SerializeField] List<Text> texts;
    [SerializeField] GameObject button;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject scene;
    float interval = 4f;
    float da;
    int ticks = 100;

    /// <summary>
    /// Метод запускает обучение
    /// </summary>
    public void PlayTutorial()
    {
        scene.SetActive(false);

        button.SetActive(false);
        foreach (Text t in texts)
            t.color = new Color();

        da = interval / ticks;

        StartCoroutine(textFading());
    }

    /// <summary>
    /// Показывает текст вступления
    /// </summary>
    /// <returns></returns>
    IEnumerator textFading()
    {
        int count = texts.Count;

        for (int i = 0; i < count; ++i)
        {
            StartCoroutine(fading(texts[i]));
            yield return new WaitForSeconds(interval);
        }
        StartCoroutine(showTutAndBut());
    }

    /// <summary>
    /// Метод для плавного появления текста
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    IEnumerator fading(Text t)
    {
        while (t.color.r < 1)
        {
            yield return new WaitForSeconds(da);
            t.color += new Color(da, da, da);
        }
    }

    /// <summary>
    /// Включает маленькую сцену, активирует ее, а потом появляется кнопка GOT IT!
    /// </summary>
    /// <returns></returns>
    IEnumerator showTutAndBut()
    {
        scene.SetActive(true);
        FindObjectOfType<LittleSceneScript>().StartTheShow();
        yield return new WaitForSeconds(5);
        button.SetActive(true);
    }

    // При нажатии на кнопку 
    public void GotIt()
    {
        scene.SetActive(false);
        canvas.SetActive(false);
    }
}