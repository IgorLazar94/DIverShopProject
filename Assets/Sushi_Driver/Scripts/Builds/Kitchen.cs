using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Kitchen : GenericBuild
{
    //[Inject] private UIController ui_Controller;
    [SerializeField] private UIController ui_Controller;

    private int fishAOnKitchen = 0;
    private int fishBOnKitchen = 0;
    private int fishCOnKitchen = 0;

    public void GetFishFromPlayer()
    {
        fishAOnKitchen = playerInventory.GetCurrentFishAValue();
        fishBOnKitchen = playerInventory.GetCurrentFishBValue();
        fishCOnKitchen = playerInventory.GetCurrentFishCValue();
        ui_Controller.UpdateCurrentFishText(fishAOnKitchen, fishBOnKitchen, fishCOnKitchen);
        playerInventory.RemoveAllFish();
        PlayerMovementControl.onPlayerStopped.Invoke();
        ui_Controller.ShowKitchenUI();
    }

}
