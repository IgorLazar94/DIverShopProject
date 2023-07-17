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
        PlayerInventoryPresenter.OnCurrentFishRemoved += RemoveFish;
    }

    private void OnDisable()
    {
        PlayerInventoryPresenter.OnMaxFishChanged -= SetMaxFishValue;
        PlayerInventoryPresenter.OnCurrentFishChanged -= SetCurrentFishValue;
        PlayerInventoryPresenter.OnCurrentFishRemoved -= RemoveFish;
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

    public int GetCurrentFishValue()
    {
        return currentFishValue;
    }

    public void RemoveFish()
    {
        currentFishValue = 0;
        view.UpdateCurrentFishText(currentFishValue);
    }
}
