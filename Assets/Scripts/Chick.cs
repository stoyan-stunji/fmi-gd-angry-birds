using UnityEngine;

public class DoNothingBird : BaseBird
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
        powered = true;
        // Does nothing when clicked
    }
}