using UnityEngine;

public class Peacock : BaseBird
{
    public float speedMultiplier = 2f;
    public Sprite idleSprite;
    public Sprite launchedSprite;
    public Sprite powerSprite;

    protected override void Awake()
    {
        base.Awake();
        sr.sprite = idleSprite;
    }

    public override void Launch(Vector2 force)
    {
        base.Launch(force);
        sr.sprite = launchedSprite;
    }

    protected override void ActivatePower()
    {
        powered = true;

        if (powerSprite != null)
        {
            sr.sprite = powerSprite;
        }

        if (rb.velocity.magnitude > 0.1f)
        {
            Vector2 extraVelocity = rb.velocity.normalized * rb.velocity.magnitude * (speedMultiplier - 1f);
            rb.AddForce(extraVelocity, ForceMode2D.Impulse);
        }
    }
}