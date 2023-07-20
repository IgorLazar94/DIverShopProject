using Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventoryModel : MonoBehaviour
{


    [SerializeField] private PlayerInventoryView view;
    private int maxFishValue;
    private int currentTotalFishQuantity;
    private int currentFishAValue = 0;
    private int currentFishBValue = 0;
    private int currentFishCValue = 0;
    private List<Food> foodInHandList = new List<Food>();
    private int dollarsInInventory = 0;

    private void Start()
    {
        CalculateTotalFishQuantity();
        maxFishValue = GameSettings.Instance.GetMaxPlayerFishInventory();
        PlayerInventoryPresenter.OnMaxFishChanged.Invoke(maxFishValue);
    }
    private void OnEnable()
    {
        PlayerInventoryPresenter.OnMaxFishChanged += SetMaxFishValue;
        PlayerInventoryPresenter.OnCurrentFishChanged += SetCurrentFishValue;
        PlayerInventoryPresenter.OnCurrentDollarsChanged += SetDollarsToInventory;
        //PlayerInventoryPresenter.OnCurrentFishRemoved += RemoveFish;
    }

    private void OnDisable()
    {
        PlayerInventoryPresenter.OnMaxFishChanged -= SetMaxFishValue;
        PlayerInventoryPresenter.OnCurrentFishChanged -= SetCurrentFishValue;
        PlayerInventoryPresenter.OnCurrentDollarsChanged -= SetDollarsToInventory;
        //PlayerInventoryPresenter.OnCurrentFishRemoved -= RemoveFish;
    }

    private void CalculateTotalFishQuantity()
    {
        currentTotalFishQuantity = currentFishAValue + currentFishBValue + currentFishCValue;
    }

    public void SetView(PlayerInventoryView view)
    {
        this.view = view;
    }

    private void SetMaxFishValue(int value)
    {
        if (value <= 0) return;
        maxFishValue = value;
        view.UpdateMaxFishText(value);
    }
    private void SetCurrentFishValue(int value, TypeOfFish typeOfFish)
    {
        //if (currentFishValue + value > maxFishValue) currentFishValue = maxFishValue;

        switch (typeOfFish)
        {
            case TypeOfFish.FishA:
                currentFishAValue += value;
                break;
            case TypeOfFish.FishB:
                currentFishBValue += value;
                break;
            case TypeOfFish.FishC:
                currentFishCValue += value;
                break;
            default:
                Debug.LogWarning("Undefined type Of Fish");
                break;
        }
        CalculateTotalFishQuantity();
        CheckMaxFish();
        view.UpdateCurrentFishText(currentTotalFishQuantity);
    }

    private void CheckMaxFish()
    {
        if (currentTotalFishQuantity >= maxFishValue)
        {
            //currentTotalFishQuantity = maxFishValue;
            PlayerStateController.OnMaxFishBlocked.Invoke(true);
        }
    }

    public int GetCurrentFishAValue()
    {
        return currentFishAValue;
    }

    public int GetCurrentFishBValue()
    {
        return currentFishBValue;
    }
    public int GetCurrentFishCValue()
    {
        return currentFishCValue;
    }

    public void RemoveAllFish()
    {
        currentFishAValue = 0;
        currentFishBValue = 0;
        currentFishCValue = 0;
        CalculateTotalFishQuantity();
        view.UpdateCurrentFishText(currentTotalFishQuantity);
    }

    public void AddNewFoodToPlayerHand(Food food)
    {
        foodInHandList.Add(food);
    }

    public List<Food> SetFoodToShop()
    {
        return foodInHandList;
    }

    public void SetDollarsToInventory(int value)
    {
        dollarsInInventory += value;
        view.UpdateDollarsCount(dollarsInInventory);
    }
}
