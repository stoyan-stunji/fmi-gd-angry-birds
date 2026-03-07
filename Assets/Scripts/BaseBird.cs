using UnityEngine;
using System.Collections;

public abstract class BaseBird : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    protected bool launched = false;
    protected bool powered = false;
    protected bool waitingForStop = false;

    protected BirdManager manager;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

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
    }

    protected virtual void Update()
    {
        if (launched && !powered && Input.GetMouseButtonDown(0))
        {
            ActivatePower();
        }

        if (waitingForStop && rb.velocity.magnitude < 0.15f)
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
            waitingForStop = true;
        }
    }

    IEnumerator ReturnNextBird()
    {
        yield return new WaitForSeconds(5f);

        manager.LoadNextBird();

        Destroy(gameObject);
    }
}