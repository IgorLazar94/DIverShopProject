using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    private int playerLevel = 1;
    public static bool isBusyHands {get; set;}

    private void Start()
    {
        isBusyHands = false;
    }
}
