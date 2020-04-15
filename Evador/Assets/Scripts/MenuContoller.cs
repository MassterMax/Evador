using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class MenuContoller : MonoBehaviour
{
    [SerializeField] Text levelText;
    [SerializeField] Text settingsText;
    [SerializeField] Text locationText;

    [SerializeField] Image levelBox;
    //[SerializeField] SpriteRenderer Hex;

    [SerializeField] GameObject panel;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject settingsButton;
    [SerializeField] GameObject secondSettingsButton;
    [SerializeField] GameObject hidingPanel;
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] GameObject slider;
    [SerializeField] GameObject slider1;
    [SerializeField] GameObject slider2;
    [SerializeField] GameObject slider3;
    [SerializeField] GameObject musicButton;

    Image panelBG, backImage, hidingImage, slider1Image, slider2Image, slider3Image, musicImage;

    string[] levels = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };
    string[] locations = { "abstract space", "candy land", "deep swarm", "my office" };
    Color32[] colors = { Color.white, new Color32(255, 134, 149, 255), new Color32(32, 106, 73, 255), new Color32(228, 200, 100, 255) };

    int maxLevel;
    int selectedLevel = 1;

    public int SelectedLevel { get => selectedLevel; set { if (0 < value && value <= maxLevel) selectedLevel = value; } }

    public void OnCloseButton()
    {
        Application.Quit();
    }

    public void OnLeftButton()
    {
        SelectedLevel--;
        levelText.text = levels[selectedLevel - 1];
        levelText.color = colors[(selectedLevel - 1) / 3];

        locationText.color = colors[(selectedLevel - 1) / 3];
        locationText.text = locations[(selectedLevel - 1) / 3];

        levelBox.color = colors[(selectedLevel - 1) / 3];
    }

    public void OnRightButton()
    {
        SelectedLevel++;
        levelText.text = levels[selectedLevel - 1];
        levelText.color = colors[(selectedLevel - 1) / 3];

        locationText.color = colors[(selectedLevel - 1) / 3];
        locationText.text = locations[(selectedLevel - 1) / 3];

        levelBox.color = colors[(selectedLevel - 1) / 3];

        //Debug.Log(levelText.color + " " + colors[(selectedLevel - 1) / 3]);
    }

    public void OnPlayButton()
    {
        FindObjectOfType<DummyPlayerContoller>().canMove = true;
        Stats.currentLevel = selectedLevel;
        Invoke("LevelLoad", 2f);

        hidingPanel.SetActive(true);

        StartCoroutine(ShowSomething(new Image[1] { hidingImage},
            new Text[0],
            new GameObject[0],
            new GameObject[0]));
        StopCoroutine("ShowSomething");
    }

    public void OnSettingsButton()
    {
        if (backButton.activeInHierarchy)
            return;

        StartCoroutine(ShowSomething(new Image[6] { panelBG, backImage, slider1Image, slider2Image, slider3Image, musicImage },
            new Text[1] { settingsText },
            new GameObject[1] { settingsCanvas },
            new GameObject[0]));

        StopCoroutine("ShowSomething");
    }

    public void OnBackButton()
    {
        if (backImage.color.a < 1)
            return;

        StartCoroutine(HideSomething(new Image[6] { panelBG, backImage, slider1Image, slider2Image, slider3Image, musicImage },
            new Text[1] { settingsText },
            new GameObject[0],
            new GameObject[1] { settingsCanvas }));

        StopCoroutine("HideSomething");
    }

    public void OnSlider()
    {
        Stats.horizontalSpeed = slider.GetComponent<Slider>().value;
        Stats.SaveProgress(maxLevel);
    }

    public void OnMusic()
    {
        Stats.music = !Stats.music;
        if (Stats.music)
            musicImage.color = new Color32(255, 255, 255, 255);
        else
            musicImage.color = new Color32(130, 130, 130, 255);

        Stats.SaveProgress(maxLevel);
    }

    void LevelLoad()
    {
        SceneManager.LoadScene(SelectedLevel);
    }

    void Start()
    {
        //Debug.Log(Application.persistentDataPath + "/data");
        if (Screen.height != Screen.safeArea.height)
        {
            Vector3 delta = settingsButton.transform.position - secondSettingsButton.transform.position;
            settingsButton.transform.position = backButton.transform.position += delta;
        }


        settingsText.text = $"Спасибо за помощь в разработке!\r\n\r\nВы умерли всего лишь {Stats.numOfDeaths} раз";
        if (Stats.numOfDeaths % 10 >= 2 && Stats.numOfDeaths % 10 <= 4 && !(Stats.numOfDeaths / 10 % 10 == 1))
            settingsText.text += "а";
        settingsText.text += "!\r\n\r\nСлайдер для изменения чувствительности боковой скорости:";

        panelBG = panel.GetComponent<Image>();
        backImage = backButton.GetComponent<Image>();
        hidingImage = hidingPanel.GetComponent<Image>();
        slider1Image = slider1.GetComponent<Image>();
        slider2Image = slider2.GetComponent<Image>();
        slider3Image = slider3.GetComponent<Image>();
        musicImage = musicButton.GetComponent<Image>();

        maxLevel = Stats.MaxLevel;

        StartCoroutine(HideSomething(new Image[1] { panelBG },
            new Text[0],
            new GameObject[1] { hidingPanel},
            new GameObject[1] {settingsCanvas}));

        StopCoroutine("HideSomething");

        if (Stats.music)
            musicImage.color = new Color32(255, 255, 255, 0);
        else
            musicImage.color = new Color32(130, 130, 130, 0);

        SelectedLevel = maxLevel;
        levelText.text = levels[SelectedLevel - 1];
        locationText.text = locations[(SelectedLevel - 1) / 3];
        levelText.color = colors[(selectedLevel - 1) / 3];
        locationText.color = colors[(selectedLevel - 1) / 3];
        levelBox.color = colors[(selectedLevel - 1) / 3];

        slider.GetComponent<Slider>().value = Stats.horizontalSpeed;
    }

    IEnumerator ShowSomething(Image[] images, Text[] texts, GameObject[] startState, GameObject[] lateState)
    {
        foreach (GameObject g in startState)
            g.SetActive(true);

        while (images[0].color.a < 1)
        {
            yield return new WaitForFixedUpdate();

            foreach (Image i in images)
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + 0.0005f);

            foreach (Text t in texts)
                t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a + 0.0005f);
        }

        foreach (GameObject g in lateState)
            g.SetActive(true);
    }

    IEnumerator HideSomething(Image[] images, Text[] texts, GameObject[] startState, GameObject[] lateState)
    {
        foreach (GameObject g in startState)
            g.SetActive(false);

        while (images[0].color.a > 0)
        {
            yield return new WaitForFixedUpdate();

            foreach (Image i in images)
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - 0.0005f);

            foreach (Text t in texts)
                t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a - 0.0005f);
        }

        foreach (GameObject g in lateState)
            g.SetActive(false);
    }

}
