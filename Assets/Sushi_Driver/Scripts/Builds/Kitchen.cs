using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Kitchen : GenericBuild
{
    //[Inject] private FoodCollection foodCollection;
    [Inject] private UIController controller;
    private int fishOnKitchen = 0;

    public void GetFishFromPlayer()
    {
        fishOnKitchen = playerInventory.GetCurrentFishValue();
        playerInventory.RemoveFish();
    }

}
