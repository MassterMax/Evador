using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float onEndDelay = 2f;
    private static GameManager instance;
    public Vector2 deltaPos;
    public float width, height;
    public bool gameHasStarted = false;
    bool gameHasEnded = false;

    public void GameOver()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            //Time.timeScale = 0f;

            Invoke("ShowGameOverScreen", onEndDelay);
        }
    }

    void ShowGameOverScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Условный рестарт.
        gameHasEnded = false;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        width = Camera.main.pixelWidth;
        height = Camera.main.pixelHeight;
        deltaPos = Camera.main.ScreenToWorldPoint(new Vector2(width, height));
    }
}
