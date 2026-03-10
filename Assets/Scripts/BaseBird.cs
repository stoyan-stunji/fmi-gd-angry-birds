using UnityEngine;
using System.Collections;

public abstract class BaseBird : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected AudioSource audioSource;

    [Header("Sound Effects")]
    [SerializeField] protected AudioClip launchSfx;
    [SerializeField] protected AudioClip powerSfx;
    [SerializeField] protected AudioClip collisionSfx;

    protected bool launched = false;
    protected bool powered = false;
    protected bool waitingForStop = false;

    protected bool collisionSoundPlayed = false;

    protected BirdManager manager;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        rb.isKinematic = true;
    }

    public void SetManager(BirdManager m)
    {
        manager = m;
    }

    public virtual void Launch(Vector2 force)
    {
        rb.isKinematic = false;
        rb.AddForce(force, ForceMode2D.Impulse);
        launched = true;

        PlaySound(launchSfx);
    }

    protected virtual void Update()
    {
        if (launched && !powered && Input.GetMouseButtonDown(0))
        {
            ActivatePower();
            PlaySound(powerSfx);
        }

        if (waitingForStop && rb.velocity.magnitude < 1f)
        {
            waitingForStop = false;
            StartCoroutine(ReturnNextBird());
        }
    }

    protected abstract void ActivatePower();

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (launched)
        {
            if (!collisionSoundPlayed)
            {
                PlaySound(collisionSfx);
                collisionSoundPlayed = true;
            }

            waitingForStop = true;
        }
    }

    IEnumerator ReturnNextBird()
    {
        yield return new WaitForSeconds(2f);


        manager.LoadNextBird();
        Destroy(gameObject);
    }

    protected void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}