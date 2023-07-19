using DG.Tweening;
using Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Shop : GenericBuild
{
    [SerializeField] private Transform foodContainer;
    [SerializeField] private GameObject dollarContainer;
    private List<Food> foodInShop = new List<Food>();
    private Vector3 foodPos;
    // Food pack
    private float offsetXFood = 0.5f;
    private float offsetYFood = 0.2f;
    private float offsetZFood = 0.5f;
    private int widthLimitFood = 3;
    private int lengthLimitFood = 4;
    private float lengthFood = 0;
    private float heightFood = 0;
    private float widthFood = 0;
    // DollarPack
    private float offsetXDollar = 0.5f;
    private float offsetYDollar = 0.2f;
    private float offsetZDollar = 0.5f;
    private int widthLimitDollar = 3;
    private int lengthLimitDollar = 4;
    private float lengthDollar = 0;
    private float heightDollar = 0;
    private float widthDollar = 0;
    public void GetFoodFromPlayer()
    {
        foodInShop = playerInventory.SetFoodToShop();
        PlayerLogic.isBusyHands = true;
        float counter = 0.2f;
        foreach (var food in foodInShop)
        {
            counter += 0.1f;
            foodPos = CalculateFoodPosition();
            food.transform.parent = null;
            food.transform.DOJump(foodPos, 3f, 1, counter).OnComplete(() => food.transform.parent = foodContainer);
            CalculateNewPosition();
        }
        PlayerAnimatorFXController.OnPlayerHandsFree.Invoke();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateFoodPosition();
        }
    }

    private Vector3 CalculateFoodPosition()
    {
        Vector3 spawnPosition = foodContainer.position + new Vector3(lengthFood * offsetXFood, widthFood * offsetYFood, -(heightFood * offsetZFood));
        return spawnPosition;
    }

    private void CalculateNewPosition()
    {
        lengthFood++;
        if (lengthFood > widthLimitFood)
        {
            lengthFood = 0;
            heightFood++;
            if (heightFood > lengthLimitFood)
            {
                lengthFood = 0;
                heightFood = 0;
                widthFood++;
            }
        }
    }

    public void SetProductToCustomer(CustomerBehaviour customer)
    {
        if (foodInShop.Count > 0)
        {
            Food lastFood = foodInShop[foodInShop.Count - 1];
            customer.SetFood(lastFood);
            lastFood.transform.DOMove(customer.transform.position, 0.25f).OnComplete(() => Transaktion(customer, lastFood));
            if (foodInShop.Count == 1)
            {
                ResetFoodPosition();
            }
        }
        else
        {
            customer.NoneFood();
        }
    }

    private void ResetFoodPosition()
    {
        lengthFood = 0f;
        widthFood = 0f;
        heightFood = 0f;
    }

    private void Transaktion(CustomerBehaviour customer, Food food)
    {
        foodInShop.Remove(food);
        Destroy(food.gameObject);
        customer.GiveMoney(food);
    }

    public void ChooseDollarPos(Transform dollar)
    {

    }
}
