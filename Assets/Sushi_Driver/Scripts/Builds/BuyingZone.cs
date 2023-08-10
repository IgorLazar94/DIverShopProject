using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public enum TypeOfBuyingZone
{
    Kitchen,
    Shop,
    Training
}

public class BuyingZone : MonoBehaviour
{
    [SerializeField] TypeOfBuyingZone typeOfBuyingZone;
    [SerializeField] private GameObject activateBuild;
    [SerializeField] private TextMeshPro priceText;
    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private int price;
    [SerializeField] private string objectName;
    private bool isKitchenComplete = false;
    private bool isShopComplete = false;
    private bool isTrainingComplete = false;

    private void Start()
    {
        LoadIsBuildingComplete();
        priceText.text = price.ToString();
        nameText.text = objectName.ToString();
        DefaultDeactivateBuild();
    }

    private void DefaultDeactivateBuild()
    {
        switch (typeOfBuyingZone)
        {
            case TypeOfBuyingZone.Kitchen:
                if (!isKitchenComplete)
                {
                    Debug.Log("Default deactivate kitchen");
                    activateBuild.SetActive(false);
                }
                break;
            case TypeOfBuyingZone.Shop:
                if (!isShopComplete)
                {
                    Debug.Log("Default deactivate shop");
                    activateBuild.SetActive(false);
                }
                break;
            case TypeOfBuyingZone.Training:
                if (!isTrainingComplete)
                {
                    Debug.Log("Default deactivate training");
                    activateBuild.SetActive(false);
                }
                break;
            default:
                break;
        }
    }

    public void CheckPlayerMoney()
    {
        if (PlayerInventoryModel.dollarsInInventory >= price)
        {
            PlayerInventoryPresenter.OnCurrentDollarsChanged.Invoke(-price);
            ActivateBuild();
        }
    }

    private void ActivateBuild()
    {
        activateBuild.transform.DOScale(Vector3.zero, 0.0f);
        activateBuild.SetActive(true);
        Debug.Log("activate build" + activateBuild.gameObject.name);
        transform.DOScale(Vector3.zero, 0.25f);
        activateBuild.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutBack).OnComplete(() => Destroy(this.gameObject));
        SaveIsBuildingComplete();
    }

    private void FastBuildActivation()
    {
        activateBuild.SetActive(true);
        Destroy(gameObject);
    }

    private void LoadIsBuildingComplete()
    {
        LoadBuildStatus();
        switch (typeOfBuyingZone)
        {
            case TypeOfBuyingZone.Kitchen:
                if (isKitchenComplete)
                {
                    Debug.Log("is buying zone Kitchen destroy from start");
                    FastBuildActivation();
                }
                break;
            case TypeOfBuyingZone.Shop:
                if (isShopComplete)
                {
                    Debug.Log("is buying zone Shop destroy from start");
                    FastBuildActivation();
                }
                break;
            case TypeOfBuyingZone.Training:
                if (isTrainingComplete)
                {
                    Debug.Log("is buying zone Training destroy from start");
                    FastBuildActivation();
                }
                break;
            default:
                break;
        }
    }

    private void LoadBuildStatus()
    {
        SaveData saveData = SaveLoadManager.LoadData();
        if (saveData != null)
        {
            isKitchenComplete = saveData.isKitchenBuildComplete;
            isShopComplete = saveData.isShopBuildComplete;
            isTrainingComplete = saveData.isTrainingBuildComplete;
        }
    }

    private void SaveIsBuildingComplete()
    {
        switch (typeOfBuyingZone)
        {
            case TypeOfBuyingZone.Kitchen:
                isKitchenComplete = true;
                break;
            case TypeOfBuyingZone.Shop:
                isShopComplete = true;
                break;
            case TypeOfBuyingZone.Training:
                isTrainingComplete = true;
                break;
            default:
                break;
        }
        SaveBuildStatus();
    }

    private void SaveBuildStatus()
    {
        SaveData saveData = new SaveData
        {
            isKitchenBuildComplete = this.isKitchenComplete,
            isShopBuildComplete = this.isShopComplete,
            isTrainingBuildComplete = this.isTrainingComplete,
        };
        SaveLoadManager.SaveData(saveData);
    }
}
