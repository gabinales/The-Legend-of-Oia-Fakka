using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector director;
    public CutsceneData[] cutscenes; // Lista de cutscenes associadas à Tela
    public playerController pController; // Para interromper ou não o fluxo de controle do personagem


    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Transform playerTransform = other.transform;
            Debug.Log("ENTROU");

            CutsceneData cutsceneToPlay = FindCutsceneByTransform(playerTransform);
            if(cutsceneToPlay != null & !cutsceneToPlay.cutsceneJaReproduzida){ // Verifica a cutscene associada ao Trigger em questão, além de testar se ela já foi exibida
                cutsceneToPlay.cutsceneJaReproduzida = true;
                pController.enabled = false;
                StartCoroutine(ReproduzirCutscene(cutsceneToPlay));
            }
        }
    }

    private IEnumerator ReproduzirCutscene(CutsceneData cutsceneData){
        // Reproduzir a cutscene selecionada:
        director.playableAsset = cutsceneData.cutscene;
        director.Play();

        yield return new WaitForSeconds(cutsceneData.duracaoPausa);
        pController.enabled = true;
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
