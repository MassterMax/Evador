using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public class MenuContoller : MonoBehaviour
{
    [SerializeField] Text levelText, locationText, settingsText;
    [SerializeField] Image levelBox;

    [SerializeField] List<GameObject> shardsImages;

    [SerializeField] GameObject backButton, settingsButton, questionButton;
    [SerializeField] GameObject settingsMenu;

    [SerializeField] GameObject tutorialCanvas;

    [SerializeField] GameObject hexagon;

    [SerializeField] GameObject hidingPanel, secondHidingPanel;
    [SerializeField] GameObject musicButton;

    [SerializeField] GameObject endMenu;

    Image hidingPanelImage, musicIconImage;

    [SerializeField] GameObject No, Yes;
    [SerializeField] Image leftBut, rightBut;

    List<Image> shardsIm = new List<Image>();

    AudioManager AM;

    string[] levels = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };
    string[] locations = { "abstract space", "candy land", "deep swarm", "my office" };
    Color32[] colors = { Color.white, new Color32(255, 134, 149, 255), new Color32(32, 106, 73, 255), new Color32(228, 200, 100, 255) };



    bool hexPressed = false;
    int maxLevel;
    int selectedLevel = 1;
    public int SelectedLevel
    {
        get => selectedLevel;

        set {
            if (0 < value && value <= maxLevel + 1 && value <= Stats.numOfLevels)
                selectedLevel = value;
        }
    }

    public void OnCloseButton()
    {
        Application.Quit();
    }

    public void OnLeftButton()
    {
        SelectedLevel--;
        SetMenuTextAndColor();
    }

    public void OnRightButton()
    {
        SelectedLevel++;
        SetMenuTextAndColor();
    }

    public void OnPlayButton()
    {
        FindObjectOfType<DummyPlayerContoller>().canMove = true;
        Stats.currentLevel = SelectedLevel;
        Invoke("LevelLoad", 2f);

        hidingPanel.SetActive(true);
        StartCoroutine(ImageFading(hidingPanelImage));
        StopCoroutine("ImageFading");
    }

    public void OnSettingsButton()
    {
        settingsMenu.SetActive(true);
    }

    public void OnBackButton()
    {
        OnNotResetButton();
        settingsMenu.SetActive(false);
    }

    public void OnQuestionButton()
    {
        tutorialCanvas.SetActive(true);
        FindObjectOfType<TutorialScripts>().PlayTutorial();
    }

    public void OnMusicButton()
    {
        Stats.music = !Stats.music;
        Stats.SaveProgress(maxLevel);
        SetMusic();
    }

    public void OnResetButton()
    {
        if (File.Exists(Stats.path))
            File.Delete(Stats.path);
        Application.Quit();
    }

    public void OnNotResetButton()
    {
        No.SetActive(false);
        Yes.SetActive(false);
    }

    public void OnPreResetButton()
    {
        No.SetActive(!No.activeSelf);
        Yes.SetActive(!Yes.activeSelf);
    }

    public void OnHexagon()
    {
        if (Stats.numOfShards == 6 && !hexPressed)
        {
            AM.SetFinal();
            AM.SetLoop(false);
            AM.Play();

            secondHidingPanel.SetActive(true); // Включаем панель, которая не дает нажимать на кнопочки


            foreach (GameObject g in shardsImages)
                shardsIm.Add(g.GetComponent<Image>());

            StartCoroutine(hexRounding());
        }
    }

    void LevelLoad()
    {
        SceneManager.LoadScene(SelectedLevel);
    }

    void Awake()
    {
        tutorialCanvas.SetActive(false);
        if (!File.Exists(Stats.path))
            OnQuestionButton();
    }

    void Start()
    {
        if (Screen.height != Screen.safeArea.height)
        {
            Vector3 delta = questionButton.transform.position - settingsButton.transform.position;
            settingsButton.transform.position = backButton.transform.position = questionButton.transform.position;
            questionButton.transform.position += delta;
        }

        OnNotResetButton();
        settingsMenu.SetActive(false);
        hidingPanel.SetActive(false);
        secondHidingPanel.SetActive(false);
        endMenu.SetActive(false);

        musicIconImage = musicButton.GetComponent<Image>();
        hidingPanelImage = hidingPanel.GetComponent<Image>();

        SelectedLevel = maxLevel = Stats.maxLevel;
        SelectedLevel += 1;

        SetShards();
        SetMusic();
        SetSettingsText();
        SetMenuTextAndColor();
    }

    IEnumerator ImageFading(Image im)
    {
        while (im.color.a < 1)
        {
            yield return new WaitForFixedUpdate();
            im.color = new Color(im.color.r, im.color.g, im.color.b, im.color.a + 0.0005f);
        }
    }

    void SetMenuTextAndColor()
    {
        leftBut.color = new Color(1, 1, 1, 0.6f);
        rightBut.color = new Color(1, 1, 1, 0.6f);

        if (SelectedLevel == 1)
            leftBut.color = new Color(1, 1, 1, 0.25f);
        if (SelectedLevel == maxLevel + 1 || SelectedLevel == Stats.numOfLevels)
            rightBut.color = new Color(1, 1, 1, 0.25f);

        levelText.text = levels[SelectedLevel - 1];
        locationText.text = locations[(SelectedLevel - 1) / 3];
        levelText.color = colors[(SelectedLevel - 1) / 3];
        locationText.color = colors[(SelectedLevel - 1) / 3];
        levelBox.color = colors[(SelectedLevel - 1) / 3];
    }

    void SetSettingsText()
    {
        settingsText.text = $"Thanks for your testing ^.^!\r\nLevels completed: {maxLevel}\r\nNumber of deaths: {Stats.numOfDeaths}\r\n" +
            $"Shards collected: {Stats.numOfShards}";
    }

    void SetMusic()
    {
        if (Stats.music)
            musicIconImage.color = new Color32(255, 255, 255, 255);
        else
            musicIconImage.color = new Color32(130, 130, 130, 255);

        AM = FindObjectOfType<AudioManager>();

        AM.Mute(!Stats.music);
    }

    void SetShards()
    {
        int i = 0;
        int count = 6;
        foreach (bool b in Stats.shards)
        {
            if (!b)
            {
                shardsImages[i].SetActive(false);
                count--;
            }
            i++;
        }
        //Debug.Log(count);
        Stats.numOfShards = count;
    }

    IEnumerator hexRounding()
    {
        float time = 0f;
        float timer2 = 0f;
        float speedMulti = 1;
        float maxTime = 10f;
        while (time < maxTime)
        {
            yield return new WaitForFixedUpdate();
            speedMulti = 1 + F(time);
            time += Time.fixedDeltaTime;
            timer2 += Time.fixedDeltaTime;

            if (timer2 > 0.2f)
            {
                foreach (Image s in shardsIm)
                    ColorFading(s);
                //Debug.Log("iteration");
                timer2 -= 0.2f;
            }

            hexagon.transform.Rotate(Vector3.forward * 180 * speedMulti * Time.fixedDeltaTime);
            hexagon.transform.localScale += (new Vector3(1.5f, 1.5f, 0) * speedMulti * Time.deltaTime);

            if (time > 8f && !endMenu.activeSelf)
                endMenu.SetActive(true);
        }
        FindObjectOfType<EndMenuScript>().PlayEnd();
    }

    float F(float x)
    {
        if (x < 0 || x > 8)
            return 0;
        float output = 3 - Mathf.Abs(2 * x / 5f - 2);
        if (x < 5f)
            output -= 0.5f;
        else
            output += 0.5f;
        return output;
    }

    void ColorFading(Image i)
    {
        //Debug.Log(i.color);
        i.color += new Color(0.03f, 0.03f, 0.03f);

    }
}
