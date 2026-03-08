using UnityEngine;
using UnityEngine.UI; // Important for UI Images

public class SpriteChangerUI : MonoBehaviour
{
    public Sprite[] sprites;
    private Image imageComponent;
    private int currentIndex = 0;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        if (sprites.Length > 0)
        {
            imageComponent.sprite = sprites[0];
        }
    }

    // For UI, we use an EventTrigger or Button, not OnMouseDown
    public void ChangeSprite()
    {
        if (sprites.Length == 0) return;

        currentIndex++;
        if (currentIndex >= sprites.Length)
        {
            currentIndex = 0;
        }

        imageComponent.sprite = sprites[currentIndex];
    }
}