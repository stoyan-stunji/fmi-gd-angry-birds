using UnityEngine;
using UnityEngine.UI;

public class SpriteChangerUI : MonoBehaviour
{
    public Sprite[] sprites;

    [Header("Sound")]
    public AudioClip clickSound;

    private Image imageComponent;
    private AudioSource audioSource;

    private int currentIndex = 0;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();

        if (sprites.Length > 0)
        {
            imageComponent.sprite = sprites[0];
        }
    }

    public void ChangeSprite()
    {
        if (sprites.Length == 0)
        {
            return;
        }

        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        currentIndex++;

        if (currentIndex >= sprites.Length)
        {
            currentIndex = 0;
        }

        imageComponent.sprite = sprites[currentIndex];
    }
}