using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerInventoryPresenter : MonoBehaviour {

    private TypeOfFish typeOfFish;
    private PlayerInventoryModel model;
    private PlayerInventoryView view;

    public static Action<int> OnMaxFishChanged;
    public static Action<int, TypeOfFish> OnCurrentFishChanged;
    public static Action<int> OnCurrentFoodChanged;
    //public static Action OnCurrentFishRemoved;
    public void CreateInventory()
    {
        model = new PlayerInventoryModel();
        view = new PlayerInventoryView();

        model.SetView(view);
        view.SetModel(model);
    }

}
