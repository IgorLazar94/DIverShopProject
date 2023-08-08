using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateLogic : MonoBehaviour
{
    [SerializeField] private bool isOutSideGate;
    private BoxCollider gateCollider;

    private void Start()
    {
        gateCollider = GetComponentInChildren<BoxCollider>();
        if (!isOutSideGate)
        {
            gateCollider.enabled = false;
        }
    }

    private void OnEnable()
    {
        PlayerAnimatorFXController.OnPlayerHandsFree += OpenGate;
        PlayerAnimatorFXController.OnPlayerHandsTook += CloseGate;
    }

    private void OnDisable()
    {
        PlayerAnimatorFXController.OnPlayerHandsFree -= OpenGate;
        PlayerAnimatorFXController.OnPlayerHandsTook -= CloseGate;
    }

    private void OpenGate()
    {
        gateCollider.enabled = false;
    }

    private void CloseGate()
    {
        gateCollider.enabled = true;
    }
}
