using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int dollars;
    public bool isKitchenCompleteData;
    public bool isShopCompleteData;
    public bool isTrainingCompleteData;

    public GameData()
    {
        this.dollars = 200;
        this.isKitchenCompleteData = false;
        this.isShopCompleteData = false;
        this.isTrainingCompleteData = false;

    }


}
