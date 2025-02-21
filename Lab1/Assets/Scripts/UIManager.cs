using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CanvasGroup menuPanel; // Assign your menu panel in Inspector
    public float fadeDuration = 1f;

    private bool isMenuVisible = false;

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape)) // Change key if needed
        //{
        //    ToggleMenu();
        //}
    }

    private void Start()
    {
        ShowObjective(3f);
    }

    public void ToggleMenu()
    {
        isMenuVisible = !isMenuVisible;
        StopAllCoroutines();
        StartCoroutine(FadeMenu(isMenuVisible));
    }

    IEnumerator FadeMenu(bool show)
    {
        float startAlpha = menuPanel.alpha;
        float endAlpha = show ? 1 : 0;
        float elapsedTime = 0f;

        menuPanel.interactable = show;
        menuPanel.blocksRaycasts = show;

        while (elapsedTime < fadeDuration)
        {
            menuPanel.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        menuPanel.alpha = endAlpha;
    }

    public void ShowObjective(float displayTime)
    {
        StopAllCoroutines();
        StartCoroutine(FadeObjective(displayTime));
    }

    IEnumerator FadeObjective(float displayTime)
    {
        yield return FadeMenu(true); // Show
        yield return new WaitForSeconds(displayTime);
        yield return FadeMenu(false); // Hide
    }
}
