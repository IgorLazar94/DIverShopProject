using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerInventoryAdapter
{
    public PlayerInventoryView playerInventoryView;
    public PlayerInventoryModel playerInventoryModel;

    public static Action onMaxFishValueChanged;
    public PlayerInventoryAdapter (PlayerInventoryView _playerInventoryView)
    {
        this.playerInventoryView = _playerInventoryView;
    }

    public void SetInventoryModel(PlayerInventoryModel _playerInventoryModel)
    {
        playerInventoryModel = _playerInventoryModel;
    }

}
