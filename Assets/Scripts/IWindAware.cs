using UnityEngine;

public interface IWindAware
{
    Rigidbody2D Rigidbody { get; }
    bool CanBeAffectedByWind();
}