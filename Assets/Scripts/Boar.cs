using UnityEngine;

public class Pig : MonoBehaviour
{
    public int health = 2;
    public Sprite idleSprite;
    public Sprite hurtSprite;
    private SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = idleSprite;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        float impact = collision.relativeVelocity.magnitude;
        if (impact > 2f) {
            TakeDamage();
        }
    }

    void TakeDamage() {
        health--;
        if (health == 1) {
            sr.sprite = hurtSprite;
        }
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}