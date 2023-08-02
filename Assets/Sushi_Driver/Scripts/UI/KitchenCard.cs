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
    public bool isReadyToCook { get; private set; }

    [SerializeField] private TypeOfCard typeOfCard;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Image recipeIcon;
    [SerializeField] private TextMeshProUGUI ingredientOneText;
    [SerializeField] private Image ingredientOneIcon;
    [SerializeField] private TextMeshProUGUI ingredientTwoText;
    [SerializeField] private Image ingredientTwoIcon;
    [SerializeField] private Image cookButtonImage;

    [SerializeField] private TextMeshProUGUI blockTitle;
    [SerializeField] private GameObject blockPanel;
    [SerializeField] private TextMeshProUGUI purchasePriceText;
    private int purchasePrice;
    private const string tutorialKey = "TutorialCompleted";
    private bool isCompleteTutorial;


    private void Start()
    {
        FillCard();
        SetDefaultCardBlockState();
        CheckTutorialComplete();
    }

    private void FillCard()
    {
        switch (typeOfCard)
        {
            case TypeOfCard.FriedFish:
                titleText.text = "FriedFish";
                blockTitle.text = "FriedFish";
                recipeIcon.sprite = foodCollection.FriedFish_Sprite;
                ingredientOneText.text = 2.ToString();
                ingredientOneIcon.sprite = fishCollection.FishA_Sprite;
                purchasePrice = 0;
                purchasePriceText.text = purchasePrice.ToString();
                break;
            case TypeOfCard.SandWich:
                titleText.text = "SandWich";
                blockTitle.text = "SandWich";
                recipeIcon.sprite = foodCollection.Sandwich_Sprite;
                ingredientOneText.text = 2.ToString();
                ingredientOneIcon.sprite = fishCollection.FishA_Sprite;
                ingredientTwoText.text = 1.ToString();
                ingredientTwoIcon.sprite = fishCollection.FishB_Sprite;
                purchasePrice = 30;
                purchasePriceText.text = purchasePrice.ToString();
                break;
            case TypeOfCard.Fishburger:
                titleText.text = "Fishburger";
                blockTitle.text = "Fishburger";
                recipeIcon.sprite = foodCollection.Fishburger_Sprite;
                ingredientOneText.text = 2.ToString();
                ingredientOneIcon.sprite = fishCollection.FishB_Sprite;
                ingredientTwoText.text = 2.ToString();
                ingredientTwoIcon.sprite = fishCollection.FishC_Sprite;
                purchasePrice = 50;
                purchasePriceText.text = purchasePrice.ToString();
                break;
            default:
                Debug.LogWarning("Undefined type of kitchen card");
                break;
        }
    }

    public void SwitchButtonReadyStatus(int fishA, int fishB, int fishC)
    {
        if (typeOfCard == TypeOfCard.FriedFish)
        {
            if (fishA >= 2)
            {
                ActivateButton();
            }
            else
            {
                DeactivateButton();
            }
        }
        else if (typeOfCard == TypeOfCard.SandWich)
        {
            if (fishA >= 2 && fishB >= 1)
            {
                ActivateButton();
            }
            else
            {
                DeactivateButton();
            }
        }
        else if (typeOfCard == TypeOfCard.Fishburger)
        {
            if (fishB >= 2 && fishC >= 2)
            {
                ActivateButton();
            }
            else
            {
                DeactivateButton();
            }
        }
    }

    private void ActivateButton()
    {

        isReadyToCook = true;
        cookButtonImage.color = Color.green;
        cookButtonImage.gameObject.GetComponent<Button>().enabled = true;
    }

    private void DeactivateButton()
    {
        isReadyToCook = false;
        cookButtonImage.color = Color.red;
        cookButtonImage.gameObject.GetComponent<Button>().enabled = false;
    }

    public void CheckPriceToUnblock()
    {
        CheckTutorialComplete();
        if (PlayerInventoryModel.dollarsInInventory >= purchasePrice && isCompleteTutorial)
        {
            PlayerInventoryPresenter.OnCurrentDollarsChanged.Invoke(-purchasePrice);
            BlockCard(false);
        }
    }

    private void BlockCard(bool value)
    {
        blockPanel.SetActive(value);
    }

    private void SetDefaultCardBlockState()
    {
        switch (typeOfCard)
        {
            case TypeOfCard.FriedFish:
                BlockCard(false);
                break;
            case TypeOfCard.SandWich:
                BlockCard(true);
                break;
            case TypeOfCard.Fishburger:
                BlockCard(true);
                break;
            default:
                break;
        }
    }

    private void CheckTutorialComplete()
    {
        isCompleteTutorial = PlayerPrefs.GetInt(tutorialKey, 0) == 1;
    }
}
