using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

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

    public int shardNumber;
    public bool shardCollected;

    public float deadY;

    public Vector2 deltaPos;
    public float width, height;
    public bool gameHasStarted = false;

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

    public void Teleport()
    {
        player.transform.position = new Vector2(0, -3);
        FindObjectOfType<WallsContainer>().DoDefaultSettings();
    }

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
        float h = Screen.height;
        float w = Screen.width;

        if (h < w) { Application.Quit(); }


        float orthoSize = finishSR.bounds.size.x * h / w * 0.5f;
        //Debug.Log(orthoSize + " " + Camera.main.orthographicSize);
        float k = Camera.main.orthographicSize / orthoSize;
        Camera.main.orthographicSize = orthoSize;
        if (playBUTTON != null)
            playBUTTON.transform.localScale *= k;


        if (homeButton != null)
        {
            deadY = (homeButton.transform.position.y + pauseButton.transform.position.y) / 2;
            //Debug.Log(deadY);
        }

        if (!Stats.running)
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
            gameHasStarted = true;
        }

    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(2f);

        player.transform.position = new Vector2(0, -3);
        WhiteBG.SetActive(false);

        //yield return new WaitForSeconds(1f);

        FindObjectOfType<PlayerController>().DefaultParams();
        FindObjectOfType<WallsContainer>().LaunchWalls();

        pauseButton.SetActive(true);
        gameHasStarted = true;
    }

    IEnumerator Wait()
    {
        FindObjectOfType<GameMenuController>().HideAll();

        yield return new WaitForSeconds(1f);

        Time.timeScale = 0f;

        SecondHomeButton.SetActive(true);

        while (true) // таймер для текста
        {
            lvlCompleteText.color = Color.white;
            yield return new WaitForSecondsRealtime(.5f);
            lvlCompleteText.color = Color.clear;
            yield return new WaitForSecondsRealtime(.5f);
        }
    }


}
