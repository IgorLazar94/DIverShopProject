using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugZone : MonoBehaviour
{
    public static Action OnEnterDebug;
    public static Action OnExitDebug;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerTrigger playerTrigger))
        {
            OnEnterDebug.Invoke();
        }
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Exit()
    {
        OnExitDebug.Invoke();
    }
}
