using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseButton, continueButton, homeButton, panel;

    public void OnPauseButton()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        continueButton.SetActive(true);
        homeButton.SetActive(true);
        panel.SetActive(true);
    }

    public void OnContinueButton()
    {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        continueButton.SetActive(false);
        homeButton.SetActive(false);
        panel.SetActive(false);
    }

    public void OnHomeButton()
    {
        Stats.SaveProgress(Stats.MaxLevel);
        Stats.running = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void HideAll()
    {
        pauseButton.SetActive(false);
        continueButton.SetActive(false);
        homeButton.SetActive(false);
        panel.SetActive(true);
    }

    void Awake()
    {
     if (Screen.height != Screen.safeArea.height)
        {
            Vector3 delta = continueButton.transform.position - homeButton.transform.position;
            continueButton.transform.position = pauseButton.transform.position = homeButton.transform.position;
            homeButton.transform.position -= delta;
        }    
    }

    void Start()
    {
        continueButton.SetActive(false);
        homeButton.SetActive(false);
        panel.SetActive(false);
    }
}
