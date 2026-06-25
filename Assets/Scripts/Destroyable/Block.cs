using UnityEngine;

public class Block :
    MonoBehaviour,
    IWindAware,
    IDestroyable
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
    private Rigidbody2D rb;

    private float spawnTime;
    public Rigidbody2D Rigidbody => rb;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

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

        if (currentHealth < maxHealth / 2f && damagedSprite != null)
        {
            sr.sprite = damagedSprite;
        }

        if (currentHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void ResetState()
    {
        currentHealth = maxHealth;
        sr.sprite = normalSprite;
    }

    public bool CanBeAffectedByWind()
    {
        return rb != null && !rb.isKinematic;
    }
}