using UnityEngine;
using System.Collections;

public class Earthquake : MonoBehaviour
{
    [Header("Quake Settings")]
    public float quakeForce = 3f;
    public float duration = 1f;
    public float interval = 10f;

    [Header("Camera Shake Settings")]
    public Camera mainCamera;
    public float shakeMagnitude = 0.3f;
    public float zoomOutSize = 10f;       // target orthographic size during quake
    public float zoomSpeed = 3f;          // how fast camera zooms in/out

    [Header("Sound Settings")]
    public AudioClip quakeSfx;
    private AudioSource audioSource;

    private Vector3 originalCamPos;
    private float originalCamSize;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera != null)
        {
            originalCamPos = mainCamera.transform.position;
            originalCamSize = mainCamera.orthographicSize;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;

        StartCoroutine(QuakeRoutine());
    }

    IEnumerator QuakeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // Play quake sound
            if (quakeSfx != null)
                audioSource.PlayOneShot(quakeSfx);

            // Zoom out smoothly at start
            if (mainCamera != null)
            {
                float t = 0f;
                float startSize = mainCamera.orthographicSize;
                while (t < 1f)
                {
                    t += Time.deltaTime * zoomSpeed;
                    mainCamera.orthographicSize = Mathf.Lerp(startSize, zoomOutSize, t);
                    yield return null;
                }
            }

            Rigidbody2D[] objects = FindObjectsOfType<Rigidbody2D>();
            float startTime = Time.time;

            while (Time.time - startTime < duration)
            {
                // Apply random force to all rigidbodies
                foreach (Rigidbody2D rb in objects)
                {
                    if (rb != null && !rb.isKinematic)
                    {
                        Vector2 randomForce = new Vector2(
                            Random.Range(-quakeForce, quakeForce),
                            Random.Range(-quakeForce, quakeForce)
                        );
                        rb.AddForce(randomForce, ForceMode2D.Force);
                    }
                }

                // Camera shake
                if (mainCamera != null)
                {
                    Vector3 shakeOffset = new Vector3(
                        Random.Range(-shakeMagnitude, shakeMagnitude),
                        Random.Range(-shakeMagnitude, shakeMagnitude),
                        0
                    );
                    mainCamera.transform.position = originalCamPos + shakeOffset;
                }

                yield return null;
            }

            // Reset camera position
            if (mainCamera != null)
            {
                mainCamera.transform.position = originalCamPos;

                // Smoothly zoom back to original size
                float t = 0f;
                float startSize = mainCamera.orthographicSize;
                while (t < 1f)
                {
                    t += Time.deltaTime * zoomSpeed;
                    mainCamera.orthographicSize = Mathf.Lerp(startSize, originalCamSize, t);
                    yield return null;
                }
            }
        }
    }
}