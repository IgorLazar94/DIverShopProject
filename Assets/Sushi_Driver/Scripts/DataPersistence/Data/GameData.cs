using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool isNewGame;
    public int dollars;
    // complete buildings
    public bool isKitchenCompleteData;
    public bool isShopCompleteData;
    public bool isTrainingCompleteData;
    // parameters
    public float currentPlayerSpeedParameter;
    public int currentMaxPlayerFishInventoryParameter;
    public int currentHarpoonParameter;
    public float currentFOVRadiusParameter;
    // parameters level update
    public int currentLevelSpeed;
    public int currentLevelMaxInventory;
    public int currentLevelHarpoon;
    public int currentLevelFOV;
    // receipes
    public bool isUnblockFriedFishReceipe;
    public bool isUnblockSandwichReceipe;
    public bool isUnblockFishburgerReceipe;

    public GameData()
    {
        // Default settings
        this.isNewGame = true;
        this.dollars = 200;

        this.isKitchenCompleteData = false;
        this.isShopCompleteData = false;
        this.isTrainingCompleteData = false;

        this.currentPlayerSpeedParameter = 4;
        this.currentMaxPlayerFishInventoryParameter = 5;
        this.currentHarpoonParameter = 1;
        this.currentFOVRadiusParameter = 4;

        this.currentLevelSpeed = 1;
        this.currentLevelMaxInventory = 1;
        this.currentLevelHarpoon = 1;
        this.currentLevelFOV = 1;

        this.isUnblockFriedFishReceipe = true;
        this.isUnblockSandwichReceipe = false;
        this.isUnblockFishburgerReceipe = false;

    }


}
