using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite normalSprite;
    public Sprite damagedSprite;

    [Header("Health Settings")]
    public float maxHealth = 10f;   // Total health of the block
    public float currentHealth;

    [Header("Impact Settings")]
    public float minImpactToDamage = 3f; // Minimum collision velocity to cause damage
    public float damageMultiplier = 1f;  // Multiply the impact to scale damage

    private SpriteRenderer sr;
    private float spawnTime;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = normalSprite;

        currentHealth = maxHealth;
        spawnTime = Time.time;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore collisions immediately after spawning
        if (Time.time - spawnTime < 0.3f)
            return;

        float impact = collision.relativeVelocity.magnitude;

        if (impact < minImpactToDamage)
            return;

        TakeDamage(impact * damageMultiplier);
    }

    /// <summary>
    /// Apply damage to the block
    /// </summary>
    /// <param name="damageAmount"></param>
    public virtual void TakeDamage(float damageAmount)  // <-- mark as virtual
    {
        currentHealth -= damageAmount;

        // Change sprite if damaged but still alive
        if (currentHealth < maxHealth / 2 && damagedSprite != null)
        {
            sr.sprite = damagedSprite;
        }

        // Destroy block if health <= 0
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Optional: reset the block to full health
    /// </summary>
    public void ResetBlock()
    {
        currentHealth = maxHealth;
        sr.sprite = normalSprite;
    }
}