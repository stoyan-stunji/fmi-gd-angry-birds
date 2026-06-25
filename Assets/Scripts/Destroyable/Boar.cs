using UnityEngine;

public class Boar :
    MonoBehaviour,
    IDestroyable
{
    public int health = 2;

    public Sprite idleSprite;
    public Sprite hurtSprite;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip collisionSfx;
    [SerializeField] private AudioClip deathSfx;

    private SpriteRenderer sr;
    private AudioSource audioSource;
    public float CurrentHealth => health;
    public float MaxHealth => 2f;

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
            TakeDamage(1f);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= Mathf.RoundToInt(amount);

        if (health == 1)
        {
            sr.sprite = hurtSprite;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    public void ResetState()
    {
        health = 2;
        sr.sprite = idleSprite;
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