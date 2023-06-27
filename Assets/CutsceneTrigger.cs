using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    private bool cutsceneJaReproduzida = false;
    public int duracaoPausa;

    public PlayableDirector director;
    public playerController pController;


   

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !cutsceneJaReproduzida){
            cutsceneJaReproduzida = true;
            pController.enabled = false;

            StartCoroutine(ReproduzirCutscene());
        }
    }

    private IEnumerator ReproduzirCutscene(){
        // Reproduzir cutscene
        director.Play();

        yield return new WaitForSeconds(duracaoPausa);
        pController.enabled = true;
    }
}
