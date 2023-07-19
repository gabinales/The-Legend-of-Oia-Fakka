using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour, IColetavel
{
    public static event HandleLootCollected OnLootCollected;
    public delegate void HandleLootCollected (ItemData itemData);

    //mudei aqui para "public". Ass. Victor 08/07/2023 AEON de peixes
    public ItemData lootData;

    public void Collect(){
        Destroy(gameObject);
        OnLootCollected?.Invoke(lootData); // Avisa às classes interessadas, como Inventory.
        AudioManager.instance.PlayOneShot(FMODEvents.instance.lootCollected, this.transform.position);
    }

    public void Initialize(ItemData itemData){
        lootData = itemData;
        GetComponent<SpriteRenderer>().sprite = itemData.spriteLoot;
    }
}


