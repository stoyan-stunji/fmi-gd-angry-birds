using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TNTBlock : Block
{
    [Header("Explosion Settings")]
    public float explosionForce = 10f;
    public float explosionRadius = 3f;
    public LayerMask affectedLayers;
    public GameObject explosionEffectPrefab; // optional particle effect

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);

        if (currentHealth <= 0)
        {
            Explode();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float impact = collision.relativeVelocity.magnitude;

        // Optional: explode if impact is very strong
        if (impact > 8f)
        {
            Explode();
        }
    }

    void Explode()
    {
        // Spawn explosion effect
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        // Apply force/damage to nearby objects
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);

        foreach (Collider2D col in colliders)
        {
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = rb.position - (Vector2)transform.position;
                rb.AddForce(direction.normalized * explosionForce, ForceMode2D.Impulse);
            }

            // Chain reaction: if another TNT block is nearby, explode it
            TNTBlock tnt = col.GetComponent<TNTBlock>();
            if (tnt != null && tnt != this)
            {
                tnt.Explode();
            }

            // Damage BaseBlocks
            Block block = col.GetComponent<Block>();
            if (block != null)
            {
                block.TakeDamage(explosionForce);
            }
        }

        Destroy(gameObject);
    }

    // Optional: visualize explosion radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}