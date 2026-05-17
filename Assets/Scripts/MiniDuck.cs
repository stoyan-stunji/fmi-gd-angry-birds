using UnityEngine;

public class MiniDuck : BirdBase
{
    protected override void Awake()
    {
        base.Awake();
        launched = true;
        rb.isKinematic = false;
    }

    public override void ActivatePower()
    {
        powered = true;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}