using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingRoof : MonoBehaviour
{
    public float fadeDuration = 0.5f;
    private bool isFading;
    private SpriteRenderer roofRenderer;
    private Color startColor;

    private void Start(){
        roofRenderer = GetComponent<SpriteRenderer>();
        startColor = roofRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            if(!isFading){
                StartCoroutine(FadeRoof(true));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            if(isFading){
                StartCoroutine(FadeRoof(false));
            }
        }
    }
    private IEnumerator FadeRoof(bool fadeOut){
        isFading = true;
        float fadeTimer = 0f;
        Color targetColor = fadeOut ? new Color(startColor.r, startColor.g, startColor.b, 0f) : startColor;
        /*
            Se fadeOut for 'true', vai pegar os mesmos valores rgb da startColor e modificar somente
            a transparência (alpha) pra 0f. Se for 'false', vai pegar todos os valores originais de
            startColor, incluindo o alpha.
        */


        while(fadeTimer < fadeDuration){
            fadeTimer += Time.deltaTime;
            float t = fadeTimer / fadeDuration;
            roofRenderer.color = Color.Lerp(startColor, targetColor, t); // interpolação linear
            yield return null;
        }

        roofRenderer.color = targetColor;
        isFading = false;
    }
}
