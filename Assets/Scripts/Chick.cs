using UnityEngine;

public class Chick : BaseBird
{
    public Sprite idleSprite;
    public Sprite launchedSprite;

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
        // Does nothing by design
        powered = true;
    }
}