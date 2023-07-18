using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Kitchen : GenericBuild
{
    //[Inject] private UIController ui_Controller;
    [SerializeField] private UIController ui_Controller;

    private int fishOnKitchen = 0;

    public void GetFishFromPlayer()
    {
        fishOnKitchen = playerInventory.GetCurrentFishValue();
        //playerInventory.RemoveFish();
    }

}
