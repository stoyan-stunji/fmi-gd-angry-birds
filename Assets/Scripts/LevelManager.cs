using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject endLevelUI;          
    public GameObject winMessage;          
    public GameObject loseMessage;      

    [Header("Scene Settings")]
    public string nextLevelName;       

    private Boar[] pigs;
    private bool gameEnded = false;

    void Start()
    {
        pigs = FindObjectsOfType<Boar>();
        endLevelUI.SetActive(false);

        if (winMessage != null)
        {
            winMessage.SetActive(false);
        }

        if (loseMessage != null)
        {
            loseMessage.SetActive(false);
        }
    }

    void Update()
    {
        if (gameEnded)
        {
            return;
        }

        pigs = FindObjectsOfType<Boar>();

        if (pigs.Length == 0)
        {
            Win();
        }
    }

    public void Win()
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;

        if (winMessage != null)
        {
            winMessage.SetActive(true);
        }

        if (loseMessage != null)
        {
            loseMessage.SetActive(false);
        }

        ShowEndLevelUI();
    }

    public void Lose()
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;

        if (loseMessage != null)
        {
            loseMessage.SetActive(true);
        }

        if (winMessage != null)
        {
            winMessage.SetActive(false);
        }

            ShowEndLevelUI();
    }

    private void ShowEndLevelUI()
    {
        if (endLevelUI != null)
        {
            endLevelUI.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void NextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogWarning("LevelManager::Next level name NOT set");
        }
    }
}