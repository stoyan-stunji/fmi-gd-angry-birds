using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject winUI;
    public GameObject loseUI;

    [Header("Scene Settings")]
    public string nextLevelName;        // <-- Set next level name in Inspector
    public float delayBeforeNextLevel = 2f;
    public float delayBeforeRestart = 2f;

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
        if (gameEnded) return;

        gameEnded = true;
        loseUI.SetActive(true);
        StartCoroutine(RestartLevelAfterDelay());
    }

    void Win()
    {
        if (gameEnded) return;

        gameEnded = true;
        winUI.SetActive(true);
        StartCoroutine(LoadNextLevelAfterDelay());
    }

    IEnumerator RestartLevelAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeRestart);
        RestartLevel();
    }

    IEnumerator LoadNextLevelAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeNextLevel);

        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogWarning("Next level name not set! Reloading current level instead.");
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}