using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfBuild
{
    Kitchen,
    Shop
}

public abstract class GenericBuild : MonoBehaviour
{
    [field: SerializeField] private TypeOfBuild typeOfBuild;
    [field: SerializeField] public PlayerInventoryModel playerInventory { get; private set; }
    [SerializeField] protected Transform playerPos;
    [SerializeField] protected GameObject receivePoint;
    [SerializeField] protected GameObject productPoint;
    private bool isPlayerClose = false;
    private bool isPlayerFar = false;

    private void Update()
    {
        CheckPlayerDistance();
    }

    private void CheckPlayerDistance()
    {
        float distance = (playerPos.position - transform.position).magnitude;
        if (distance < 5f && !isPlayerClose)
        {
            isPlayerClose = true;
            isPlayerFar = false;
            ActivateBuild();
        }
        else if (distance >= 5f && !isPlayerFar)
        {
            isPlayerFar = true;
            isPlayerClose = false;
            DeactivateBuild();
        }
    }

    private void ActivateBuild()
    {
        receivePoint.SetActive(true);
        productPoint.SetActive(true);
        receivePoint.transform.DOScale(Vector3.one, 0.5f);
        productPoint.transform.DOScale(Vector3.one, 0.5f);
    }

    private void DeactivateBuild()
    {
        receivePoint.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => receivePoint.SetActive(false));
        productPoint.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => productPoint.SetActive(false));
    }
}
