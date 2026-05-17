using UnityEngine;

public class Pigeon : BirdBase
{
    public Sprite idleSprite;
    public Sprite launchedSprite;
    public Sprite powerSprite;

    public float expandForce = 5f;
    public float expandScale = 1.8f;

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

    public override void ActivatePower()
    {
        powered = true;
        transform.localScale *= expandScale;
        rb.AddForce(Vector2.up * expandForce, ForceMode2D.Impulse);
        sr.sprite = powerSprite;
    }
}