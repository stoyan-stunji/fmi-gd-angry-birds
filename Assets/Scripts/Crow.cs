using UnityEngine;

public class Crow : BaseBird
{
    public GameObject blueBombPrefab;  // 5%
    public GameObject blueRockPrefab;  // 45%
    public GameObject blueStrawPrefab; // 50%

    public float spinSpeed = 100f;
    public float shootForce = 5f;
    public float beakOffset = 0.6f;

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
            prefabToSpawn = blueBombPrefab;
        }
        else if (rand < 0.50f)
        {
            prefabToSpawn = blueRockPrefab;
        }
        else
        {
            prefabToSpawn = blueStrawPrefab;
        }

        if (prefabToSpawn == null)
        {
            Debug.LogError("Crow::Blue object prefab not assigned!");
            return;
        }

        Vector3 spawnPos = transform.position + transform.up * beakOffset;

        GameObject obj = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();
        if (objRb != null)
        {
            objRb.velocity = rb.velocity; // inherit Crow velocity
            objRb.AddForce(transform.up * shootForce, ForceMode2D.Impulse);
        }
    }
}