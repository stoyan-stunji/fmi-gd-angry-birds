using UnityEngine;

public class EggBomb : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionForce = 8f;
    public float explosionRadius = 2f;
    public LayerMask affectedLayers;  // Only objects on these layers are affected
    public float armDelay = 0.2f;     // Delay before the bomb can explode

    private bool armed = false;

    void Start()
    {
        // Delay before bomb can explode (prevents exploding immediately)
        Invoke(nameof(Arm), armDelay);
    }

    void Arm()
    {
        armed = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!armed) return;

        Explode();
    }

    void Explode()
    {
        // Find all colliders in the explosion radius on the affected layers
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);

        foreach (Collider2D col in colliders)
        {
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Push objects away from explosion center
                Vector2 direction = rb.position - (Vector2)transform.position;
                rb.AddForce(direction.normalized * explosionForce, ForceMode2D.Impulse);

                // Optional: If the object is a BaseBlock, apply damage
                Block block = col.GetComponent<Block>();
                if (block != null)
                {
                    // Apply damage proportional to distance or use explosionForce
                    block.TakeDamage(explosionForce);
                }
            }
        }

        // Optional: add explosion particles or sound here

        Destroy(gameObject);
    }

    // Visualize explosion radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}