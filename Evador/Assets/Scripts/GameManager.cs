using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject bg;
    [SerializeField] GameObject pauseButton;
    [SerializeField] Text lvlCompleteText;
    [SerializeField] GameObject homeButton;
    [SerializeField] GameObject SecondHomeButton;
    Image bgImage;
    public float deadY;

    public Vector2 deltaPos;
    public float width, height;
    public bool gameHasStarted = false;

    public void GameOver()
    {
        Stats.numOfDeaths++;

        pauseButton.SetActive(false);

        bgImage.color = Color.white;

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
        if (homeButton != null)
        {
            deadY = (homeButton.transform.position.y + pauseButton.transform.position.y) / 2;
            Debug.Log(deadY);
        }

        if (!Stats.running)
        {
            Stats.running = true;
            Stats.ReadProgress();
        }

        width = Camera.main.pixelWidth;
        height = Camera.main.pixelHeight;
        deltaPos = Camera.main.ScreenToWorldPoint(new Vector2(width, height));

        bgImage = bg.GetComponent<Image>();

        if (SecondHomeButton != null)
            SecondHomeButton.SetActive(false);
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(2f);

        player.transform.position = new Vector2(0, -3);
        bgImage.color = Color.black;

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
