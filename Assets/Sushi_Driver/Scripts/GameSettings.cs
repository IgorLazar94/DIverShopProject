using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    [SerializeField][Range(1f, 10f)] private float playerSpeed;
    [SerializeField] private ushort maxPlayerFishInventory;

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }

    public ushort GetMaxPlayerFishInventory()
    {
        return maxPlayerFishInventory;
    }


}
