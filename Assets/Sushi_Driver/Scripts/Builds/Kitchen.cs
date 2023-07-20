using DG.Tweening;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Kitchen : GenericBuild
{
    //[Inject] private UIController ui_Controller;
    [Inject] private FoodCollection foodCollection;
    [SerializeField] private UIController ui_Controller;
    [SerializeField] private Transform spawnProductPoint;
    [SerializeField] private Transform foodContainer;
    private int fishAOnKitchen = 0;
    private int fishBOnKitchen = 0;
    private int fishCOnKitchen = 0;
    private float lastProductHeight = 0;
    private List<Food> readyFoodList = new List<Food>();
    private TypeOfFood currentTypeOfFood;

    protected override void Start()
    {
        base.Start();
        ui_Controller.SetKitchen(this);
    }

    public void GetFishFromPlayer()
    {
        fishAOnKitchen += playerInventory.GetCurrentFishAValue();
        fishBOnKitchen += playerInventory.GetCurrentFishBValue();
        fishCOnKitchen += playerInventory.GetCurrentFishCValue();
        ui_Controller.UpdateCurrentFishText(fishAOnKitchen, fishBOnKitchen, fishCOnKitchen);
        playerInventory.RemoveAllFish();
        PlayerMovementControl.onPlayerStopped.Invoke();
        ui_Controller.ShowKitchenUI();
    }

    public void CookedFriedFish()
    {
        currentTypeOfFood = TypeOfFood.FriedFish;
        FriedFishReceipe friedFishReceipe = new FriedFishReceipe(fishAOnKitchen, fishBOnKitchen, fishCOnKitchen);
        fishAOnKitchen = friedFishReceipe.CookIngredientOne();
        fishBOnKitchen = friedFishReceipe.CookIngredientTwo();
        fishCOnKitchen = friedFishReceipe.CookIngredientThree();
        ui_Controller.UpdateCurrentFishText(fishAOnKitchen, fishBOnKitchen, fishCOnKitchen);
        CreateFood();
    }

    public void CookedSandwich()
    {
        currentTypeOfFood = TypeOfFood.Sandwich;
        SandwichRecipe sandwichRecipe = new SandwichRecipe(fishAOnKitchen, fishBOnKitchen, fishCOnKitchen);
        fishAOnKitchen = sandwichRecipe.CookIngredientOne();
        fishBOnKitchen = sandwichRecipe.CookIngredientTwo();
        fishCOnKitchen = sandwichRecipe.CookIngredientThree();
        ui_Controller.UpdateCurrentFishText(fishAOnKitchen, fishBOnKitchen, fishCOnKitchen);
        CreateFood();
    }

    public void CookedFishburger()
    {
        currentTypeOfFood = TypeOfFood.Fishburger;
        FishburgerRecipe fishburgerRecipe = new FishburgerRecipe(fishAOnKitchen, fishBOnKitchen, fishCOnKitchen);
        fishAOnKitchen = fishburgerRecipe.CookIngredientOne();
        fishBOnKitchen = fishburgerRecipe.CookIngredientTwo();
        fishCOnKitchen = fishburgerRecipe.CookIngredientThree();
        ui_Controller.UpdateCurrentFishText(fishAOnKitchen, fishBOnKitchen, fishCOnKitchen);
        CreateFood();
    }


    private void CreateFood()
    {
        var food = ChooseFoodType();
        readyFoodList.Add(food.GetComponent<Food>());
        food.transform.position = new Vector3(spawnProductPoint.position.x,
                                               spawnProductPoint.position.y + (lastProductHeight * 0.8f),
                                               spawnProductPoint.position.z);
        food.transform.parent = foodContainer;
        lastProductHeight += food.GetComponent<BoxCollider>().bounds.size.y;
        food.GetComponent<BoxCollider>().enabled = false;
    }

    private GameObject ChooseFoodType()
    {
        switch (currentTypeOfFood)
        {
            case TypeOfFood.FriedFish:
                return Instantiate(foodCollection.FriedFish);
            case TypeOfFood.Sandwich:
                return Instantiate(foodCollection.Sandwich);
            case TypeOfFood.Fishburger:
                return Instantiate(foodCollection.Fishburger);
            default:
                Debug.LogWarning("Undefined type of food on kitchen");
                return null;
        }
    }

    public void SetFoodForPlayer()
    {
        PlayerLogic.isBusyHands = true;
        Vector3 defaultSpawnPos = foodContainer.position;
        foodContainer.DOJump(playerInventory.transform.position, 3f, 1, 0.5f).OnComplete(() => RemoveReadyFood(defaultSpawnPos));
    }

    private void RemoveReadyFood(Vector3 _defaultSpawnPos)
    {
        PlayerAnimatorFXController.OnPlayerHandsTook.Invoke();
        foreach (var food in readyFoodList)
        {
            playerInventory.AddNewFoodToPlayerHand(food);
            float foodHeight = food.transform.position.y;
            food.transform.parent = playerInventory.transform;
            food.transform.localPosition = new Vector3(0f, foodHeight - 0.75f, 0.7f);
        }
        readyFoodList.Clear();
        lastProductHeight = 0f;
        foodContainer.position = _defaultSpawnPos;
    }
}
