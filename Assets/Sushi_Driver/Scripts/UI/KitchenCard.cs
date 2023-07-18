using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public enum TypeOfCard
{
    FriedFish,
    SandWich,
    Fishburger
}
public class KitchenCard : MonoBehaviour
{
    [Inject] private FoodCollection foodCollection;
    [Inject] private FishCollection fishCollection;

    [SerializeField] private TypeOfCard typeOfCard;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Image recipeIcon;
    [SerializeField] private TextMeshProUGUI ingredientOneText;
    [SerializeField] private Image ingredientOneIcon;
    [SerializeField] private TextMeshProUGUI ingredientTwoText;
    [SerializeField] private Image ingredientTwoIcon;

    private void Start()
    {
        FillCard();
    }

    private void FillCard()
    {
        switch (typeOfCard)
        {
            case TypeOfCard.FriedFish:
                titleText.text = "FriedFish";
                recipeIcon.sprite = foodCollection.FriedFish_Sprite;
                ingredientOneText.text = 2.ToString();
                ingredientOneIcon.sprite = fishCollection.FishA_Sprite;
                break;
            case TypeOfCard.SandWich:
                titleText.text = "SandWich";
                recipeIcon.sprite = foodCollection.Sandwich_Sprite;
                ingredientOneText.text = 2.ToString();
                ingredientOneIcon.sprite = fishCollection.FishA_Sprite;
                ingredientTwoText.text = 1.ToString();
                ingredientTwoIcon.sprite = fishCollection.FishB_Sprite;
                break;
            case TypeOfCard.Fishburger:
                titleText.text = "Fishburger";
                recipeIcon.sprite = foodCollection.Fishburger_Sprite;
                ingredientOneText.text = 2.ToString();
                ingredientOneIcon.sprite = fishCollection.FishB_Sprite;
                ingredientTwoText.text = 2.ToString();
                ingredientTwoIcon.sprite = fishCollection.FishC_Sprite;
                break;
            default:
                Debug.LogWarning("Undefined type of kitchen card");
                break;
        }
    }
}
