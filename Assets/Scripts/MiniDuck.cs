using UnityEngine;

public class MiniDuck : BaseBird
{
    protected override void Awake()
    {
        base.Awake();

        launched = true;
        rb.isKinematic = false;
    }

    protected override void ActivatePower()
    {
        powered = true;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}