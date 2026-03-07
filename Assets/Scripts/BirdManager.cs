using UnityEngine;
using System.Collections.Generic;

public class BirdManager : MonoBehaviour
{
    public Slingshot slingshot;
    public CameraFollow cameraFollow;

    public List<BaseBird> birdPrefabs;

    public Transform[] queuePositions;

    private List<BaseBird> spawnedBirds = new List<BaseBird>();

    private int currentBird = 0;

    void Start()
    {
        SpawnBirdQueue();
        LoadNextBird();
    }

    void SpawnBirdQueue()
    {
        for (int i = 0; i < birdPrefabs.Count; i++)
        {
            Vector3 pos = queuePositions[Mathf.Min(i, queuePositions.Length - 1)].position;

            BaseBird bird = Instantiate(
                birdPrefabs[i],
                pos,
                Quaternion.identity
            );

            bird.SetManager(this);

            spawnedBirds.Add(bird);
        }
    }

    public void LoadNextBird()
    {
        if (currentBird >= spawnedBirds.Count)
        {
            Debug.Log("No birds left!");
            FindObjectOfType<LevelManager>().Lose();
            return;
        }

        BaseBird bird = spawnedBirds[currentBird];

        bird.transform.position = slingshot.launchPoint.position;

        slingshot.SetBird(bird);

        cameraFollow.SetTarget(bird.transform);

        currentBird++;

        UpdateQueuePositions();
    }

    void UpdateQueuePositions()
    {
        for (int i = currentBird; i < spawnedBirds.Count; i++)
        {
            int queueIndex = i - currentBird;

            if (queueIndex >= queuePositions.Length)
                break;

            spawnedBirds[i].transform.position = queuePositions[queueIndex].position;
        }
    }
}