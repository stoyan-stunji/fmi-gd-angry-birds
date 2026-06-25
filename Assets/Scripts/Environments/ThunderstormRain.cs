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
    public Image flashImage;
    public float flashDuration = 0.15f;

    private AudioSource audioSource;
    private Material flashMaterial;

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
        {
            flashMaterial = flashImage.material;

            if (flashMaterial != null)
            {
                flashMaterial.SetFloat("_FlashStrength", 0f);
            }
        }

        StartCoroutine(RainRoutine());
        StartCoroutine(ThunderRoutine());
    }

    IEnumerator RainRoutine()
    {
        while (true)
        {
            SpawnRainDrop();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnRainDrop()
    {
        Vector3 spawnPos = new Vector3(Random.Range(spawnRangeX.x, spawnRangeX.y), spawnHeight, 0);
        GameObject drop = Instantiate(rainDropPrefab, spawnPos, Quaternion.identity);
        Rigidbody2D rb = drop.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = new Vector2(0f, Random.Range(fallSpeedRange.x, fallSpeedRange.y));
        }
    }

    IEnumerator ThunderRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(thunderInterval * 0.5f, thunderInterval * 1.5f));
            Vector3 strikePos = GetThunderStrikePosition();

            SpawnThunderVisual(strikePos);
            PlayThunderSound(strikePos);

            if (flashImage != null)
            {
                StartCoroutine(FlashScreen());
            }

            ApplyThunderForceToBlocks(strikePos);
        }
    }

    private Vector3 GetThunderStrikePosition()
    {
        float strikeX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        return new Vector3(strikeX, 0f, 0f);
    }

    private void SpawnThunderVisual(Vector3 position)
    {
        if (thunderPrefab != null)
        {
            GameObject thunderObj = Instantiate(thunderPrefab, position, Quaternion.identity);
            Destroy(thunderObj, thunderDuration);
        }
    }

    private void PlayThunderSound(Vector3 position)
    {
        if (thunderSfx != null)
        {
            AudioSource.PlayClipAtPoint(thunderSfx, position);
        }
    }

    private void ApplyThunderForceToBlocks(Vector3 strikePos)
    {
        Block[] blocks = FindObjectsOfType<Block>();
        foreach (Block block in blocks)
        {
            Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float distance = Vector2.Distance(rb.position, strikePos);
                if (distance < thunderRadius)
                {
                    Vector2 force = new Vector2(Random.Range(-thunderForce, thunderForce), thunderForce);
                    rb.AddForce(force, ForceMode2D.Impulse);
                }
            }
        }
    }

    IEnumerator FlashScreen()
    {
        yield return SingleFlash(1f, flashDuration * 0.4f);
        yield return new WaitForSeconds(0.03f);
        yield return SingleFlash(0.6f, flashDuration * 0.6f);
    }

    IEnumerator SingleFlash(float maxStrength, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float strength;
            if (t < duration * 0.25f)
            {
                strength = Mathf.Lerp(0f, maxStrength, t / (duration * 0.25f));
            }
            else
            {
                strength = Mathf.Lerp(maxStrength, 0f, (t - duration * 0.25f) / (duration * 0.75f));
            }

            if (flashMaterial != null)
            {
                flashMaterial.SetFloat("_FlashStrength", strength);
            }
            yield return null;
        }

        if (flashMaterial != null)
        {
            flashMaterial.SetFloat("_FlashStrength", 0f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3((spawnRangeX.x + spawnRangeX.y) / 2, 0f, 0f), thunderRadius);
    }
}