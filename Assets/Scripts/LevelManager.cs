using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject winUI;
    public GameObject loseUI;

    private Pig[] pigs;
    private bool gameEnded = false;

    void Start()
    {
        pigs = FindObjectsOfType<Pig>();

        winUI.SetActive(false);
        loseUI.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        pigs = FindObjectsOfType<Pig>();

        if (pigs.Length == 0)
        {
            Win();
        }
    }

    public void Lose()
    {
        gameEnded = true;
        loseUI.SetActive(true);
    }

    void Win()
    {
        gameEnded = true;
        winUI.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}