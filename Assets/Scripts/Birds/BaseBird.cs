using UnityEngine;
using System.Collections;

public abstract class BirdBase :
    MonoBehaviour,
    IBird,
    IWindAware
{
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected AudioSource audioSource;

    [Header("Sound Effects")]
    [SerializeField] protected AudioClip launchSfx;
    [SerializeField] protected AudioClip powerSfx;
    [SerializeField] protected AudioClip collisionSfx;

    protected bool launched;
    protected bool powered;
    protected bool waitingForStop;
    protected bool collisionSoundPlayed;

    protected BirdManager manager;

    public bool IsLaunched => launched;
    public bool IsPowered => powered;
    public Rigidbody2D Rigidbody => rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        rb.isKinematic = true;
    }

    public virtual void SetManager(BirdManager m)
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

        if (waitingForStop && rb.velocity.magnitude < 2f)
        {
            waitingForStop = false;
            StartCoroutine(ReturnNextBird());
        }
    }

    public abstract void ActivatePower();

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!launched)
        {
            return;
        }

        if (!collisionSoundPlayed)
        {
            PlaySound(collisionSfx);
            collisionSoundPlayed = true;
        }
        waitingForStop = true;
    }

    protected virtual IEnumerator ReturnNextBird()
    {
        yield return new WaitForSeconds(2f);
        manager.LoadNextBird();
        Destroy(gameObject);
    }

    public virtual bool CanBeAffectedByWind()
    {
        return rb != null && !rb.isKinematic && rb.velocity.magnitude > 0.1f;
    }

    protected void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}