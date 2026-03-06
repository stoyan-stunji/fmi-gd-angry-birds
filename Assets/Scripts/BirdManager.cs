using UnityEngine;
using System.Collections.Generic;

public class BirdManager : MonoBehaviour
{
    public Slingshot slingshot;
    public CameraFollow cameraFollow;

    public List<Bird> birds;

    private int currentBird = 0;

    void Start()
    {
        LoadNextBird();
    }

    public void LoadNextBird()
    {
        if (currentBird >= birds.Count)
        {
            Debug.Log("No birds left!");
            return;
        }

        Bird bird = birds[currentBird];

        bird.SetManager(this);

        slingshot.SetBird(bird);

        cameraFollow.SetTarget(bird.transform);  

        currentBird++;
    }
}