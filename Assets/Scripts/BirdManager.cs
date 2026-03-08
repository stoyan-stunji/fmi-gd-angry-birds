using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BirdManager : MonoBehaviour
{
    public Slingshot slingshot;
    public CameraFollow cameraFollow;

    public List<BaseBird> birdPrefabs;
    public Transform[] queuePositions;

    [Header("UI")]
    public Image[] birdIcons;

    private List<BaseBird> spawnedBirds = new List<BaseBird>();
    private int currentBird = 0;

    void Start() {
        SpawnBirdQueue();
        LoadNextBird();
        UpdateBirdUI();
    }

    void SpawnBirdQueue() {
        for (int i = 0; i < birdPrefabs.Count; i++) {
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

    public void LoadNextBird() {
        if (currentBird >= spawnedBirds.Count)
        {
            Debug.Log("BirdManager::No birds left");
            FindObjectOfType<LevelManager>().Lose();
            return;
        }

        BaseBird bird = spawnedBirds[currentBird];
        bird.transform.position = slingshot.launchPoint.position;
        slingshot.SetBird(bird);
        cameraFollow.SetTarget(bird.transform);
        currentBird++;
        UpdateQueuePositions();
        UpdateBirdUI();
    }

    void UpdateQueuePositions() {
        for (int i = currentBird; i < spawnedBirds.Count; i++) {
            int queueIndex = i - currentBird;
            if (queueIndex >= queuePositions.Length) {
                break;
            }

            spawnedBirds[i].transform.position = queuePositions[queueIndex].position;
        }
    }

    public void AddBirdToQueue(BaseBird newBirdPrefab) {
        Vector3 pos = queuePositions[Mathf.Min(spawnedBirds.Count, queuePositions.Length - 1)].position;

        BaseBird bird = Instantiate(
            newBirdPrefab,
            pos,
            Quaternion.identity
        );

        bird.SetManager(this);
        spawnedBirds.Add(bird);
        UpdateQueuePositions();
        UpdateBirdUI();
    }

    void UpdateBirdUI() {
        for (int i = 0; i < birdIcons.Length; i++) {
            if (i < spawnedBirds.Count && i >= currentBird) {
                birdIcons[i].enabled = true;
            }
            else {
                birdIcons[i].enabled = false;
            }
        }
    }
}