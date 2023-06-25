using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Lista de itens no Inventário
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>(); // Esse dicionário vai mexer com o stackSize

    public delegate void ItemChangedHandler();
    public event ItemChangedHandler OnItemChanged; //evento para sinalizar que um item foi adicionado

    private void OnEnable(){
        Loot.OnLootCollected += Add;  // Define esse método como listener desse evento
    }
    private void OnDisable(){
        Loot.OnLootCollected -= Add;
    }

    // Métodos para adicionar e remover do inventário:
    public void Add(ItemData itemData){
        if(inventory.Count >= 5){
            Debug.Log("Chegou no limite de 5");
            return;
        }

        // Precisa checar se o item já está no dicionário, ou seja, se já existe outro no Inventário
        if(itemDictionary.TryGetValue(itemData, out InventoryItem item)){// tenta buscar e se encontrar, usa
            item.AddToStack();
            Debug.Log($"{item.itemData.nomeLoot} no inventário: {item.stackSize}");
        }
        else{ // se não, cria um novo item (slot) no inventário
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem); // adiciona na Lista
            itemDictionary.Add(itemData, newItem); // adiciona no Dicionário
            Debug.Log($"Adicionou {itemData.nomeLoot} ao inventário pela primeira vez.");
        }

        // Para disparar o evento OnItemChanged:
        OnItemChanged?.Invoke();
    }

    public void Remove(ItemData itemData){
        if(itemDictionary.TryGetValue(itemData, out InventoryItem item)){
            item.RemoveFromStack();
            // para remover do inventário e do dicionário se for igual a zero:
            if(item.stackSize == 0){
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
            OnItemChanged?.Invoke();
        }
    }
}
