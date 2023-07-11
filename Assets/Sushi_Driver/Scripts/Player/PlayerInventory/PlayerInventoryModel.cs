using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryModel
{
    PlayerInventoryAdapter inventoryAdapter;
    private int fishInInventory;
    private int maxFish = 10;


    public PlayerInventoryModel(PlayerInventoryAdapter _inventoryAdapter)
    {
        this.inventoryAdapter = _inventoryAdapter;
    }

    private void AddFish(int value)
    {
        if (value < 0) return;
        fishInInventory += value;
    }
}
