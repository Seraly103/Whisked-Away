using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightLighting : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;

    [Header("Light Intensity")]
    [SerializeField] private float sunsetIntensity = 0.8f;
    [SerializeField] private float nightIntensity = 0.3f;
    [SerializeField] private float sunriseIntensity = 0.9f;

    [Header("Light Colors")]
    [SerializeField] private Color sunsetColor = Color.white;
    [SerializeField] private Color nightColor = Color.white;
    [SerializeField] private Color sunriseColor = Color.white;

    void Update()
    {
        if (DayManager.Instance == null)
            return;

        UpdateLighting();
    }

    private void UpdateLighting()
    {
        float hour = DayManager.Instance.CurrentHour;

        float intensity;

        // Sunset → getting darker
        if (hour < 18f)
        {
            intensity = Mathf.Lerp(
                sunsetIntensity,
                0.55f,
                Mathf.InverseLerp(13f, 18f, hour)
            );
        }

        // Evening → night
        else if (hour < 21f)
        {
            intensity = Mathf.Lerp(
                0.55f,
                nightIntensity,
                Mathf.InverseLerp(18f, 21f, hour)
            );
        }

        // Deep night
        else if (hour < 24f)
        {
            intensity = nightIntensity;
        }

        // Sunrise starts
        else if (hour < 26f)
        {
            intensity = Mathf.Lerp(
                nightIntensity,
                0.6f,
                Mathf.InverseLerp(24f, 26f, hour)
            );
        }

        // Sunrise gets brighter
        else
        {
            intensity = Mathf.Lerp(
                0.6f,
                sunriseIntensity,
                Mathf.InverseLerp(26f, 27f, hour)
            );
        }

        globalLight.intensity = intensity;
    }
}