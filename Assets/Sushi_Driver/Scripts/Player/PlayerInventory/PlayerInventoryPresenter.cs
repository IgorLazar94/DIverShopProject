using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerInventoryPresenter : MonoBehaviour
{
    public static Action<int> OnMaxFishChanged;
    public static Action<int, TypeOfFish> OnCurrentFishChanged;
    public static Action<int> OnCurrentFoodChanged;
    public static Action<int> OnCurrentDollarsChanged;
    private PlayerInventoryView view;
    private PlayerInventoryModel model;
    public void CreateInventory()
    {
        model = new PlayerInventoryModel();
        view = new PlayerInventoryView();

        model.SetView(view);
    }
}
