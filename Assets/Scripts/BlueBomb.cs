using UnityEngine;

public class BlueBomb : MonoBehaviour
{
    public float explosionRadius = 2f;     // How far the explosion affects
    public float explosionForce = 8f;      // Force applied to nearby objects
    public LayerMask affectedLayers;       // Layers that can be pushed/damaged
    public float armDelay = 0.2f;          // Seconds before the bomb can explode

    private bool armed = false;

    void Start()
    {
        // Delay before the bomb can explode
        Invoke(nameof(Arm), armDelay);
    }

    void Arm()
    {
        armed = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!armed) return; // ignore collisions until armed

        Explode();
    }

    void Explode()
    {
        // Find all colliders in the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);

        foreach (Collider2D col in colliders)
        {
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = rb.position - (Vector2)transform.position;
                rb.AddForce(direction.normalized * explosionForce, ForceMode2D.Impulse);
            }
        }

        // Optional: play explosion effect here (particles, sound)
        Destroy(gameObject);
    }

    // Optional: visualize explosion radius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}