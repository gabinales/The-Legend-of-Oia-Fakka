using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public Animator alertaContextualAnimator;
    public PlayableDirector director;
    public CutsceneData[] cutscenes; // Lista de cutscenes associadas à Tela
    public playerController pController; // Para interromper ou não o fluxo de controle do personagem

    private Transform playerTransform;

    public void StartCutscene(Transform transform){
        playerTransform = transform;

        CutsceneData cutsceneToPlay = FindCutsceneByTransform(playerTransform);
        if(cutsceneToPlay != null && !cutsceneToPlay.cutsceneJaReproduzida){
            cutsceneToPlay.cutsceneJaReproduzida = true;
            pController.enabled = false;
            //pController.animator.enabled = false;
            //pController.isCutsceneActive = true;
            
            StartCoroutine(ReproduzirCutscene(cutsceneToPlay));
        }
    }
    private IEnumerator ReproduzirCutscene(CutsceneData cutsceneData){

        // Reproduzir a cutscene selecionada:
        director.playableAsset = cutsceneData.cutscene;
        director.Play();

        yield return new WaitForSeconds(cutsceneData.duracaoPausa);
        pController.enabled = true;
        //pController.animator.enabled = true;
        //pController.isCutsceneActive = false;
    }

    [System.Serializable]
    public class CutsceneData{
        public Transform triggerTransform;
        public PlayableAsset cutscene;
        public bool cutsceneJaReproduzida = false;
        public float duracaoPausa;
    }
    private CutsceneData FindCutsceneByTransform(Transform playerTransform){
        foreach(CutsceneData cutscene in cutscenes){
            if(cutscene.triggerTransform == playerTransform){
                return cutscene;
            }
        }
        return null;
    }
}
