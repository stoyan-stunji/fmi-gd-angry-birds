using UnityEngine;
using UnityEngine.UI;

public class BirdCageBlock : Block
{
    [Header("Bird Settings")]
    public DoNothingBird chickPrefab;  // Assign your DoNothingBird prefab here
    private bool addedToRoster = false;  // Prevent adding multiple times

    [Header("UI Images")]
    public Image chickInCage;       // Assign the UI image of the chick in the cage
    public Image chickOutOfCage;    // Assign the UI image of the chick out of the cage

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);

        if (currentHealth <= 0 && !addedToRoster)
        {
            AddChickToRoster();
            UpdateChickUI();
        }
    }

    private void AddChickToRoster()
    {
        if (chickPrefab == null)
        {
            Debug.LogWarning("Chick prefab not assigned to BirdCageBlock!");
            return;
        }

        addedToRoster = true;

        BirdManager manager = FindObjectOfType<BirdManager>();

        if (manager != null)
        {
            manager.AddBirdToQueue(chickPrefab);
            Debug.Log("Chick added to BirdManager queue!");
        }
    }

    private void UpdateChickUI()
    {
        if (chickInCage != null)
            chickInCage.enabled = false;   // Hide chick in cage

        if (chickOutOfCage != null)
            chickOutOfCage.enabled = true; // Show chick out of cage
    }
}