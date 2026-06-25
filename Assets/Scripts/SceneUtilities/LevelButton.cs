using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelNumber;
    public string sceneName;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        bool unlocked = ProgressManager.IsLevelUnlocked(levelNumber);

        button.interactable = unlocked;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }
}