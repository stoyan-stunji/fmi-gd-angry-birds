using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public Transform launchPoint;
    public float maxDistance = 2f;
    public float launchPower = 8f;

    private IBird currentBird;
    private Transform currentBirdTransform;

    private bool dragging = false;

    void Update()
    {
        if (currentBird == null)
        {
            return;
        }
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
        }

        if (dragging)
        {
            DragBird();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseBird();
        }
    }

    private void DragBird()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mouse - (Vector2)launchPoint.position;
        direction = Vector2.ClampMagnitude(direction, maxDistance);
        currentBirdTransform.position = launchPoint.position + (Vector3)direction;
    }

    private void ReleaseBird()
    {
        dragging = false;
        Vector2 launchDir = launchPoint.position - currentBirdTransform.position;

        currentBird.Launch(launchDir * launchPower);
        currentBird = null;
        currentBirdTransform = null;
    }

    public void SetBird(IBird bird)
    {
        currentBird = bird;
        currentBirdTransform = ((MonoBehaviour)bird).transform;
        currentBirdTransform.position = launchPoint.position;
    }
}