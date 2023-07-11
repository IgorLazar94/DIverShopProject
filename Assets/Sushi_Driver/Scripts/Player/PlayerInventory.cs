using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    PlayerInventoryAdapter inventoryAdapter;
    PlayerInventoryModel inventoryModel;
    [SerializeField] PlayerInventoryView inventoryView;

    private void Start()
    {
        inventoryAdapter = new PlayerInventoryAdapter(inventoryView);
        inventoryModel = new PlayerInventoryModel(inventoryAdapter);
        inventoryAdapter.SetInventoryModel(inventoryModel);
        Debug.Log(inventoryView + " view");
        Debug.Log(inventoryModel + " model");
        Debug.Log(inventoryAdapter + " adapter");

    }
}
