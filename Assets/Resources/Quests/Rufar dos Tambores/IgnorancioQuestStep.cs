using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class IgnorancioQuestStep : QuestStep
{
    private Inventory Inventory;
    public string itemBusca;


    private void Start()
    {
        Inventory = GameObject.Find("Inventário").GetComponent<Inventory>();
    }

    private void Update()
    {
        HasVinyl();
    }

    private void HasVinyl()
    {
        foreach (InventoryItem item in Inventory.inventory)
        {
            if (item.itemData.nomeLoot == itemBusca)
            {
                FinishQuestStep();
            }
        }
    }
}