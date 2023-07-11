using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryModel
{
    PlayerInventoryAdapter inventoryAdapter = new PlayerInventoryAdapter();

    public PlayerInventoryModel (PlayerInventoryAdapter _inventoryAdapter)
    {
        this.inventoryAdapter = _inventoryAdapter;
    }
}
