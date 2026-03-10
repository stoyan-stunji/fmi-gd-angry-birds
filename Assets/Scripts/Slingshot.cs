using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public Transform launchPoint;
    public float maxDistance = 2f;
    public float launchPower = 8f;

    private BaseBird currentBird;
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
            StartDragging();
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

    private void StartDragging()
    {
        dragging = true;
    }

    private void DragBird()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouse - (Vector2)launchPoint.position;
        direction = Vector2.ClampMagnitude(direction, maxDistance);
        currentBird.transform.position = launchPoint.position + (Vector3)direction;
    }

    private void ReleaseBird()
    {
        dragging = false;

        Vector2 launchDir = launchPoint.position - currentBird.transform.position;
        currentBird.Launch(launchDir * launchPower);
        currentBird = null;
    }

    public void SetBird(BaseBird bird)
    {
        currentBird = bird;
        bird.transform.position = launchPoint.position;
    }
}