using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseButton, continueButton, homeButton;

    public void OnPauseButton()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        continueButton.SetActive(true);
        homeButton.SetActive(true);
    }

    public void OnContinueButton()
    {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        continueButton.SetActive(false);
        homeButton.SetActive(false);
    }

    public void OnHomeButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    void Start()
    {
        continueButton.SetActive(false);
        homeButton.SetActive(false);
    }
}
