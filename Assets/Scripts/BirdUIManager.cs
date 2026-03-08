using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BirdUIManager : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public Transform iconContainer; // UI panel that holds icons
    public GameObject iconPrefab;   // UI Image prefab

    [Header("Bird Sprites")]
    public List<Sprite> birdSprites;

    private List<GameObject> icons = new List<GameObject>();

    public void SetupUI(int birdCount)
    {
        for (int i = 0; i < birdCount; i++)
        {
            GameObject icon = Instantiate(iconPrefab, iconContainer);

            Image img = icon.GetComponent<Image>();

            if (i < birdSprites.Count)
                img.sprite = birdSprites[i];

            icons.Add(icon);
        }
    }

    public void UseBird()
    {
        if (icons.Count == 0) return;

        Destroy(icons[0]);
        icons.RemoveAt(0);
    }
}