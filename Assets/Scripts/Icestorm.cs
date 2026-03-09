using UnityEngine;
using System.Collections;

public class FallingDebris : MonoBehaviour
{
    [Header("Debris Settings")]
    public GameObject debrisPrefab;
    public int debrisPerSpawn = 3;               // how many debris objects spawn each time
    public Vector2 spawnRangeX = new Vector2(-5f, 5f);
    public float spawnHeight = 8f;
    public float minInterval = 5f;
    public float maxInterval = 12f;

    [Header("Sound Settings")]
    public AudioClip debrisSfx;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        StartCoroutine(SpawnDebris());
    }

    IEnumerator SpawnDebris()
    {
        while (true)
        {
            float interval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(interval);

            // Spawn multiple debris
            for (int i = 0; i < debrisPerSpawn; i++)
            {
                Vector3 spawnPos = new Vector3(
                    Random.Range(spawnRangeX.x, spawnRangeX.y),
                    spawnHeight + Random.Range(0f, 2f), // slight height variation
                    0
                );

                Instantiate(debrisPrefab, spawnPos, Quaternion.identity);
            }

            // Play sound
            if (debrisSfx != null)
                audioSource.PlayOneShot(debrisSfx);
        }
    }
}