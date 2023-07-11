using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryView : MonoBehaviour
{
    PlayerInventoryAdapter inventoryAdapter;

    public PlayerInventoryView (PlayerInventoryAdapter inventoryAdapter)
    {
        this.inventoryAdapter = inventoryAdapter;
    }

    public void StartTestMessage()
    {
        Debug.Log("WORLD");
    }
}
