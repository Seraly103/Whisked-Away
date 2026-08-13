using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndDayUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup overlayCanvasGroup;
    [SerializeField] private GameObject endDayMenu;

    [SerializeField] private float fadeDuration = 2f;

    private void Start()
    {
        overlayCanvasGroup.alpha = 0f;
        overlayCanvasGroup.blocksRaycasts = false;
        overlayCanvasGroup.interactable = false;

        endDayMenu.SetActive(false);
    }

    public void ShowEndDayScreen()
    {
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        overlayCanvasGroup.blocksRaycasts = true;

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            overlayCanvasGroup.alpha =
                Mathf.Lerp(0f, 1f, elapsed / fadeDuration);

            yield return null;
        }

        overlayCanvasGroup.alpha = 1f;

        endDayMenu.SetActive(true);
        overlayCanvasGroup.interactable = true;
    }

    public void NextDay()
    {
        endDayMenu.SetActive(false);

        DayManager.Instance.StartNextDay();

        StartCoroutine(FadeFromBlack());
    }

    private IEnumerator FadeFromBlack()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            overlayCanvasGroup.alpha =
                Mathf.Lerp(1f, 0f, elapsed / fadeDuration);

            yield return null;
        }

        overlayCanvasGroup.alpha = 0f;
        overlayCanvasGroup.blocksRaycasts = false;
        overlayCanvasGroup.interactable = false;
    }

    public void SaveGame()
    {
        Debug.Log("Save system coming later!");
    }
}