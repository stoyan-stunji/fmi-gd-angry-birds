using UnityEngine;
using System.Collections.Generic;

public class BirdManager : MonoBehaviour
{
    public Slingshot slingshot;
    public CameraFollow cameraFollow;

    // Store bird **prefabs**, not instances
    public List<BaseBird> birdPrefabs;

    private int currentBird = 0;

    void Start()
    {
        LoadNextBird();
    }

    public void LoadNextBird()
    {
        if (currentBird >= birdPrefabs.Count)
        {
            Debug.Log("No birds left!");
            return;
        }

        // Instantiate a new bird from prefab
        BaseBird bird = Instantiate(
            birdPrefabs[currentBird],
            slingshot.launchPoint.position,
            Quaternion.identity
        );

        bird.SetManager(this);

        slingshot.SetBird(bird);

        cameraFollow.SetTarget(bird.transform);

        currentBird++;
    }
}