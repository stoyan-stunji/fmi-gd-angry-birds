using UnityEngine;

public class Chicken : BaseBird
{
    public GameObject eggBombPrefab;

    public Sprite idleSprite;
    public Sprite launchedSprite;
    public Sprite powerSprite;

    public float flyUpForce = 6f;

    protected override void Awake() {
        base.Awake();
        sr.sprite = idleSprite;
    }

    public override void Launch(Vector2 force) {
        base.Launch(force);
        sr.sprite = launchedSprite;
    }

    protected override void ActivatePower() {
        powered = true;
        sr.sprite = powerSprite;
        DropEgg();

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * flyUpForce, ForceMode2D.Impulse);
    }

    void DropEgg() {
        if (eggBombPrefab == null) {
            Debug.LogError("Chicken::Egg Bomb Prefab not assigned");
            return;
        }

        GameObject egg = Instantiate(
            eggBombPrefab,
            transform.position,
            Quaternion.identity
        );

        Rigidbody2D eggRb = egg.GetComponent<Rigidbody2D>();

        if (eggRb != null) {
            eggRb.velocity = new Vector2(rb.velocity.x, -2f);
        }

        if (manager != null && manager.cameraFollow != null) {
            manager.cameraFollow.SetTarget(egg.transform);
        }
    }
}