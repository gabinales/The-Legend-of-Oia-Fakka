using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour, IColetavel
{
    public static event HandleLootCollected OnLootCollected;
    public delegate void HandleLootCollected (ItemData itemData);
    private ItemData lootData;

    public void Collect(){
        Destroy(gameObject);
        OnLootCollected?.Invoke(lootData); // Avisa às classes interessadas, como Inventory.
    }

    public void Initialize(ItemData itemData){
        lootData = itemData;
        GetComponent<SpriteRenderer>().sprite = itemData.spriteLoot;
    }
}


