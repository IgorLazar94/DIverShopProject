using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectTest : MonoBehaviour
{
    private void Start()
    {
        var allFishInfo = Resources.LoadAll<FishConfig>("ScriptableObjects");
        foreach (var item in allFishInfo)
        {
            //Debug.Log(item.price + item.name);
        }
    }









}
