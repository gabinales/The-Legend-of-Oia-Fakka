using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class LootChest : MonoBehaviour, iInteragivel
{
    [SerializeField] private TextAsset inkJSON;
    public ItemData conteudo;
    public int _quantidade = 1;
    private string _nomeDoItem;
    public Inventory Inventory;

    public Animator animator;

    private bool pegou = false;

    private void Start(){
        _nomeDoItem = conteudo.nomeLoot;
        //Inventory = GetComponent<Inventory>();
    }
    
    public void Interacao(){
        if(!pegou){
            // Exibe animação, se houver:
            if(animator != null){
                animator.SetTrigger("aberto");
                // Por algum motivo, a animação tá pulando pro último frame.
            }

            // Abre a caixa de diálogo, mas antes informa 
            // ao Ink o item e a quantidade:
            Ink.Runtime.Object nomeDoItem = new Ink.Runtime.StringValue(_nomeDoItem);
            Ink.Runtime.Object quantidade = new Ink.Runtime.IntValue(_quantidade);
            DialogManager.Instance.SetVariableState("NomeDoItem", nomeDoItem);
            DialogManager.Instance.SetVariableState("Quantidade", quantidade);

            DialogManager.Instance.StartDialogue(inkJSON);

            // Adiciona o item ao inventário:
            Inventory.Add(conteudo);

            // Esvazia o "baú":
            conteudo = null;

            pegou = true;
        }
    }
}
