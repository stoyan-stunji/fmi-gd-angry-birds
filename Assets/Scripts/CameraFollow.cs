using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float speed = 8f;
    public Vector3 offset = new Vector3(0, 5, -10);

    [Header("Intro Settings")]
    public Transform levelOverviewPoint;
    public float introDuration = 5f;

    private bool introPlaying = true;
    private float introTimer = 0f;

    void Start() {
        if (levelOverviewPoint != null) {
            transform.position = levelOverviewPoint.position;
        }
    }

    void LateUpdate() {
        if (target == null) {
            return;
        }

        if (introPlaying) {
            PlayIntro();
            return;
        }

        FollowTarget();
    }

    void PlayIntro() {
        introTimer += Time.deltaTime;

        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.Lerp(
            levelOverviewPoint.position,
            desiredPosition,
            introTimer / introDuration
        );

        if (introTimer >= introDuration) {
            introPlaying = false;
        }
    }

    void FollowTarget() {
        Vector3 desiredPosition = target.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            speed * Time.deltaTime
        );
    }

    public void SetTarget(Transform newTarget) {
        target = newTarget;
    }
}