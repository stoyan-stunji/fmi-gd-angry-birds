using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject continueButton;

    void Start()
    {
        continueButton.SetActive(false);
        Invoke(nameof(ShowButton), 5f);
    }

    void ShowButton()
    {
        continueButton.SetActive(true);
    }
}