using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventoryModel : MonoBehaviour {

    [SerializeField] private PlayerInventoryView view;
    private int maxFishValue = 0;
    private int currentFishValue = 0;

    private void Start()
    {
        //maxFishValue = 12; // (!) from GameSettings
        PlayerInventoryPresenter.OnMaxFishChanged.Invoke(12);
    }
    private void OnEnable()
    {
        PlayerInventoryPresenter.OnMaxFishChanged += SetMaxFishValue;
        PlayerInventoryPresenter.OnCurrentFishChanged += SetCurrentFishValue;
    }

    private void OnDisable()
    {
        PlayerInventoryPresenter.OnMaxFishChanged -= SetMaxFishValue;
        PlayerInventoryPresenter.OnCurrentFishChanged -= SetCurrentFishValue;
    }

    public void SetView(PlayerInventoryView view)
    {
        this.view = view;
    }

    private void SetMaxFishValue(int value)
    {
        if (value <= 0) return;
        maxFishValue = value;
        view.UpdateMaxFishText(value);
    }
    private void SetCurrentFishValue(int value)
    {
        //if (currentFishValue + value > maxFishValue) currentFishValue = maxFishValue;

        currentFishValue += value;
        view.UpdateCurrentFishText(currentFishValue);
    }
}
