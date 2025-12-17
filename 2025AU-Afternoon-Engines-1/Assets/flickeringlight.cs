using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [Header("Light Reference")]
    [SerializeField] private Light lightSource;

    [Header("Flicker Settings")]
    [Tooltip("Type of flicker effect")]
    public FlickerMode flickerMode = FlickerMode.Random;

    [Header("Intensity Settings")]
    [SerializeField] private float minIntensity = 0.5f;
    [SerializeField] private float maxIntensity = 1.5f;
    [SerializeField] private float baseIntensity = 1f;

    [Header("Timing Settings")]
    [SerializeField] private float flickerSpeed = 0.1f;
    [SerializeField] private float smoothness = 5f;

    [Header("Advanced Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float flickerChance = 0.5f; // For Random mode
    [SerializeField] private bool randomizeOnStart = true;

    private float targetIntensity;
    private float flickerTimer;

    public enum FlickerMode
    {
        Random,          // Random flickering
        Smooth,          // Smooth wave-like flicker
        Strobe,          // Fast on/off
        CanleFlame,      // Candle-like gentle flicker
        Broken,          // Damaged light effect
        Lightning        // Quick flash effect
    }

    void Start()
    {
        // Get light component if not assigned
        if (lightSource == null)
            lightSource = GetComponent<Light>();

        if (lightSource == null)
        {
            Debug.LogError("No Light component found! Please assign a light.");
            enabled = false;
            return;
        }

        baseIntensity = lightSource.intensity;
        targetIntensity = baseIntensity;

        if (randomizeOnStart)
            flickerTimer = Random.Range(0f, 100f);
    }

    void Update()
    {
        if (lightSource == null) return;

        flickerTimer += Time.deltaTime;

        switch (flickerMode)
        {
            case FlickerMode.Random:
                RandomFlicker();
                break;
            case FlickerMode.Smooth:
                SmoothFlicker();
                break;
            case FlickerMode.Strobe:
                StrobeFlicker();
                break;
            case FlickerMode.CanleFlame:
                CandleFlicker();
                break;
            case FlickerMode.Broken:
                BrokenFlicker();
                break;
            case FlickerMode.Lightning:
                LightningFlicker();
                break;
        }

        // Smooth transition to target intensity
        lightSource.intensity = Mathf.Lerp(
            lightSource.intensity,
            targetIntensity,
            Time.deltaTime * smoothness
        );
    }

    void RandomFlicker()
    {
        if (flickerTimer >= flickerSpeed)
        {
            flickerTimer = 0f;

            if (Random.value < flickerChance)
            {
                targetIntensity = Random.Range(minIntensity, maxIntensity);
            }
            else
            {
                targetIntensity = baseIntensity;
            }
        }
    }

    void SmoothFlicker()
    {
        // Perlin noise for smooth variation
        float noise = Mathf.PerlinNoise(flickerTimer * flickerSpeed, 0f);
        targetIntensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }

    void StrobeFlicker()
    {
        if (flickerTimer >= flickerSpeed)
        {
            flickerTimer = 0f;
            targetIntensity = (targetIntensity == maxIntensity) ? minIntensity : maxIntensity;
        }
    }

    void CandleFlicker()
    {
        // Multiple noise layers for realistic candle effect
        float noise1 = Mathf.PerlinNoise(flickerTimer * 2f, 0f);
        float noise2 = Mathf.PerlinNoise(flickerTimer * 5f, 10f) * 0.3f;

        float combined = (noise1 + noise2) / 1.3f;
        targetIntensity = Mathf.Lerp(
            baseIntensity * 0.8f,
            baseIntensity * 1.2f,
            combined
        );
    }

    void BrokenFlicker()
    {
        if (flickerTimer >= flickerSpeed)
        {
            flickerTimer = 0f;

            // Randomly flicker on/off with varying durations
            if (Random.value < 0.3f)
            {
                targetIntensity = 0f;
            }
            else if (Random.value < 0.7f)
            {
                targetIntensity = Random.Range(minIntensity, maxIntensity);
            }
            else
            {
                targetIntensity = maxIntensity;
            }
        }
    }

    void LightningFlicker()
    {
        if (flickerTimer >= flickerSpeed)
        {
            flickerTimer = 0f;

            // Quick intense flash then darkness
            if (Random.value < 0.1f)
            {
                targetIntensity = maxIntensity * 2f;
                Invoke(nameof(TurnOff), 0.05f);
            }
        }
    }

    void TurnOff()
    {
        targetIntensity = 0f;
    }

    // Public methods for external control
    public void SetFlickerMode(FlickerMode mode)
    {
        flickerMode = mode;
    }

    public void SetFlickerSpeed(float speed)
    {
        flickerSpeed = speed;
    }

    public void EnableFlicker(bool enable)
    {
        enabled = enable;
        if (!enable)
            lightSource.intensity = baseIntensity;
    }
}