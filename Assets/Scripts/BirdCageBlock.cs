using UnityEngine;

public class BirdCageBlock : Block
{
    [Header("Bird Settings")]
    public DoNothingBird chickPrefab;  // Assign your DoNothingBird prefab here
    private bool addedToRoster = false;  // Prevent adding multiple times

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);

        if (currentHealth <= 0 && !addedToRoster)
        {
            AddChickToRoster();
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

        // Find the BirdManager in the scene
        BirdManager manager = FindObjectOfType<BirdManager>();
        if (manager != null)
        {
            // Add the prefab to the BirdManager's list
            manager.birdPrefabs.Add(chickPrefab);
            Debug.Log("Chick added to BirdManager roster!");
        }
    }
}