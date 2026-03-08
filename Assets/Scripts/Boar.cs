using UnityEngine;

public class Boar : MonoBehaviour
{
    public int health = 2;
    public Sprite idleSprite;
    public Sprite hurtSprite;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip collisionSfx;
    [SerializeField] private AudioClip deathSfx;

    private SpriteRenderer sr;
    private AudioSource audioSource;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        sr.sprite = idleSprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float impact = collision.relativeVelocity.magnitude;

        if (impact > 2f)
        {
            PlaySound(collisionSfx);
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        health--;

        if (health == 1)
        {
            sr.sprite = hurtSprite;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlaySound(deathSfx);
        Destroy(gameObject, 0.3f);
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}