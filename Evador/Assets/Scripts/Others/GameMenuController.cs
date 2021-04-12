using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseButton, continueButton, homeButton, panel;

    /// <summary>
    /// При нажатии на паузу
    /// </summary>
    public void OnPauseButton()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        continueButton.SetActive(true);
        homeButton.SetActive(true);
        panel.SetActive(true);
    }

    /// <summary>
    /// При возобновлении игры
    /// </summary>
    public void OnContinueButton()
    {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        continueButton.SetActive(false);
        homeButton.SetActive(false);
        panel.SetActive(false);
    }

    /// <summary>
    /// При нажатии на кнопку домой
    /// </summary>
    public void OnHomeButton()
    {
        Time.timeScale = 1f;
        Stats.running = false;
        SceneManager.LoadScene(0);
    }

    public void OnNextLevelButton()
    {
        Stats.currentLevel++;
        Stats.running = true;
        SceneManager.LoadScene(Stats.currentLevel);
    }

    /// <summary>
    /// Метод для прятания всех компонент меню
    /// </summary>
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
        //nextLevel.SetActive(false);
        panel.SetActive(false);

        Time.timeScale = 1f;
    }

    /// <summary>
    /// В случае выхода из приложения ставим паузу автоматически
    /// </summary>
    /// <param name="pause"> Игра на паузе?</param>
    void OnApplicationPause(bool pause)
    {
        if (pause)
           OnPauseButton();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            OnPauseButton();
    }
}