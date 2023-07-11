using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    PlayerInventoryAdapter inventoryAdapter;
    PlayerInventoryModel inventoryModel;
    PlayerInventoryView inventoryView;

    private void Start()
    {
        inventoryAdapter = new PlayerInventoryAdapter();
        inventoryModel = new PlayerInventoryModel(inventoryAdapter);
        inventoryView = new PlayerInventoryView(inventoryAdapter);
        inventoryAdapter.HelloWorld();
        inventoryAdapter.SetInventoryComponent(inventoryView, inventoryModel);
    }
}
