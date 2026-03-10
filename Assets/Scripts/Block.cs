using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite normalSprite;
    public Sprite damagedSprite;

    [Header("Health Settings")]
    public float maxHealth = 10f;
    public float currentHealth;

    [Header("Impact Settings")]
    public float minImpactToDamage = 3f;
    public float damageMultiplier = 1f;

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
        if (Time.time - spawnTime < 0.3f)
        {
            return;
        }

        float impact = collision.relativeVelocity.magnitude;
        if (impact < minImpactToDamage)
        {
            return;
        }
        TakeDamage(impact * damageMultiplier);
    }

    public virtual void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth < maxHealth / 2 && damagedSprite != null)
        {
            sr.sprite = damagedSprite;
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void ResetBlock()
    {
        currentHealth = maxHealth;
        sr.sprite = normalSprite;
    }
}