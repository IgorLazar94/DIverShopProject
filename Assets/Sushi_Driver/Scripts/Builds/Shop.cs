using DG.Tweening;
using Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Shop : GenericBuild
{
    private List<Food> foodInShop = new List<Food>();
    [SerializeField] private Transform foodContainer;
    [SerializeField] private GameObject testCube;

    private Vector3 foodPos;
    private float offsetXFood = 0.5f;
    private float offsetYFood = 0.2f;
    private float offsetZFood = 0.5f;
    private int widthLimitFood = 3;
    private int lengthLimitFood = 4;
    private float lengthFood = 0;
    private float heightFood = 0;
    private float widthFood = 0;
    public void GetFoodFromPlayer()
    {
        foodInShop = playerInventory.SetFoodToShop();
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

}
