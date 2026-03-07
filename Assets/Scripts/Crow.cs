using UnityEngine;

public class Crow : BaseBird
{
    public GameObject blueBombPrefab; // 5%
    public GameObject blueRockPrefab;  // 45%
    public GameObject blueStrawPrefab; // 50%

    public float spinSpeed = 100f; // degrees per second
    public float shootForce = 5f;  // initial forward force for the blue object
    public float beakOffset = 0.6f; // how far in front of crow's beak to spawn

    public Sprite idleSprite;
    public Sprite launchedSprite;
    public Sprite powerSprite;

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

    protected override void Update()
    {
        base.Update();

        // Spin while in air
        if (launched)
        {
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        }
    }

    protected override void ActivatePower()
    {
        powered = true;
        sr.sprite = powerSprite;

        ShootFromBeak();
    }

    void ShootFromBeak()
    {
        GameObject prefabToSpawn = null;

        float rand = Random.Range(0f, 1f);

        if (rand < 0.05f)
        {
            prefabToSpawn = blueBombPrefab; // 5%
        }
        else if (rand < 0.50f)
        {
            prefabToSpawn = blueRockPrefab; // 45%
        }
        else
        {
            prefabToSpawn = blueStrawPrefab; // 50%
        }

        if (prefabToSpawn == null)
        {
            Debug.LogError("Blue object prefab not assigned!");
            return;
        }

        // Spawn position at beak
        Vector3 spawnPos = transform.position + transform.up * beakOffset;

        GameObject obj = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();
        if (objRb != null)
        {
            // Set initial velocity: crow's velocity + forward shooting force
            objRb.velocity = rb.velocity; // inherit crow velocity
            objRb.AddForce(transform.up * shootForce, ForceMode2D.Impulse);
        }
    }
}