using UnityEngine;
using UnityEngine.UI;

public class BirdCageBlock :
    Block
{
    [Header("Bird Settings")]
    public Chick chickPrefab;

    private bool addedToRoster = false;

    [Header("UI Images")]
    public Image chickInCage;
    public Image chickOutOfCage;

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
            Debug.LogWarning("BirdCageBlock::Chick prefab NOT assigned");
            return;
        }

        addedToRoster = true;
        BirdManager manager = FindObjectOfType<BirdManager>();

        if (manager != null)
        {
            manager.AddBirdToQueue(chickPrefab);
        }
    }

    private void UpdateChickUI()
    {
        if (chickInCage != null)
        {
            chickInCage.enabled = false;
        }

        if (chickOutOfCage != null)
        {
            chickOutOfCage.enabled = true;
        }
    }
}