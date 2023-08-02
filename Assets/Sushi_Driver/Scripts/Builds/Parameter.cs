using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TypeOfParameter
{
    Harpoon,
    Backpack,
    Speed,
    FOV
}
public class Parameter : MonoBehaviour
{
    [SerializeField] private TypeOfParameter typeOfParameter;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Image updateLevelButtonImage;
    [SerializeField] private UIController uIController;
    private int level = 1;
    private int price;

    private string violetHEX = "F359EC";
    private string orangeHEX = "FD9C68";

    private void Start()
    {
        CalculateNewLevelParameters();
    }

    //public void SetUIParentObject(UIController _uiController)
    //{
    //    uIController = _uiController;
    //}

    private void CalculateNewLevelParameters()
    {
        switch (level)
        {
            case 1:
                price = 30;
                levelText.color = Color.grey;
                break;
            case 2:
                price = 50;
                levelText.color = Color.green;
                break;
            case 3:
                price = 80;
                levelText.color = Color.blue;
                break;
            case 4:
                price = 120;
                levelText.color = HexToColor(orangeHEX);
                break;
            case 5:
                price = 180;
                levelText.color = HexToColor(violetHEX);
                break;
            default:
                Debug.LogWarning("Undefined parameter level update");
                break;
        }
        UpdateLevelText();
        uIController.CheckTrainingButtonsActive();
    }

    private void UpdateLevelText()
    {
        levelText.text = "Lvl " + level.ToString();
        costText.text = "Cost: " + price.ToString();
    }

    private Color HexToColor(string hex)
    {
        hex = hex.Replace("#", "");
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte a = 255;
        return new Color32(r, g, b, a);
    }

    public void LevelUpdate()
    {
        Debug.Log(typeOfParameter + "type of parameter update");
        PlayerInventoryPresenter.OnCurrentDollarsChanged(-price);
        level++;
        CalculateNewLevelParameters();
        //CheckReadyUpdateButton();
    }

    public void CheckReadyUpdateButton()
    {
        if (PlayerInventoryModel.dollarsInInventory >= price)
        {
            ActivateButton();
        }
        else
        {
            DeactivateButton();
        }
    }

    private void ActivateButton()
    {

        updateLevelButtonImage.color = Color.green;
        updateLevelButtonImage.gameObject.GetComponent<Button>().enabled = true;
    }

    private void DeactivateButton()
    {
        updateLevelButtonImage.color = Color.red;
        updateLevelButtonImage.gameObject.GetComponent<Button>().enabled = false;
    }
}
