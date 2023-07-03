using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.Playables;

public class PickWeaponGrassblade : MonoBehaviour, IColetavel
{
    public PlayerStats playerStats;

    public PlayableDirector director;
    public PlayableAsset cutscene;

    private string _armaAtual = "ArmaAtual"; // ArmaAtual
    private string _grassblade = "Grassblade";

    private string _jaConversou = "TalkedToZoroastros";
    private bool jaConversou = false;

    public void Collect(){
        // Altera o valor da variável armaAtual em playerStats e habilita o ataque de espada:
        playerStats.ArmaAtual = Arma.Grassblade;

        // Altera o valor da variável ArmaAtual no globals.ink:
        Ink.Runtime.Object _novaArma = new Ink.Runtime.StringValue(_grassblade);
        DialogManager.Instance.SetVariableState(_armaAtual, _novaArma);

        // Destrói o objeto:
        Destroy(gameObject);

        // Exibe a cutscene:
        if(director != null){
            director.playableAsset = cutscene;
            director.Play();
        }

        // Altera o valor do booleano "TalkedToZoroastros" para 'true':
        if(!jaConversou){
            jaConversou = true;
            Ink.Runtime.Object obj = new Ink.Runtime.BoolValue(jaConversou);
            DialogManager.Instance.SetVariableState(_jaConversou, obj);
        }

        // Inicia o diálogo com Zoroastros:
        
    }
}
