using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public string nextScene;

    public void LoadScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}