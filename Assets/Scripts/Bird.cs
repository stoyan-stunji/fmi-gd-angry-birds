using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite launchedSprite;
    public Sprite powerSprite;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private bool launched = false;
    private bool powered = false;
    private bool waitingForStop = false;

    public float expandForce = 5f;
    public float expandScale = 1.8f;

    private BirdManager manager;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        rb.isKinematic = true;
        sr.sprite = idleSprite;
    }

    public void SetManager(BirdManager m)
    {
        manager = m;
    }

    public void Launch(Vector2 force)
    {
        rb.isKinematic = false;
        rb.AddForce(force, ForceMode2D.Impulse);

        launched = true;
        sr.sprite = launchedSprite;
    }

    void Update()
    {
        if (launched && !powered && Input.GetMouseButtonDown(0))
        {
            PowerUp();
        }

        // Wait for the bird to stop moving
        if (waitingForStop && rb.velocity.magnitude < 0.15f)
        {
            waitingForStop = false;
            StartCoroutine(ReturnNextBird());
        }
    }

    void PowerUp()
    {
        powered = true;

        transform.localScale *= expandScale;
        rb.AddForce(Vector2.up * expandForce, ForceMode2D.Impulse);

        sr.sprite = powerSprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
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