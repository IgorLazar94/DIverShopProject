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

public class BuyingZone : MonoBehaviour, IDataPersistence
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
        DefaultDeactivateBuild();
        LoadIsBuildingComplete();
        priceText.text = price.ToString();
        nameText.text = objectName.ToString();
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
        switch (typeOfBuyingZone)
        {
            case TypeOfBuyingZone.Kitchen:
                if (isKitchenComplete)
                {
                    FastBuildActivation();
                }
                break;
            case TypeOfBuyingZone.Shop:
                if (isShopComplete)
                {
                    FastBuildActivation();
                }
                break;
            case TypeOfBuyingZone.Training:
                if (isTrainingComplete)
                {
                    FastBuildActivation();
                }
                break;
            default:
                break;
        }
    }

    private void SaveIsBuildingComplete()
    {
        switch (typeOfBuyingZone)
        {
            case TypeOfBuyingZone.Kitchen:
                isKitchenComplete = true;
                DataPersistenceManager.Instance.SaveGame();
                break;
            case TypeOfBuyingZone.Shop:
                isShopComplete = true;
                DataPersistenceManager.Instance.SaveGame();
                break;
            case TypeOfBuyingZone.Training:
                isTrainingComplete = true;
                DataPersistenceManager.Instance.SaveGame();
                break;
            default:
                break;
        }
    }

    public void LoadData(GameData gameData)
    {
        this.isKitchenComplete = gameData.isKitchenCompleteData;
        this.isShopComplete = gameData.isShopCompleteData;
        this.isTrainingComplete = gameData.isTrainingCompleteData;
    }

    public void SaveData(ref GameData gameData)
    {
        if (typeOfBuyingZone == TypeOfBuyingZone.Kitchen)
        {
            gameData.isKitchenCompleteData = this.isKitchenComplete;
        }
        else if (typeOfBuyingZone == TypeOfBuyingZone.Shop)
        {
            gameData.isShopCompleteData = this.isShopComplete;
        }
        else if (typeOfBuyingZone == TypeOfBuyingZone.Training)
        {
            gameData.isTrainingCompleteData = this.isTrainingComplete;
        }
    }
}
