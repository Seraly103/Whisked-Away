using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private TMP_Text timeText;

    [Header("Time Portrait UI")]
    [SerializeField] private Image currentSkyImage;
    [SerializeField] private Image transitionSkyImage;

    [Header("Sky Sprites")]
    [SerializeField] private Sprite SunsetSky;
    [SerializeField] private Sprite SunsetSky2;
    [SerializeField] private Sprite Night1;
    [SerializeField] private Sprite Night2;
    [SerializeField] private Sprite Sunrise1;
    [SerializeField] private Sprite Sunrise2;

    private Sprite currentSkySprite;
    private Coroutine fadeCoroutine;

    void Start()
    {
        currentSkySprite = SunsetSky;

        currentSkyImage.sprite = SunsetSky;

        Color fadeColor = transitionSkyImage.color;;
        fadeColor.a = 0f;
        transitionSkyImage.color = fadeColor;
    }

    void Update()
    {
        if (DayManager.Instance == null)
            return;

        dayText.text = "Day " + DayManager.Instance.CurrentDay;

        float currentHour = DayManager.Instance.CurrentHour;

        int hour = Mathf.FloorToInt(currentHour);
        int minutes = Mathf.FloorToInt((currentHour - hour) * 60f);

        timeText.text =
            hour.ToString("00") + ":" +
            minutes.ToString("00");

        UpdateSkyImage();
    }

    private void UpdateSkyImage()
    {
        float hour = DayManager.Instance.CurrentHour;

        Sprite targetSprite;

        if (hour < 16f)
        {
            targetSprite = SunsetSky;
        }
        else if (hour < 18f)
        {
            targetSprite = SunsetSky2;
        }
        else if (hour < 21f)
        {
            targetSprite = Night1;
        }
        else if (hour < 24f)
        {
            targetSprite = Night2;
        }
        else if (hour < 26f)
        {
            targetSprite = Sunrise1;
        }
        else
        {
            targetSprite = Sunrise2;
        }

        if (targetSprite != currentSkySprite)
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }

            fadeCoroutine = StartCoroutine(
                FadeToSky(targetSprite)
            );
        }
    }

    private IEnumerator FadeToSky(Sprite newSprite)
    {
        currentSkySprite = newSprite;

        transitionSkyImage.sprite = newSprite;

        Color color = transitionSkyImage.color;
        color.a = 0f;
        transitionSkyImage.color = color;

        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            color.a = Mathf.Lerp(
                0f,
                1f,
                elapsed / duration
            );

            transitionSkyImage.color = color;

            yield return null;
        }

        currentSkyImage.sprite = newSprite;

        color.a = 0f;
        transitionSkyImage.color = color;

        fadeCoroutine = null;
    }
}