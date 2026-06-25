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

    [Header("Sound Settings")]
    public AudioClip quakeSfx;
    private AudioSource audioSource;

    private Vector3 originalCamPos;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera != null)
        {
            originalCamPos = mainCamera.transform.position;
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
            PlayQuakeSound();

            Rigidbody2D[] objects = FindObjectsOfType<Rigidbody2D>();
            yield return ApplyQuake(objects);
        }
    }

    private void PlayQuakeSound()
    {
        if (quakeSfx != null)
        {
            audioSource.PlayOneShot(quakeSfx);
        }
    }

    private IEnumerator ApplyQuake(Rigidbody2D[] objects)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            ApplyForceToRigidbodies(objects);
            ShakeCamera();
            yield return null;
        }

        ResetCameraPosition();
    }

    private void ApplyForceToRigidbodies(Rigidbody2D[] objects)
    {
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
    }

    private void ShakeCamera()
    {
        if (mainCamera == null)
        {
            return;
        }

        Vector3 shakeOffset = new Vector3(
            Random.Range(-shakeMagnitude, shakeMagnitude),
            Random.Range(-shakeMagnitude, shakeMagnitude),
            0
        );
        mainCamera.transform.position = originalCamPos + shakeOffset;
    }

    private void ResetCameraPosition()
    {
        if (mainCamera != null)
        {
            mainCamera.transform.position = originalCamPos;
        }
    }
}