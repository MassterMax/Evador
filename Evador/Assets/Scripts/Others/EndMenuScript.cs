using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenuScript : MonoBehaviour
{
    [SerializeField] GameObject Credits;
    [SerializeField] List<Text> texts;
    float interval = 4f;
    float da;
    int ticks = 100;
    bool canSkip = false;

    void Start()
    {
        Credits.SetActive(false);
        foreach (Text t in texts)
            t.color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        if (canSkip && (Input.touchCount > 0 || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            FindObjectOfType<AudioManager>().SetStart();
            FindObjectOfType<AudioManager>().Play();
            FindObjectOfType<AudioManager>().SetLoop(true);
            SceneManager.LoadScene(0);
        }
    }

    public void PlayEnd()
    {
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

        for (int i = 0; i < count; ++i)
        {
            StartCoroutine(hiding(texts[i]));
        }

        Credits.SetActive(true);

        yield return new WaitForSeconds(60f);
        canSkip = true;
    }

    IEnumerator fading(Text t)
    {
        while (t.color.a < 1)
        {
            yield return new WaitForSeconds(da);
            t.color += new Color(0, 0, 0, da);
        }
    }

    IEnumerator hiding(Text t)
    {
        while (t.color.a > 0)
        {
            yield return new WaitForSeconds(da);
            t.color -= new Color(0, 0, 0, da);
        }
    }
}
