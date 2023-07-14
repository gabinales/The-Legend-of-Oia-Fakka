using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Fechadura : MonoBehaviour, iInteragivel
{
    [SerializeField] private TextAsset portaTrancada;
    [SerializeField] private TextAsset portaDestrancada;
    
    private string _nomeDoItem;

    public Animator animator;
    
    private bool trancada = true;
    private Inventory Inventory;
    public ItemData chave;

    private void Start(){
        Inventory = FindObjectOfType<Inventory>();
        _nomeDoItem = chave.nomeLoot;
    }

    public void Interacao(){
        SearchKeyInInventory(chave);
        if(trancada){
            DialogManager.Instance.StartDialogue(portaTrancada);
        }
        else{
            Ink.Runtime.Object nomeDoItem = new Ink.Runtime.StringValue(_nomeDoItem);
            DialogManager.Instance.SetVariableState("NomeDoItem", nomeDoItem);
            DialogManager.Instance.StartDialogue(portaDestrancada);
        }
    }

    public void SearchKeyInInventory(ItemData chave){
        foreach(InventoryItem item in Inventory.inventory){
            if(item.itemData == chave){
                Inventory.Remove(item.itemData); //???
                trancada = false;

                animator.SetTrigger("destrancou");
            }
        }
    }

    public void DestroyLockSprite(){
        Destroy(gameObject);
    }
}
