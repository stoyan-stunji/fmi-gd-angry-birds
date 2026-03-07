using UnityEngine;

public class Duck : BaseBird
{
    public GameObject miniDuckPrefab;

    public Sprite idleSprite;
    public Sprite launchedSprite;
    public Sprite powerSprite;

    public int splitCount = 5;

    protected override void Awake()
    {
        base.Awake();
        sr.sprite = idleSprite;
    }

    public override void Launch(Vector2 force)
    {
        base.Launch(force);
        sr.sprite = launchedSprite;
    }

    protected override void ActivatePower()
    {
        powered = true;
        sr.sprite = powerSprite;

        GameObject firstDuck = null;

        for (int i = 0; i < splitCount; i++)
        {
            GameObject mini = Instantiate(
                miniDuckPrefab,
                transform.position,
                Quaternion.identity
            );

            if (i == 0)
                firstDuck = mini;

            Rigidbody2D miniRb = mini.GetComponent<Rigidbody2D>();

            miniRb.isKinematic = false;

            Vector2 spread = new Vector2(
                Random.Range(-2f, 2f),
                Random.Range(0.5f, 2f)
            );

            miniRb.velocity = rb.velocity + spread;

            BaseBird miniBird = mini.GetComponent<BaseBird>();
            miniBird.SetManager(manager);
        }

        // move camera to one of the mini ducks
        if (firstDuck != null)
        {
            manager.cameraFollow.SetTarget(firstDuck.transform);
        }

        Destroy(gameObject);
    }
}