using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryView : MonoBehaviour
{
    PlayerInventoryAdapter inventoryAdapter = new PlayerInventoryAdapter();

    public PlayerInventoryView (PlayerInventoryAdapter inventoryAdapter)
    {
        this.inventoryAdapter = inventoryAdapter;
    }

    public void StartTestMessage()
    {
        Debug.Log("WORLD");
    }
}
