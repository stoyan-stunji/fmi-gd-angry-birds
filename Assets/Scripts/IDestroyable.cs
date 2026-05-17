using UnityEngine;

public interface IDestroyable
{
    float CurrentHealth { get; }
    float MaxHealth { get; }

    void TakeDamage(float amount);
    void ResetState();
}