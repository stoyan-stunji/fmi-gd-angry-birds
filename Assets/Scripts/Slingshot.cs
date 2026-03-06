using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public Transform launchPoint;
    public float maxDistance = 2f;
    public float launchPower = 8f;

    private Bird currentBird;
    private bool dragging = false;

    void Update()
    {
        if (currentBird == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
        }

        if (dragging)
        {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mouse - (Vector2)launchPoint.position;

            direction = Vector2.ClampMagnitude(direction, maxDistance);

            currentBird.transform.position = launchPoint.position + (Vector3)direction;
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;

            Vector2 launchDir = launchPoint.position - currentBird.transform.position;

            currentBird.Launch(launchDir * launchPower);

            currentBird = null;
        }
    }

    public void SetBird(Bird bird)
    {
        currentBird = bird;

        bird.transform.position = launchPoint.position;
    }
}