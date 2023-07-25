using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SwitchCristalVerde : ToggleSwitch
{
    public Light2D luzPrincipal;
    public AnimationCurve curvaIntensidade;

    private float intensidadeAtual = 0f;
    private float intensidadeMaxima = 0f;
    private float duracaoTransicao = 1f;

    private void Start(){
        if(curvaIntensidade == null || curvaIntensidade.length == 0){
            Debug.LogError("A AnimationCurve não foi atribuída.");
            enabled = false;
            return;
        }
        intensidadeAtual = luzPrincipal.intensity;
    }
    private void Update(){
        if(!estaAtivado){
            intensidadeMaxima = 0f;
            ChamaCoroutine();
        }

        else if(estaAtivado){
            intensidadeMaxima = 2f;
            ChamaCoroutine();
        }
    }

    private void ChamaCoroutine(){
        StartCoroutine(MudaIntensidade());
    }

    private IEnumerator MudaIntensidade(){
        float timeElapsed = 0f;
        float intensidadeInicial = intensidadeAtual;

        while(timeElapsed < duracaoTransicao){
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / duracaoTransicao);
            intensidadeAtual = Mathf.Lerp(intensidadeInicial, intensidadeMaxima, curvaIntensidade.Evaluate(t));
            luzPrincipal.intensity = intensidadeAtual;
            yield return null;
        }
        intensidadeAtual = intensidadeMaxima;
        luzPrincipal.intensity = intensidadeAtual;
    }
}
