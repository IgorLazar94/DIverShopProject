using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfBuild
{
    Kitchen,
    Shop,
    Training
}

public abstract class GenericBuild : MonoBehaviour
{
    [field: SerializeField] private TypeOfBuild typeOfBuild;
    [field: SerializeField] public PlayerInventoryModel playerInventory { get; private set; }
    [SerializeField] protected GameObject receivePoint;
    [SerializeField] protected GameObject productPoint;
    protected Transform playerPos;
    private bool isPlayerClose = true;
    private bool isActivateBuild = false;

    protected virtual void Start()
    {
        playerPos = playerInventory.gameObject.transform.parent;
        receivePoint.transform.localScale = Vector3.zero;
        productPoint.transform.localScale = Vector3.zero;
        receivePoint.SetActive(false);
        productPoint.SetActive(false);
    }
    protected virtual void FixedUpdate()
    {
        CheckPlayerDistance();
    }

    private void CheckPlayerDistance()
    {
        float distance = (playerPos.position - transform.position).magnitude;
        if (distance < 5f)
        {
            isPlayerClose = true;
        }
        else
        {
            isPlayerClose = false;
        }

        if (isPlayerClose && !isActivateBuild)
        {
            ActivateBuild();
        }
        else if (distance >= 5f && isActivateBuild)
        {
            DeactivateBuild();
        }
    }

    private void ActivateBuild()
    {
        isActivateBuild = true;
        receivePoint.SetActive(true);
        productPoint.SetActive(true);
        receivePoint.transform.DOScale(Vector3.one, 0.2f);
        productPoint.transform.DOScale(Vector3.one, 0.2f);
    }

    private void DeactivateBuild()
    {
        isActivateBuild = false;
        receivePoint.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => receivePoint.SetActive(false));
        productPoint.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => productPoint.SetActive(false));
    }
}
