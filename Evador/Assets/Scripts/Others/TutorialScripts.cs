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

    public void PlayTutorial()
    {
        scene.SetActive(false);
        //canvas.SetActive(false);
        button.SetActive(false);
        foreach (Text t in texts)
            t.color = new Color();

        da = interval / ticks;

        StartCoroutine(textFading());
    }

    IEnumerator textFading()
    {
        int count = texts.Count;

        for (int i = 0; i < count; ++i)
        {
            StartCoroutine(fading(texts[i]));
            yield return new WaitForSeconds(interval);
        }

        //StopAllCoroutines();
        StartCoroutine(showTutAndBut());
    }

    IEnumerator fading(Text t)
    {
        while (t.color.r < 1)
        {
            yield return new WaitForSeconds(da);
            t.color += new Color(da, da, da);
        }
    }

    IEnumerator showTutAndBut()
    {
        scene.SetActive(true);
        //Debug.Log("HERE");
        FindObjectOfType<LittleSceneScript>().StartTheShow();
        yield return new WaitForSeconds(5);
        button.SetActive(true);
    }

    public void GotIt()
    {
        scene.SetActive(false);
        canvas.SetActive(false);
    }
}
