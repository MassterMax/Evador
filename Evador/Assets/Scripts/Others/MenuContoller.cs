using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class MenuContoller : MonoBehaviour
{
    [SerializeField] Text levelText, locationText, settingsText;
    [SerializeField] Image levelBox;

    [SerializeField] List<GameObject> shardsImages;

    [SerializeField] GameObject backButton, settingsButton, secondSettingsButton;
    [SerializeField] GameObject settingsMenu;

    [SerializeField] GameObject hidingPanel;
    [SerializeField] GameObject musicButton;
    Image hidingPanelImage, musicIconImage;

    string[] levels = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X", "XI", "XII" };
    string[] locations = { "abstract space", "candy land", "deep swarm", "my office" };
    Color32[] colors = { Color.white, new Color32(255, 134, 149, 255), new Color32(32, 106, 73, 255), new Color32(228, 200, 100, 255) };

    int maxLevel;
    int selectedLevel = 1;
    public int SelectedLevel { get => selectedLevel; set { if (0 < value && value <= maxLevel + 1 && value <= Stats.numOfLevels) selectedLevel = value; } }

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
        settingsMenu.SetActive(false);
    }

    public void OnMusicButton()
    {
        Stats.music = !Stats.music;
        Stats.SaveProgress(maxLevel);
        SetMusic();
    }

    public void OnHexagon()
    {

    }

    void LevelLoad()
    {
        SceneManager.LoadScene(SelectedLevel);
    }

    void Start()
    {
        if (Screen.height != Screen.safeArea.height)
        {
            Vector3 delta = settingsButton.transform.position - secondSettingsButton.transform.position;
            settingsButton.transform.position = backButton.transform.position += delta;
        }

        settingsMenu.SetActive(false);
        hidingPanel.SetActive(false);

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

        FindObjectOfType<AudioManager>().GetComponent<AudioSource>().mute = !Stats.music; // СТРАШНА
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
}
