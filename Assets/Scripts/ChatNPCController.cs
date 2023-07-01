using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class ChatNPCController : MonoBehaviour, iInteragivel // Explicita que esta classe é um NPC interagível
{
    [SerializeField] private TextAsset inkJSON;
    public string itemBusca;
    public int itemQuant;

    public Inventory Inventory;
    private int itemStackSize;

    public void Interacao()
    {
        SearchInventory(itemBusca);
        Ink.Runtime.Object obj = new Ink.Runtime.IntValue(itemStackSize);
        DialogManager.Instance.SetVariableState(itemBusca.Replace(" ", "_"), obj);
   
        DialogManager.Instance.StartDialogue(inkJSON); // Chama uma instância do Dialog Manager
    }

    // Para quests de coletar itens: verifica o item e a quantidade
    public void SearchInventory(string itemBusca){
        foreach(InventoryItem item in Inventory.inventory){
            if(item.itemData.nomeLoot == itemBusca){
                itemStackSize = item.stackSize;
                //return true;
            }
        }
        //return false;
    }
}
