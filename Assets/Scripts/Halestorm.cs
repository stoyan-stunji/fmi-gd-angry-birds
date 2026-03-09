using UnityEngine;
using System.Collections;

public class Halestorm : MonoBehaviour
{
    [Header("Halestorm Settings")]
    public GameObject debrisPrefab;
    public int debrisPerSpawn = 3;               // how many debris objects spawn each time
    public Vector2 spawnRangeX = new Vector2(-5f, 5f);
    public float spawnHeight = 8f;
    public float minInterval = 5f;
    public float maxInterval = 12f;

    [Header("Sound Settings")]
    public AudioClip debrisSfx;

    [Header("Camera Zoom Settings")]
    public Camera mainCamera;
    public float zoomOutSize = 10f;      // target zoom size when halestorm starts
    public float zoomSpeed = 3f;         // speed of zoom in/out

    private AudioSource audioSource;
    private float originalCamSize;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera != null)
            originalCamSize = mainCamera.orthographicSize;

        StartCoroutine(SpawnDebris());
    }

    IEnumerator SpawnDebris()
    {
        while (true)
        {
            float interval = Random.Range(minInterval, maxInterval);

            // Smooth zoom out at start of this spawn
            if (mainCamera != null)
            {
                float t = 0f;
                float startSize = mainCamera.orthographicSize;
                while (t < 1f)
                {
                    t += Time.deltaTime * zoomSpeed;
                    mainCamera.orthographicSize = Mathf.Lerp(startSize, zoomOutSize, t);
                    yield return null;
                }
            }

            // Spawn multiple debris
            for (int i = 0; i < debrisPerSpawn; i++)
            {
                Vector3 spawnPos = new Vector3(
                    Random.Range(spawnRangeX.x, spawnRangeX.y),
                    spawnHeight + Random.Range(0f, 2f),
                    0
                );

                Instantiate(debrisPrefab, spawnPos, Quaternion.identity);
            }

            // Play sound
            if (debrisSfx != null)
                audioSource.PlayOneShot(debrisSfx);

            yield return new WaitForSeconds(interval);

            // Smooth zoom back to original camera size
            if (mainCamera != null)
            {
                float t = 0f;
                float startSize = mainCamera.orthographicSize;
                while (t < 1f)
                {
                    t += Time.deltaTime * zoomSpeed;
                    mainCamera.orthographicSize = Mathf.Lerp(startSize, originalCamSize, t);
                    yield return null;
                }
            }
        }
    }
}