using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.Playables;

/* Este script altera os valores das variáveis globais do Ink:
    ArmaAtual: "Grassblade"

*/
public class PickWeaponGrassblade : QuestPoint
{
    public PlayerStats playerStats;
    public playerController pController; // Para interromper o fluxo de controle do personagem.
    
    [Header("Cutscene")]
    public float duracaoPausa;

    public PlayableDirector director;
    public PlayableAsset cutscene;

    private string _armaAtual = "ArmaAtual"; // ArmaAtual
    private string _grassblade = "Grassblade";

    private bool alreadyPlayed = false;

    public override void Interacao(){ // essa Interacao (override) sobrepõe a Interacao (virtual) na classe base QuestPoint
        // FMOD:
        AudioManager.instance.PlayOneShot(FMODEvents.instance.weaponCollected, this.transform.position);
        
        // Altera o valor da variável armaAtual em playerStats e habilita o ataque de espada:

        playerStats.ArmaAtual = Arma.Grassblade;
        
        // Altera o valor da variável ArmaAtual no globals.ink:
        Ink.Runtime.Object _novaArma = new Ink.Runtime.StringValue(_grassblade);
        DialogManager.Instance.SetVariableState(_armaAtual, _novaArma);

        // Destrói o objeto:
        Destroy(gameObject);

        // Exibe a cutscene:
        if(director != null){
            pController.enabled = false;
            //pController.isCutsceneActive = true;
            ReproduzirCutscene();
        }
    }

    private void ReproduzirCutscene(){
        director.playableAsset = cutscene;
        director.stopped += OnCutsceneStopped;
        director.Play();
    }

    private void OnCutsceneStopped(PlayableDirector director){
        if(!alreadyPlayed){
            // Inicia o diálogo após a cutscene terminar:
            pController.moveDirection = Vector2.zero;
            DialogManager.Instance.StartDialogue(inkJSON);
            alreadyPlayed = true;
            pController.enabled = true;
            //pController.isCutsceneActive = false;
        }
    }
}
