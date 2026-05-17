using UnityEngine;

public class Wind : MonoBehaviour
{
    [Header("Wind Settings")]
    public Vector2 windForce =
        new Vector2(5f, 0f);

    [Header("Filtering")]
    public bool affectBirds = true;
    public bool affectBlocks = true;

    [Header("Audio")]
    public AudioClip windSfx;

    private AudioSource audioSource;

    void Awake()
    {
        SetupAudio();
    }

    void FixedUpdate()
    {
        ApplyWind();
    }

    void SetupAudio()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        if (windSfx != null)
        {
            audioSource.clip = windSfx;
            audioSource.Play();
        }
    }

    void ApplyWind()
    {
        MonoBehaviour[] objects = FindObjectsOfType<MonoBehaviour>();

        foreach (MonoBehaviour obj in objects)
        {
            IWindAware windAware = obj as IWindAware;
            if (windAware == null)
            {
                continue;
            }

            bool isBird = obj is BirdBase;
            bool isBlock = obj is Block;

            if (isBird && !affectBirds)
            {
                continue;
            }

            if (isBlock && !affectBlocks)
            {
                continue;
            }

            if (!windAware.CanBeAffectedByWind())
            {
                continue;
            }

            windAware.Rigidbody.AddForce(windForce, ForceMode2D.Force);
        }
    }
}