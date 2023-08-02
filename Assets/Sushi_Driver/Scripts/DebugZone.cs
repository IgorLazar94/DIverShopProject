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
            Debug.Log("IN");
            OnEnterDebug.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerTrigger playerTrigger))
        {
            Debug.Log("OUT");
            OnExitDebug.Invoke();
        }
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Save()
    {

    }

    public void Load()
    {

    }
}
