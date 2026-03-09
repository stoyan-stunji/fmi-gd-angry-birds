using UnityEngine;

public class PoofEffect : MonoBehaviour
{
    [Header("Poof Settings")]
    public int particleCount = 15;
    public float duration = 0.4f;
    public float startLifetime = 0.35f;
    public float startSpeed = 2f;
    public float startSize = 0.4f;
    public float radius = 0.15f;
    public float floatUpVelocity = 0.5f;

    void Start()
    {
        // Create particle system
        ParticleSystem ps = gameObject.AddComponent<ParticleSystem>();

        // Main settings
        var main = ps.main;
        main.loop = false;
        main.duration = duration;
        main.startLifetime = startLifetime;
        main.startSpeed = startSpeed;
        main.startSize = startSize;
        main.startColor = Color.white;
        main.gravityModifier = 0f;
        main.simulationSpace = ParticleSystemSimulationSpace.World;

        // Emission
        var emission = ps.emission;
        emission.rateOverTime = 0;
        emission.SetBursts(new ParticleSystem.Burst[]
        {
            new ParticleSystem.Burst(0f, (short)particleCount)
        });

        // Shape
        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Circle;
        shape.radius = radius;

        // Size over lifetime
        var sizeOverLifetime = ps.sizeOverLifetime;
        sizeOverLifetime.enabled = true;
        sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1f, AnimationCurve.Linear(0f, 1f, 1f, 0f));

        // Color over lifetime (fade out)
        var colorOverLifetime = ps.colorOverLifetime;
        colorOverLifetime.enabled = true;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0f), new GradientColorKey(Color.white, 1f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(0f, 1f) }
        );
        colorOverLifetime.color = gradient;

        // Velocity over lifetime
        var velocity = ps.velocityOverLifetime;
        velocity.enabled = true;
        velocity.y = floatUpVelocity;

        // Play and destroy after duration
        ps.Play();
        Destroy(gameObject, duration + startLifetime);
    }
}