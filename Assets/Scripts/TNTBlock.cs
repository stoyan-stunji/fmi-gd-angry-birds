using UnityEngine;

public class TNTBlock : Block
{
    [Header("Explosion Settings")]
    public float explosionForce = 5f;      // reduced force
    public float explosionRadius = 2.5f;
    public LayerMask affectedLayers;

    [Header("Explosion Visual")]
    public Sprite explosionSprite;
    public float explosionSpriteDuration = 0.15f;
    public float explosionSpriteScale = 0.2f;

    [Header("Sound")]
    public AudioClip explosionSfx;

    private bool exploded = false;

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);

        if (currentHealth <= 0 && !exploded)
        {
            Explode();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float impact = collision.relativeVelocity.magnitude;

        if (impact > 8f && !exploded)
        {
            Explode();
        }
    }

    void Explode()
    {
        exploded = true;

        // Play sound
        if (explosionSfx != null)
            AudioSource.PlayClipAtPoint(explosionSfx, transform.position);

        // Small explosion sprite
        if (explosionSprite != null)
        {
            GameObject spriteObj = new GameObject("ExplosionSprite");
            spriteObj.transform.position = transform.position;
            spriteObj.transform.localScale = Vector3.one * explosionSpriteScale;

            SpriteRenderer sr = spriteObj.AddComponent<SpriteRenderer>();
            sr.sprite = explosionSprite;
            sr.sortingOrder = 100;

            Destroy(spriteObj, explosionSpriteDuration);
        }

        // Apply explosion
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);

        foreach (Collider2D col in colliders)
        {
            Rigidbody2D rb = col.attachedRigidbody;

            if (rb != null)
            {
                Vector2 direction = rb.position - (Vector2)transform.position;
                float distance = direction.magnitude;

                float forcePercent = 1 - (distance / explosionRadius);
                float finalForce = explosionForce * forcePercent;

                rb.AddForce(direction.normalized * finalForce, ForceMode2D.Impulse);

                // Prevent crazy velocities
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, 15f);
            }

            // Controlled chain reaction
            TNTBlock tnt = col.GetComponent<TNTBlock>();
            if (tnt != null && tnt != this && !tnt.exploded)
            {
                tnt.Invoke("Explode", 0.05f); // delay prevents physics spike
            }

            Block block = col.GetComponent<Block>();
            if (block != null)
            {
                block.TakeDamage(explosionForce);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}