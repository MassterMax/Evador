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

    public float deadY;

    public Vector2 deltaPos;
    public float width, height;
    public bool gameHasStarted = false;

    public void GameOver()
    {
        Stats.numOfDeaths++;

        pauseButton.SetActive(false);

        WhiteBG.SetActive(true);

        FindObjectOfType<PlayerController>().ResetSpeed();
        FindObjectOfType<WallsContainer>().DoDefaultSettings();

        StartCoroutine(Restart());
        StopCoroutine(Restart());
    }

    public void LevelComplete()
    {
        if (Stats.currentLevel == Stats.MaxLevel && Stats.MaxLevel < Stats.numOfLevels)
            Stats.MaxLevel++;

        StartCoroutine(Wait());
        StopCoroutine(Wait());
    }

    void Awake()
    {
        float h = Screen.height;
        float w = Screen.width;

        if (h < w) { Application.Quit(); }
        float orthoSize = finishSR.bounds.size.x * h / w * 0.5f; //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        Camera.main.orthographicSize = orthoSize;                                       //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

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
            SecondHomeButton.SetActive(false);

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
    }

    IEnumerator Wait()
    {
        FindObjectOfType<GameMenuController>().HideAll();

        yield return new WaitForSeconds(1f);

        Time.timeScale = 0f;

        SecondHomeButton.SetActive(true);

        while (true)
        {
            lvlCompleteText.color = Color.white;
            yield return new WaitForSecondsRealtime(.5f);
            lvlCompleteText.color = Color.clear;
            yield return new WaitForSecondsRealtime(.5f);
        }
    }


}
