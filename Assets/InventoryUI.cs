using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    public Transform itemSlotContainer;
    public GameObject itemDataPanel; // Ativado ou desativado de acordo com a seleção
    public Image[] itemImages;
    public TextMeshProUGUI[] itemStackSizes;

    public Image spriteImage;
    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI infoLabel;

    private Inventory inventory;
    private int selectedItemIndex = -1;
    public GameObject pauseFirstSelected;

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

                int index = i; // Armazena o valor atual de 'i' em uma variável local para que possa ser acessada corretamente dentro dos eventos

                // Adiciona um evento de passagem do mouse (ponteiro sobre o item)
                itemImages[i].GetComponent<Button>().onClick.AddListener(() => SelectItem(index));
            }
            else{
                itemImages[i].gameObject.SetActive(false);
                itemStackSizes[i].gameObject.SetActive(false);
            }
        }
        EventSystem.current.SetSelectedGameObject(pauseFirstSelected);
    }

    public void SelectItem(int index){
        if (index >= 0 && index < inventory.inventory.Count){
            selectedItemIndex = index;
            ItemData selectedItemData = inventory.inventory[selectedItemIndex].itemData;
            UpdateItemPanel(selectedItemData);
            itemDataPanel.SetActive(true);
        }
        else{
            selectedItemIndex = -1;
            ClearItemPanel();
        }
    }
    private void UpdateItemPanel(ItemData itemData){
        spriteImage.sprite = itemData.spriteLoot;
        nameLabel.text = itemData.nomeLoot;
        infoLabel.text = itemData.lootInfo;
    }
    private void ClearItemPanel(){
        spriteImage.sprite = null;
        nameLabel.text = "";
        infoLabel.text = "";
        itemDataPanel.SetActive(false);
    }
}
