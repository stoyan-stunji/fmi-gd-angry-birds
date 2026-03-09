using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject endLevelUI;           // panel that contains all buttons
    public GameObject winMessage;           // optional text/object for win
    public GameObject loseMessage;          // optional text/object for lose

    [Header("Scene Settings")]
    public string nextLevelName;            // set next level name in Inspector

    private Boar[] pigs;
    private bool gameEnded = false;

    void Start()
    {
        pigs = FindObjectsOfType<Boar>();
        endLevelUI.SetActive(false);

        if (winMessage != null) winMessage.SetActive(false);
        if (loseMessage != null) loseMessage.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

        pigs = FindObjectsOfType<Boar>();

        if (pigs.Length == 0)
        {
            Win();
        }

        // Optional: Lose condition can be triggered elsewhere, e.g. no birds left
    }

    public void Win()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (winMessage != null) winMessage.SetActive(true);
        if (loseMessage != null) loseMessage.SetActive(false);

        ShowEndLevelUI();
    }

    public void Lose()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (loseMessage != null) loseMessage.SetActive(true);
        if (winMessage != null) winMessage.SetActive(false);

        ShowEndLevelUI();
    }

    private void ShowEndLevelUI()
    {
        if (endLevelUI != null)
            endLevelUI.SetActive(true);
    }

    // Button Functions
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("SelectLevel"); // make sure this scene exists
    }

    public void NextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
            SceneManager.LoadScene(nextLevelName);
        else
            Debug.LogWarning("Next level name not set!");
    }
}