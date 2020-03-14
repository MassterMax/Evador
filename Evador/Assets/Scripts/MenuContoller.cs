using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuContoller : MonoBehaviour
{
    [SerializeField] Text levelText;

    string[] levels = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };

    int maxLevel;
    int selectedLevel = 1;

    public int SelectedLevel { get => selectedLevel; set { if (0 < value && value <= maxLevel) selectedLevel = value; } }

    public void OnCloseButton()
    {
        Application.Quit();
    }

    public void OnLeftButton()
    {
        SelectedLevel--;
        levelText.text = levels[selectedLevel - 1];
    }

    public void OnRightButton()
    {
        SelectedLevel++;
        levelText.text = levels[selectedLevel - 1];
    }

    public void OnPlayButton()
    {
        FindObjectOfType<DummyPlayerContoller>().canMove = true;
        FindObjectOfType<GameManager>().currentLevel = SelectedLevel;
        Invoke("LevelLoad", 3f);
        gameObject.SetActive(false);
    }

    void LevelLoad()
    {
        SceneManager.LoadScene(SelectedLevel);
    }

    void Start()
    {
        maxLevel = FindObjectOfType<GameManager>().maxLevel;
    }
}
