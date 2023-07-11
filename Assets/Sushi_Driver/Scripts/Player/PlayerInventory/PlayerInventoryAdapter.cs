using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryAdapter : MonoBehaviour
{
    public static PlayerInventoryAdapter Instance;

    PlayerInventoryView inventoryView;
    PlayerInventoryModel inventoryModel;

    private void Awake()
    {
        MakeSingleton();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventoryView.StartTestMessage();
            Debug.Log(inventoryView);
        }
    }

    private void MakeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void HelloWorld()
    {
        Debug.Log("Hello");
    }

    public void SetInventoryComponent(PlayerInventoryView _inventoryView, PlayerInventoryModel _inventoryModel)
    {
        inventoryModel = _inventoryModel;
        inventoryView = _inventoryView;
    }

}
