using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
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

    [SerializeField] Text adsRemover;

    List<Image> shardsIm = new List<Image>();

    AudioManager AM;

    [SerializeField] GameObject langCanvas;

    string[] levels = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII", "XIII", "XIV", "XV", "XVI" };
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

    const int levelsInArea = 4;

    bool shouldPlayTutorial = false;

    public void OnCloseButton()
    {
        Application.Quit();
    }

    /// <summary>
    /// При нажатии на кнопку уменьшения уровня
    /// </summary>
    public void OnLeftButton()
    {
        SelectedLevel--;
        SetMenuTextAndColor();
    }

    /// <summary>
    /// При нажатии на кнопку увеличения уровня
    /// </summary>
    public void OnRightButton()
    {
        SelectedLevel++;
        SetMenuTextAndColor();
    }

    /// <summary>
    /// При нажатии на кнопку PLAY
    /// </summary>
    public void OnPlayButton()
    {
        FindObjectOfType<DummyPlayerContoller>().canMove = true;
        Stats.currentLevel = SelectedLevel;
        Invoke("LevelLoad", 2f);

        hidingPanel.SetActive(true);
        StartCoroutine(ImageFading(hidingPanelImage));
        StopCoroutine("ImageFading");
    }

    /// <summary>
    /// При нажатии на кнопку настроек
    /// </summary>
    public void OnSettingsButton()
    {
        settingsMenu.SetActive(true);
    }

    /// <summary>
    /// При нажатии на кнопку назад
    /// </summary>
    public void OnBackButton()
    {
        OnNotResetButton();
        settingsMenu.SetActive(false);
    }

    /// <summary>
    /// При нажатии на кнопку обучения
    /// </summary>
    public void OnQuestionButton()
    {
        shouldPlayTutorial = false;
        tutorialCanvas.SetActive(true);
        FindObjectOfType<TutorialScripts>().PlayTutorial();
    }

    /// <summary>
    /// При включении выключении звука
    /// </summary>
    public void OnMusicButton()
    {
        Stats.music = !Stats.music;
        Stats.SaveProgress(maxLevel);
        SetMusic();
    }

    /// <summary>
    /// При нажатии на сброс настроек
    /// </summary>
    public void OnResetButton()
    {
        if (File.Exists(Stats.path))
            File.Delete(Stats.path);
        Application.Quit();
    }

    /// <summary>
    /// Если игрок не решается сбросить настройки
    /// </summary>
    public void OnNotResetButton()
    {
        No.SetActive(false);
        Yes.SetActive(false);
    }

    /// <summary>
    /// При нажатии на CLEAR DATA
    /// </summary>
    public void OnPreResetButton()
    {
        No.SetActive(!No.activeSelf);
        Yes.SetActive(!Yes.activeSelf);
    }

    /// <summary>
    /// При нажатии на кнопку-шестиугольник
    /// </summary>
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

    public void OnAdRemove()
    {
        if (Stats.AdsRemoved)
            adsRemover.text = GetComponent<TextManager>().RemovedAdText();
    }

    public void OnChangeLanguage()
    {
        langCanvas.SetActive(true);
    }

    public void OnUSALanguage()
    {
        Stats.lng = "eng";
        Stats.SaveProgress(Stats.maxLevel);
        langCanvas.SetActive(false);

        AfterLanguageChanged();
    }

    public void OnRussianLanguage()
    {
        Stats.lng = "rus";
        Stats.SaveProgress(Stats.maxLevel);
        langCanvas.SetActive(false);

        AfterLanguageChanged();
    }

    void AfterLanguageChanged()
    {
        GetComponent<TextManager>().SetMenuText();

        switch (Stats.lng)
        {
            case "rus":
                locationText.text = TextManager.locations_rus[(SelectedLevel - 1) / levelsInArea];
                break;
            default:
                locationText.text = TextManager.locations_eng[(SelectedLevel - 1) / levelsInArea];
                break;
        }

        if (shouldPlayTutorial)
            OnQuestionButton();
    }

    /// <summary>
    /// Загрузка выбранного уровня
    /// </summary>
    void LevelLoad()
    {
        SceneManager.LoadScene(SelectedLevel);
    }

    void Awake()
    {
        Stats.path = Application.persistentDataPath + "/.data";
        Stats.version = Application.version;

        Debug.Log(Stats.path);

        tutorialCanvas.SetActive(false);
        langCanvas.SetActive(false);

        if (!File.Exists(Stats.path))
        {
            shouldPlayTutorial = true;
            OnChangeLanguage();
        }
    }

    void Start()
    {
        GetComponent<TextManager>().SetMenuText();

        if (Screen.height != Screen.safeArea.height) // Настройки по смещению UI
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

        OnAdRemove();
    }

    /// <summary>
    /// Метод для плавного включения картинки
    /// </summary>
    /// <param name="im"></param>
    /// <returns></returns>
    IEnumerator ImageFading(Image im)
    {
        while (im.color.a < 1)
        {
            yield return new WaitForFixedUpdate();
            im.color = new Color(im.color.r, im.color.g, im.color.b, im.color.a + 0.0005f);
        }
    }

    /// <summary>
    /// Метод для визуализации выбранного уровня
    /// </summary>
    void SetMenuTextAndColor()
    {
        leftBut.color = new Color(1, 1, 1, 0.6f);
        rightBut.color = new Color(1, 1, 1, 0.6f);

        if (SelectedLevel == 1)
            leftBut.color = new Color(1, 1, 1, 0.25f);
        if (SelectedLevel == maxLevel + 1 || SelectedLevel == Stats.numOfLevels)
            rightBut.color = new Color(1, 1, 1, 0.25f);

        levelText.text = levels[SelectedLevel - 1];

        switch (Stats.lng)
        {
            case "rus":
                locationText.text = TextManager.locations_rus[(SelectedLevel - 1) / levelsInArea];
                break;
            default:
                locationText.text = TextManager.locations_eng[(SelectedLevel - 1) / levelsInArea];
                break;
        }


        levelText.color = colors[(SelectedLevel - 1) / levelsInArea];
        locationText.color = colors[(SelectedLevel - 1) / levelsInArea];
        levelBox.color = colors[(SelectedLevel - 1) / levelsInArea];
    }

    /// <summary>
    /// Метод для установки текста настроек
    /// </summary>
    void SetSettingsText()
    {
        GetComponent<TextManager>().SetSettingsText();
    }

    /// <summary>
    /// Метод при изменении музыки
    /// </summary>
    void SetMusic()
    {
        if (Stats.music)
            musicIconImage.color = new Color32(255, 255, 255, 255);
        else
            musicIconImage.color = new Color32(130, 130, 130, 255);

        AM = FindObjectOfType<AudioManager>();

        AM.Mute(!Stats.music);
    }

    /// <summary>
    /// Метод для визуализации собранных осколков
    /// </summary>
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

        Stats.numOfShards = count;
    }

    /// <summary>
    /// Метод для вращения шестиугольника в случае прохождения игры
    /// </summary>
    /// <returns></returns>
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

                timer2 -= 0.2f;
            }

            hexagon.transform.Rotate(Vector3.forward * 180 * speedMulti * Time.fixedDeltaTime);
            hexagon.transform.localScale += (new Vector3(1.5f, 1.5f, 0) * speedMulti * Time.deltaTime);

            if (time > 8f && !endMenu.activeSelf)
                endMenu.SetActive(true);
        }
        FindObjectOfType<EndMenuScript>().PlayEnd();
    }

    /// <summary>
    /// Некоторая зависимость, нужная для разной скорости вращения шестиугольника
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Небольшое затухание картинки
    /// </summary>
    /// <param name="i"> Выбранная картинка</param>
    void ColorFading(Image i)
    {
        i.color += new Color(0.03f, 0.03f, 0.03f);
    }
}