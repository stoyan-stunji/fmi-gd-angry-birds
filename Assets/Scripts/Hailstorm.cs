using UnityEngine;
using System.Collections;

public class Hailstorm : MonoBehaviour
{
    [Header("Hailstorm Settings")]
    public GameObject hailPrefab;
    public int hailPerSpawn = 3;
    public Vector2 spawnRangeX = new Vector2(-5f, 5f);
    public float spawnHeight = 8f;
    public float minInterval = 5f;
    public float maxInterval = 12f;

    [Header("Sound Settings")]
    public AudioClip debrisSfx;

    [Header("Camera Zoom Settings")]
    public Camera mainCamera;
    public float zoomOutSize = 10f;
    public float zoomSpeed = 3f;

    private AudioSource audioSource;
    private float originalCamSize;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera != null)
        {
            originalCamSize = mainCamera.orthographicSize;
        }

        StartCoroutine(SpawnDebris());
    }

    IEnumerator SpawnDebris()
    {
        while (true)
        {
            float interval = Random.Range(minInterval, maxInterval);
            yield return ZoomCamera(mainCamera.orthographicSize, zoomOutSize);
            SpawnMultipleDebris();
            PlayDebrisSound();
            yield return new WaitForSeconds(interval);
            yield return ZoomCamera(mainCamera.orthographicSize, originalCamSize);
        }
    }

    private void SpawnMultipleDebris()
    {
        for (int i = 0; i < hailPerSpawn; i++)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(spawnRangeX.x, spawnRangeX.y),
                spawnHeight + Random.Range(0f, 2f),
                0
            );

            Instantiate(hailPrefab, spawnPos, Quaternion.identity);
        }
    }

    private void PlayDebrisSound()
    {
        if (debrisSfx != null)
        {
            audioSource.PlayOneShot(debrisSfx);
        }
    }

    private IEnumerator ZoomCamera(float startSize, float targetSize)
    {
        if (mainCamera == null)
        {
            yield break;
        }

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * zoomSpeed;
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null;
        }
        mainCamera.orthographicSize = targetSize;
    }
}