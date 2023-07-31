using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventoryView : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI currentFishText;
    [SerializeField] private TextMeshProUGUI maxFishText;
    [SerializeField] private TextMeshProUGUI dollarsCountText;
    //private PlayerInventoryModel model;
    //private int lastFishCount = 0;
    //private int maxFishCount = 0;

    //public void SetModel(PlayerInventoryModel _model)
    //{
    //    this.model = _model;
    //}

    public void UpdateCurrentFishText(int value)
    {
        currentFishText.text = value.ToString();
        //if (lastFishCount != 0)
        //{
        //    //currentFishText.text = (value + lastFishCount).ToString();
        //}
        //else
        //{
        //}
        //lastFishCount += value;
    }

    public void UpdateMaxFishText(int value)
    {
        maxFishText.text = value.ToString();
    }

    public void UpdateDollarsCount(int value)
    {
        dollarsCountText.text = value.ToString();
    }

}
