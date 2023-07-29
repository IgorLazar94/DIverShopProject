using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyingZone : MonoBehaviour
{
    [SerializeField] private GameObject activateBuild;
    [SerializeField] private TextMeshPro priceText;
    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private GameObject backgroundFullObject;
    [SerializeField] private int price;
    [SerializeField] private string objectName;
    //[SerializeField] private float backgroundWidthSize;

    private void Start()
    {
        //backgroundWidthSize = backgroundFullObject.GetComponent<SpriteRenderer>().size.x;
        priceText.text = price.ToString();
        nameText.text = objectName.ToString();
        activateBuild.SetActive(false);
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
        transform.DOScale(Vector3.zero, 0.25f);
        activateBuild.SetActive(true);
        activateBuild.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutBack).OnComplete(() => Destroy(this.gameObject));
    }
}
