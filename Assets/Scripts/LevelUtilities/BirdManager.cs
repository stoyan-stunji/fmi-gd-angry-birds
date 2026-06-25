using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BirdManager : MonoBehaviour
{
    public Slingshot slingshot;
    public CameraFollow cameraFollow;

    public List<BirdBase> birdPrefabs;
    public Transform[] queuePositions;

    [Header("UI")]
    public Image[] birdIcons;

    private List<IBird> spawnedBirds = new List<IBird>();
    private int currentBird = 0;

    void Start()
    {
        SpawnBirdQueue();
        LoadNextBird();
        UpdateBirdUI();
    }

    public void SpawnBirdQueue()
    {
        for (int i = 0; i < birdPrefabs.Count; i++)
        {
            Vector3 pos = queuePositions[Mathf.Min(i, queuePositions.Length - 1)].position;
            BirdBase bird = Instantiate(birdPrefabs[i], pos, Quaternion.identity);
            bird.SetManager(this);
            spawnedBirds.Add(bird);
        }
    }

    public void LoadNextBird()
    {
        if (currentBird >= spawnedBirds.Count)
        {
            Debug.Log("BirdManager::No birds left");
            FindObjectOfType<LevelManager>().Lose();
            return;
        }

        IBird bird = spawnedBirds[currentBird];
        Transform birdTransform = ((MonoBehaviour)bird).transform;
        birdTransform.position = slingshot.launchPoint.position;

        slingshot.SetBird(bird);
        cameraFollow.SetTarget(birdTransform);
        currentBird++;

        UpdateQueuePositions();
        UpdateBirdUI();
    }

    void UpdateQueuePositions()
    {
        for (int i = currentBird; i < spawnedBirds.Count; i++)
        {
            int queueIndex = i - currentBird;
            if (queueIndex >= queuePositions.Length)
            {
                break;
            }

            Transform birdTransform = ((MonoBehaviour)spawnedBirds[i]).transform;
            birdTransform.position = queuePositions[queueIndex].position;
        }
    }

    public void AddBirdToQueue(BirdBase newBirdPrefab)
    {
        Vector3 pos = queuePositions[Mathf.Min(spawnedBirds.Count,queuePositions.Length - 1)].position;
        BirdBase bird = Instantiate(newBirdPrefab, pos, Quaternion.identity);
        bird.SetManager(this);
        spawnedBirds.Add(bird);

        UpdateQueuePositions();
        UpdateBirdUI();
    }

    void UpdateBirdUI()
    {
        for (int i = 0; i < birdIcons.Length; i++)
        {
            if (i < spawnedBirds.Count && i >= currentBird)
            {
                birdIcons[i].enabled = true;
            }
            else
            {
                birdIcons[i].enabled = false;
            }
        }
    }
}