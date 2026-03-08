using UnityEngine;

public class BlueBomb : MonoBehaviour
{
    public float explosionRadius = 2f;     
    public float explosionForce = 8f;     
    public LayerMask affectedLayers;       
    public float armDelay = 0.2f;          
    private bool armed = false;

    void Start() {
        Invoke(nameof(Arm), armDelay);
    }

    void Arm() {
        armed = true;
    }

    void OnCollisionEnter2D(Collision2D collision) {   
        if (!armed) {
            return;
        }
        Explode();
    }

    void Explode() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers);

        foreach (Collider2D col in colliders) {
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            if (rb != null) {
                Vector2 direction = rb.position - (Vector2)transform.position;
                rb.AddForce(direction.normalized * explosionForce, ForceMode2D.Impulse);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}