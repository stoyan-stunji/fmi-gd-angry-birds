using UnityEngine;

public class BlueBomb : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionRadius = 2.5f;
    public float explosionForce = 9f;
    public LayerMask affectedLayers;
    public float armDelay = 0.2f;

    [Header("Explosion Visual")]
    public Sprite explosionSprite;
    public float spriteDuration = 0.2f;
    public float spriteScale = 0.25f;

    [Header("Sound")]
    public AudioClip explosionSfx;

    private bool armed = false;
    private bool exploded = false;

    void Start()
    {
        Invoke(nameof(Arm), armDelay);
    }

    void Arm()
    {
        armed = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!armed || exploded) return;
        Explode();
    }

    void Explode()
    {
        exploded = true;

        if (explosionSfx != null)
            AudioSource.PlayClipAtPoint(explosionSfx, transform.position);

        // Explosion sprite
        if (explosionSprite != null)
        {
            GameObject spriteObj = new GameObject("ExplosionSprite");
            spriteObj.transform.position = transform.position;
            spriteObj.transform.localScale = Vector3.one * spriteScale;

            SpriteRenderer sr = spriteObj.AddComponent<SpriteRenderer>();
            sr.sprite = explosionSprite;
            sr.sortingOrder = 100;

            Destroy(spriteObj, spriteDuration);
        }

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
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, 18f);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}