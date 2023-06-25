using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform itemSlotContainer;
    public Image[] itemImages;
    public TextMeshProUGUI[] itemStackSizes;
    //public Text[] itemStackSizes;

    private Inventory inventory;

    private void Start(){
        inventory = GetComponent<Inventory>();
        inventory.OnItemChanged += UpdateInventoryUI; // Registra o método UpdateInventoryUI como listener do evento OnItemAdded
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI(){
        int itemCount = inventory.inventory.Count;

        for(int i = 0; i < itemImages.Length; i++){
            if(i < itemCount){
                InventoryItem item = inventory.inventory[i];
                itemImages[i].sprite = item.itemData.spriteLoot;
                itemImages[i].gameObject.SetActive(true);
                itemStackSizes[i].text = item.stackSize.ToString();
                itemStackSizes[i].gameObject.SetActive(true);
            }
            else{
                itemImages[i].gameObject.SetActive(false);
                itemStackSizes[i].gameObject.SetActive(false);
            }
        }
    }
}
