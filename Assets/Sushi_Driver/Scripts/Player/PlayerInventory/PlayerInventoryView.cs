using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventoryView : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI currentFishText;
    [SerializeField] private TextMeshProUGUI maxFishText;
    [SerializeField] private TextMeshProUGUI dollarsCountText;

    public void UpdateCurrentFishText(int value)
    {
        currentFishText.text = value.ToString();
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
