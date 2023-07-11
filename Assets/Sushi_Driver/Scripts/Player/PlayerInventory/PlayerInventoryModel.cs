using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryModel
{
    PlayerInventoryAdapter inventoryAdapter;

    public PlayerInventoryModel (PlayerInventoryAdapter _inventoryAdapter)
    {
        this.inventoryAdapter = _inventoryAdapter;
    }
}
