using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ThunderstormRain : MonoBehaviour
{
    [Header("Rain Settings")]
    public GameObject rainDropPrefab;
    public float spawnInterval = 0.1f;
    public Vector2 spawnRangeX = new Vector2(-8f, 8f);
    public float spawnHeight = 8f;
    public Vector2 fallSpeedRange = new Vector2(5f, 10f);

    [Header("Thunder Settings")]
    public GameObject thunderPrefab;
    public AudioClip thunderSfx;
    public float thunderInterval = 5f;
    public float thunderForce = 30f;
    public float thunderRadius = 2f;
    public float thunderDuration = 0.3f;

    [Header("Sound Settings")]
    public AudioClip rainSfx;

    [Header("Thunder Flash Settings")]
    public Image flashImage;           // UI Image covering full screen (white)
    public float flashDuration = 0.1f; // how fast it fades in/out

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        if (rainSfx != null)
        {
            audioSource.clip = rainSfx;
            audioSource.Play();
        }

        if (flashImage != null)
            flashImage.color = new Color(1f, 1f, 1f, 0f); // make transparent at start

        StartCoroutine(RainRoutine());
        StartCoroutine(ThunderRoutine());
    }

    IEnumerator RainRoutine()
    {
        while (true)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(spawnRangeX.x, spawnRangeX.y),
                spawnHeight,
                0
            );

            GameObject drop = Instantiate(rainDropPrefab, spawnPos, Quaternion.identity);

            Rigidbody2D rb = drop.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = new Vector2(0f, -Random.Range(fallSpeedRange.x, fallSpeedRange.y));

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator ThunderRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(thunderInterval * 0.5f, thunderInterval * 1.5f));

            float strikeX = Random.Range(spawnRangeX.x, spawnRangeX.y);
            Vector3 strikePos = new Vector3(strikeX, 0f, 0f);

            // Spawn thunder visual
            if (thunderPrefab != null)
            {
                GameObject thunderObj = Instantiate(thunderPrefab, strikePos, Quaternion.identity);
                Destroy(thunderObj, thunderDuration);
            }

            // Thunder sound
            if (thunderSfx != null)
                AudioSource.PlayClipAtPoint(thunderSfx, strikePos);

            // Flash screen white
            if (flashImage != null)
                StartCoroutine(FlashScreen());

            // Affect blocks in radius
            Block[] blocks = FindObjectsOfType<Block>();
            foreach (Block block in blocks)
            {
                Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    float distance = Vector2.Distance(rb.position, strikePos);
                    if (distance < thunderRadius)
                    {
                        Vector2 force = new Vector2(
                            Random.Range(-thunderForce, thunderForce),
                            thunderForce
                        );
                        rb.AddForce(force, ForceMode2D.Impulse);
                        // Optional: destroy block instantly
                        // block.TakeDamage(999f);
                    }
                }
            }
        }
    }

    IEnumerator FlashScreen()
    {
        float halfDuration = flashDuration / 2f;
        // Fade in
        for (float t = 0; t < halfDuration; t += Time.deltaTime)
        {
            flashImage.color = new Color(1f, 1f, 1f, t / halfDuration);
            yield return null;
        }
        flashImage.color = Color.white;

        // Fade out
        for (float t = 0; t < halfDuration; t += Time.deltaTime)
        {
            flashImage.color = new Color(1f, 1f, 1f, 1f - t / halfDuration);
            yield return null;
        }
        flashImage.color = new Color(1f, 1f, 1f, 0f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3((spawnRangeX.x + spawnRangeX.y) / 2, 0f, 0f), thunderRadius);
    }
}