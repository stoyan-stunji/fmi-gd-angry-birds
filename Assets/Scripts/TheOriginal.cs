using UnityEngine;
using System.Collections;

public class TheOriginal : BaseBird
{
    [Header("Sprites")]
    public Sprite idleSprite;
    public Sprite launchedSprite;
    public Sprite powerSprite;

    [Header("Drop Strike Power")]
    public float impactRadius = 3.5f;
    public float impactForce = 100f;
    public float groundCheckDistance = 0.2f;

    [Header("Audio")]
    public AudioClip hearingScreamSfx;

    private Material originalMaterial;

    protected override void Awake()
    {
        base.Awake();

        if (idleSprite != null)
        {
            sr.sprite = idleSprite;
        }
        originalMaterial = sr.material;
    }

    public override void Launch(Vector2 force)
    {
        base.Launch(force);

        if (launchedSprite != null)
        {
            sr.sprite = launchedSprite;
        }
    }

    protected override void ActivatePower()
    {
        if (powered)
        {
            return;
        }

        powered = true;

        if (powerSprite != null)
        {
            sr.sprite = powerSprite;
        }

        if (hearingScreamSfx != null)
        {
            PlaySound(hearingScreamSfx);
        }

        rb.velocity = Vector2.zero;
        StartCoroutine(DropStrike());
    }

    IEnumerator DropStrike()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;

        yield return new WaitForSeconds(0.4f);

        float lockedX = transform.position.x;
        rb.gravityScale = 6f;

        while (true)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);

            transform.position = new Vector3(
                lockedX,
                transform.position.y,
                transform.position.z
            );

            if (rb.velocity.y < -12f && IsGrounded())
            {
                DoImpactDamage();
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        manager.LoadNextBird();
        Destroy(gameObject);
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(
            transform.position,
            Vector2.down,
            groundCheckDistance,
            LayerMask.GetMask("Ground")
        );
    }

    void DoImpactDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            impactRadius
        );

        foreach (Collider2D hit in hits)
        {
            Rigidbody2D targetRb = hit.attachedRigidbody;
            if (targetRb == null || targetRb == rb)
            {
                continue;
            }

            Vector2 dir = (targetRb.position - (Vector2)transform.position).normalized;

            targetRb.AddForce(dir * impactForce, ForceMode2D.Impulse);

            hit.SendMessage(
                "TakeDamage",
                impactForce,
                SendMessageOptions.DontRequireReceiver
            );
        }

        CameraShake();
    }

    void CameraShake()
    {
        Camera cam = Camera.main;
        if (cam == null)
        {
            return;
        }
        StartCoroutine(ShakeRoutine(cam));
    }

    IEnumerator ShakeRoutine(Camera cam)
    {
        Vector3 start = cam.transform.position;
        float t = 0f;

        while (t < 0.25f)
        {
            t += Time.deltaTime;
            cam.transform.position = start + (Vector3)Random.insideUnitCircle * 0.15f;
            yield return null;
        }

        cam.transform.position = start;
    }
}