using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerInventoryPresenter : MonoBehaviour {

    private PlayerInventoryModel model;
    private PlayerInventoryView view;

    public static Action<int> OnMaxFishChanged;
    public static Action<int> OnCurrentFishChanged;
    public void CreateInventory()
    {
        model = new PlayerInventoryModel();
        view = new PlayerInventoryView();

        model.SetView(view);
        view.SetModel(model);
    }

}
