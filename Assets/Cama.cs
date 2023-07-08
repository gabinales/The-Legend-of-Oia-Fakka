using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;


public class Cama : MonoBehaviour, iInteragivel
{
    [SerializeField] private GameObject gameController;

    [SerializeField] private TextAsset inkJSON;

    public void Interacao()
    {

        Story currentStory = new Story(inkJSON.text);

        currentStory.BindExternalFunction("MudaHora", (int id) =>
        {
            GameController controler = gameController.GetComponent<GameController>();
            controler.timeOfDay = id;
        });

        DialogManager.Instance.StartDialogue(inkJSON);

        /*      void mudaHora()
             {
                 horaAtual = gameController.timeOfDay;

             } */
    }
}