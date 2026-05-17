using UnityEngine;

public interface IBird
{
    bool IsLaunched { get; }
    bool IsPowered { get; }

    Rigidbody2D Rigidbody { get; }

    void Launch(Vector2 force);
    void ActivatePower();
    void SetManager(BirdManager manager);
}