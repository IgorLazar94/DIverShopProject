using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    private void Awake()
    {
        MakeSingleton();
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

    [Space]
    [SerializeField] private int playerDefaultDollars;
    [Space]
    [Header("Default parameter values")]
    [SerializeField][Range(1f, 10f)] private float defaultPlayerSpeedParameter;
    [SerializeField] private ushort defaultMaxPlayerFishInventory;
    [SerializeField] private int defaultHarpoonParameter;
    [SerializeField] private float defaultFOVRadiusParameter;

    [Space]
    [Tooltip("The amount by which the parameter increases when improving")]
    [Header("Parameter update factor")]
    [SerializeField] private float defaultPlayerSpeedParameterFactor;
    [SerializeField] private int defaultMaxPlayerFishInventoryFactor;
    [SerializeField] private int defaultHarpoonParameterFactor;
    [SerializeField] private float defaultFOVRadiusParameterFactor;

    [Space]
    [Header("Prices for player upgrades")]
    [SerializeField] private int parameterPriceLevel_1;
    [SerializeField] private int parameterPriceLevel_2;
    [SerializeField] private int parameterPriceLevel_3;
    [SerializeField] private int parameterPriceLevel_4;
    [SerializeField] private int parameterPriceLevel_5;

    [Space]
    [Tooltip("in seconds")][SerializeField] private float timeToVisitCustomers;

    [Space]
    [Header("Prices to unblock receipe")]
    [SerializeField] private int priceToUnblockSandwitch;
    [SerializeField] private int priceToUnblockFishburger;

    [Space]
    [Header("Product payment from costumer")]
    [SerializeField] private ushort friedFishPrice;
    [SerializeField] private ushort sandwichPrice;
    [SerializeField] private ushort fishburgerPrice;

    public int GetPlayerDefaultDollars()
    {
        return playerDefaultDollars;
    }
    //Parameters
    public float GetPlayerSpeedParameter()
    {
        return defaultPlayerSpeedParameter;
    }
    public ushort GetMaxPlayerFishInventory()
    {
        return defaultMaxPlayerFishInventory;
    }
    public int GetDefaultHarpoonLevel()
    {
        return defaultHarpoonParameter;
    }
    public float GetDefaultPlayerFOV()
    {
        return defaultFOVRadiusParameter;
    }
    // Parameter prices
    public int GetParameterPriceLevelOne()
    {
        return parameterPriceLevel_1;
    }
    public int GetParameterPriceLevelTwo()
    {
        return parameterPriceLevel_2;
    }
    public int GetParameterPriceLevelThree()
    {
        return parameterPriceLevel_3;
    }
    public int GetParameterPriceLevelFour()
    {
        return parameterPriceLevel_4;
    }
    public int GetParameterPriceLevelFive()
    {
        return parameterPriceLevel_5;
    }
    //Customers
    public float GetTimeToVisitCustomers()
    {
        return timeToVisitCustomers;
    }
    //Price to unblock
    public int GetPriceToUnblockSandwitch()
    {
        return priceToUnblockSandwitch;
    }
    public int GetPriceToUnblockFishburger()
    {
        return priceToUnblockFishburger;
    }
    // Product price
    public ushort GetProductPriceFriedFish()
    {
        return friedFishPrice;
    }
    public ushort GetProductPriceSandwich()
    {
        return sandwichPrice;
    }
    public ushort GetProductPriceFishburger()
    {
        return fishburgerPrice;
    }
    // Factors
    public float GetPlayerSpeedFactor()
    {
        return defaultPlayerSpeedParameterFactor;
    }
    public int GetPlayerMaxInventoryFactor()
    {
        return defaultMaxPlayerFishInventoryFactor;
    }
    public int GetHarpoonFactor()
    {
        return defaultHarpoonParameterFactor;
    }
    public float GetPlayerFOVFactor()
    {
        return defaultFOVRadiusParameterFactor;
    }

}
