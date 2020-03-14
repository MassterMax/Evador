using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuContoller : MonoBehaviour
{
    private int selectedLvl = 1;

    public void OnCloseButton()
    {
        Application.Quit();
    }

    public void OnPlayButton()
    {
        FindObjectOfType<DummyPlayerContoller>().canMove = true;
        Invoke("LevelLoad", 3f);
        gameObject.SetActive(false);
    }

    void LevelLoad()
    {
        SceneManager.LoadScene(selectedLvl);
    }
}
