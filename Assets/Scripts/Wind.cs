using UnityEngine;

public class ConstantWind : MonoBehaviour
{
    [Header("Wind Settings")]
    public Vector2 windForce = new Vector2(5f, 0f); // strong horizontal wind
    public bool affectBlocks = true;
    public bool affectBirds = true;

    [Header("Sound Settings")]
    public AudioClip windSfx;
    private AudioSource audioSource;

    void Awake()
    {
        // Setup audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        if (windSfx != null)
        {
            audioSource.clip = windSfx;
            audioSource.Play();
        }
    }

    void FixedUpdate()
    {
        // Apply wind to birds
        if (affectBirds)
        {
            BaseBird[] birds = FindObjectsOfType<BaseBird>();
            foreach (BaseBird bird in birds)
            {
                Rigidbody2D rb = bird.GetComponent<Rigidbody2D>();
                if (rb != null && !rb.isKinematic && rb.velocity.magnitude > 0.1f)
                {
                    rb.AddForce(windForce, ForceMode2D.Force);
                }
            }
        }

        // Apply wind to blocks
        if (affectBlocks)
        {
            Rigidbody2D[] blocks = FindObjectsOfType<Rigidbody2D>();
            foreach (Rigidbody2D rb in blocks)
            {
                if (rb != null && !rb.isKinematic)
                    rb.AddForce(windForce, ForceMode2D.Force);
            }
        }
    }
}