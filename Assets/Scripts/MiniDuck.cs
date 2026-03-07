using UnityEngine;

public class MiniDuck : BaseBird
{
    protected override void Awake()
    {
        base.Awake();

        // mini ducks are already flying
        launched = true;
        rb.isKinematic = false;
    }

    protected override void ActivatePower()
    {
        powered = true;
        // mini ducks have no power
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}