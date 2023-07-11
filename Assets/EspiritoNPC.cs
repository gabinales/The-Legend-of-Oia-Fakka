using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Ink.UnityIntegration;

public class EspiritoNPC : MonoBehaviour, iInteragivel
{
    [SerializeField] private TextAsset inkJSON;

    private GameObject Ignorancio;

    public GameController GameController;

    //private Story inkStory;

    //private bool spawned = false;

    //private Dictionary<string, Ink.Runtime.Object> variables;

    Story inkStory;

    public void Start()
    {
        inkStory = new Story(inkJSON.text);
        Ignorancio = transform.GetChild(0).gameObject;
    }

    public void Update()
    {
        int spawn = ((Ink.Runtime.IntValue)DialogManager.Instance.GetVariable("SPAWN")).value;

        if (GameController.timeOfDay != 2)
        {
            Ignorancio.GetComponentInChildren<SpriteRenderer>().enabled = false;

        }
        else if (spawn == 1)
        {
            Ignorancio.GetComponentInChildren<SpriteRenderer>().enabled = true;

        }
    }

    public void Interacao()
    {
        //Atualiza a ink var "hora do dia" para o valor atual do timeofday (convertido para ink object)
        DialogManager.Instance.SetVariableState("HoraDoDia", new Ink.Runtime.IntValue(GameController.timeOfDay));
        DialogManager.Instance.StartDialogue(inkJSON);
    }
}
