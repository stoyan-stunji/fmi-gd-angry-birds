using UnityEngine;

public class EggBomb : MonoBehaviour
{
    public float explosionForce = 8f;
    public float explosionRadius = 2f;

    private bool armed = false;

    void Start()
    {
        // allow physics but delay explosion
        Invoke(nameof(Arm), 0.2f);
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
        Collider2D[] objects = Physics2D.OverlapCircleAll(
            transform.position,
            explosionRadius
        );

        foreach (Collider2D obj in objects)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 direction =
                    rb.position - (Vector2)transform.position;

                rb.AddForce(direction.normalized * explosionForce,
                    ForceMode2D.Impulse);
            }
        }

        Destroy(gameObject);
    }
}