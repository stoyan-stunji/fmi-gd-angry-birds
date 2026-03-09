using UnityEngine;
using System.Collections;

public class SweepingTornado : MonoBehaviour
{
    [Header("Tornado Movement")]
    public Vector2 startPosition = new Vector2(-8f, 0f);   // starting point of tornado
    public Vector2 endPosition = new Vector2(8f, 0f);      // ending point of tornado
    public float speed = 2f;                                // movement speed across level
    public Vector2 size = new Vector2(3f, 4f);             // width and height of tornado "window"

    [Header("Tornado Force")]
    public float pullStrength = 15f;       // horizontal pull toward center of window
    public float updraftStrength = 20f;    // vertical force
    public bool affectBirds = true;
    public bool affectBlocks = true;
    public bool affectBoars = true;

    [Header("Sound")]
    public AudioClip tornadoSfx;
    private AudioSource audioSource;

    private Vector2 direction;

    void Start()
    {
        transform.position = startPosition;
        direction = (endPosition - startPosition).normalized;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        if (tornadoSfx != null)
        {
            audioSource.clip = tornadoSfx;
            audioSource.Play();
        }
    }

    void FixedUpdate()
    {
        // Move tornado
        transform.position += (Vector3)(direction * speed * Time.fixedDeltaTime);

        // Reverse at boundaries
        if (Vector2.Distance(transform.position, endPosition) < 0.1f)
            direction = (startPosition - endPosition).normalized;
        if (Vector2.Distance(transform.position, startPosition) < 0.1f)
            direction = (endPosition - startPosition).normalized;

        // Apply force to objects inside rectangle
        ApplyTornadoForce();
    }

    void ApplyTornadoForce()
    {
        // Calculate rectangle bounds
        Vector2 min = (Vector2)transform.position - size / 2f;
        Vector2 max = (Vector2)transform.position + size / 2f;

        // Birds
        if (affectBirds)
        {
            BaseBird[] birds = FindObjectsOfType<BaseBird>();
            foreach (BaseBird bird in birds)
            {
                Rigidbody2D rb = bird.GetComponent<Rigidbody2D>();
                if (rb != null && !rb.isKinematic)
                    ApplyForceIfInside(rb, min, max);
            }
        }

        // Blocks
        if (affectBlocks)
        {
            Block[] blocks = FindObjectsOfType<Block>();
            foreach (Block block in blocks)
            {
                Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
                if (rb != null && !rb.isKinematic)
                    ApplyForceIfInside(rb, min, max);
            }
        }

        // Boars
        if (affectBoars)
        {
            Boar[] boars = FindObjectsOfType<Boar>();
            foreach (Boar boar in boars)
            {
                Rigidbody2D rb = boar.GetComponent<Rigidbody2D>();
                if (rb != null)
                    ApplyForceIfInside(rb, min, max);
            }
        }
    }

    void ApplyForceIfInside(Rigidbody2D rb, Vector2 min, Vector2 max)
    {
        Vector2 pos = rb.position;
        if (pos.x > min.x && pos.x < max.x && pos.y > min.y && pos.y < max.y)
        {
            // Pull toward center of rectangle horizontally
            Vector2 center = (min + max) / 2f;
            Vector2 pullDir = new Vector2(center.x - pos.x, 0f).normalized;
            rb.AddForce(pullDir * pullStrength * Time.fixedDeltaTime, ForceMode2D.Force);

            // Add vertical updraft
            rb.AddForce(Vector2.up * updraftStrength * Time.fixedDeltaTime, ForceMode2D.Force);

            // Optional spin effect
            rb.AddTorque(10f * Time.fixedDeltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw rectangle
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, size);
    }
}