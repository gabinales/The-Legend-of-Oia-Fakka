using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingRoof : MonoBehaviour
{
    private float fadeDuration = 0.1f; // Duração do efeito de fade (em segundos)
    private bool isFading; // Indica se o fade está ocorrendo
    private SpriteRenderer roofRenderer;
    private Color startColor;
    private Color targetColor;

    private void Start()
    {
        roofRenderer = GetComponent<SpriteRenderer>();
        startColor = roofRenderer.color;
        targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isFading)
            {
                StartCoroutine(FadeRoof(targetColor));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isFading)
            {
                StartCoroutine(FadeRoof(startColor));
            }
        }
    }

    private IEnumerator FadeRoof(Color target)
    {
        isFading = true;
        float fadeTimer = 0f;
        Color currentColor = roofRenderer.color;

        while (fadeTimer < fadeDuration)
        {
            fadeTimer += Time.deltaTime;
            float t = fadeTimer / fadeDuration;
            roofRenderer.color = Color.Lerp(currentColor, target, t);
            yield return null;
        }

        roofRenderer.color = target;

        isFading = false;
    }
}