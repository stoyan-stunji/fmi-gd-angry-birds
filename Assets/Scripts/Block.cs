using UnityEngine;

public class Block : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite damagedSprite;

    private SpriteRenderer sr;

    private bool damaged = false;
    private float spawnTime;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = normalSprite;

        spawnTime = Time.time;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time - spawnTime < 0.3f)
            return;

        float impact = collision.relativeVelocity.magnitude;

        if (impact > 3f && !damaged)
        {
            damaged = true;
            sr.sprite = damagedSprite;
        }
    }
}