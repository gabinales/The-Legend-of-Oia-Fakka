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

        DialogManager.Instance.StartDialogue(inkJSON);
    }
}