using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using GoogleMobileAds.Api;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject WhiteBG;
    [SerializeField] GameObject pauseButton;
    [SerializeField] Text lvlCompleteText;
    [SerializeField] GameObject homeButton;
    [SerializeField] GameObject SecondHomeButton;
    [SerializeField] SpriteRenderer finishSR;

    [SerializeField] GameObject playBUTTON;

    [SerializeField] GameObject NextLevelButton;

    public int shardNumber;
    public bool shardCollected;

    public float deadY;

    public Vector2 deltaPos;
    public float width, height;
    public bool gameHasStarted = false;

    const string appID = "ca-app-pub-7272958162561065~3900634957";
    const string gameOverAD = "ca-app-pub-7272958162561065/5808064252";
    InterstitialAd interstitial;

    /// <summary>
    /// Метод, который вызывается при смерти
    /// </summary>
    public void GameOver()
    {
        gameHasStarted = false;
        Stats.numOfDeaths++;

        pauseButton.SetActive(false);

        WhiteBG.SetActive(true);

        FindObjectOfType<PlayerController>().ResetSpeed();
        FindObjectOfType<WallsContainer>().DoDefaultSettings();

        StartCoroutine(Restart());
        StopCoroutine(Restart());

        Stats.SaveProgress(Stats.maxLevel);
    }

    /// <summary>
    /// Телепортация игрока в стартовую позицию
    /// </summary>
    public void Teleport()
    {
        player.transform.position = new Vector2(0, -3);
        FindObjectOfType<WallsContainer>().DoDefaultSettings();
    }

    /// <summary>
    /// При завершении уровня
    /// </summary>
    public void LevelComplete()
    {
        if (Stats.currentLevel == Stats.maxLevel + 1 && Stats.maxLevel <= Stats.numOfLevels)
            Stats.maxLevel++;

        if (shardCollected)
            Stats.shards[shardNumber] = true;

        StartCoroutine(Wait());
        StopCoroutine(Wait());

        Stats.SaveProgress(Stats.maxLevel);
    }

    void Awake()
    {
        CreateAd();
        LoadAd();

        float h = Screen.height;
        float w = Screen.width;

        if (h < w) { Application.Quit(); }

        float orthoSize = finishSR.bounds.size.x * h / w * 0.5f; // Магические вычисления по приведению камеры к нужному состоянию + ресайз кнопки play
        float k = Camera.main.orthographicSize / orthoSize;
        Camera.main.orthographicSize = orthoSize;
        if (playBUTTON != null)
            playBUTTON.transform.localScale *= k;


        if (homeButton != null) // Определение мертвой зоны (чтобы при нажатии на кнопку паузы игрок не перемещался)
        {
            deadY = (homeButton.transform.position.y + pauseButton.transform.position.y) / 2;
        }

        if (!Stats.running) // Запускаем игру и читаем прогресс
        {
            Stats.running = true;
            Stats.ReadProgress();
        }

        width = Camera.main.pixelWidth;

        height = Camera.main.pixelHeight;
        deltaPos = Camera.main.ScreenToWorldPoint(new Vector2(width, height));

        if (WhiteBG != null) WhiteBG.SetActive(false);

        if (SecondHomeButton != null)
        {
            SecondHomeButton.SetActive(false);
            if (NextLevelButton != null)
                NextLevelButton.SetActive(false);
            gameHasStarted = true;
        }

    }

    /// <summary>
    /// Телепортируем игрока на старт и перезапускаем уровень
    /// </summary>
    /// <returns></returns>
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(2f);

        ShowAd();

        player.transform.position = new Vector2(0, -3);
        WhiteBG.SetActive(false);

        FindObjectOfType<PlayerController>().DefaultParams();
        FindObjectOfType<WallsContainer>().LaunchWalls();

        pauseButton.SetActive(true);
        gameHasStarted = true;
    }

    /// <summary>
    /// В случае завершения уровня запускаем это
    /// </summary>
    /// <returns></returns>
    IEnumerator Wait()
    {
        FindObjectOfType<GameMenuController>().HideAll();

        yield return new WaitForSeconds(1f);

        Time.timeScale = 0f;

        SecondHomeButton.SetActive(true);
        if (NextLevelButton != null)
            NextLevelButton.SetActive(true);

        switch (Stats.lng)
        {
            case "rus":
                lvlCompleteText.text = "УРОВЕНЬ ПРОЙДЕН!";
                break;
            default:
                lvlCompleteText.text = "LEVEL COMPLETE!";
                break;
        }

        while (true) // Таймер для текста
        {
            lvlCompleteText.color = Color.white;
            yield return new WaitForSecondsRealtime(.5f);
            lvlCompleteText.color = Color.clear;
            yield return new WaitForSecondsRealtime(.5f);
        }
    }

    void CreateAd()
    {
        interstitial = new InterstitialAd(gameOverAD);
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        interstitial.OnAdClosed += HandleOnAdClosed;
    }

    void LoadAd()
    {
        AdRequest request = new AdRequest.Builder().Build();
        //AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("3F742CFF4F3411C6").Build();
        interstitial.LoadAd(request);
    }

    void ShowAd()
    {
        //Debug.Log("So close to... SHOW");
        if (interstitial.IsLoaded() && Stats.numOfDeaths % 2 == 0 && !Stats.AdsRemoved)  // && Не куплена корона
        {
            //Debug.Log("SHOW");
            interstitial.Show();
        }
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //Debug.LogError("closed");
        FindObjectOfType<GameMenuController>().OnContinueButton();
        LoadAd();
    }

    public void HandleOnAdFailedToLoad(object sender, EventArgs args)
    {
        //Debug.LogError("failed");
        LoadAd();
    }
}