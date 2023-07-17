using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : GenericBuild
{
    [SerializeField] private FoodCollection foodCollection;
    private int fishOnKitchen = 0;

    public void GetFishFromPlayer()
    {
        fishOnKitchen = playerInventory.GetCurrentFishValue();
        playerInventory.RemoveFish();
    }

}
