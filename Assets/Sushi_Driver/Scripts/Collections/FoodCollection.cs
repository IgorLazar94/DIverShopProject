using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollection : MonoBehaviour
{
    [field: SerializeField] public GameObject FriedFish { get; private set; }
    [field: SerializeField] public GameObject Sandwich { get; private set; }
    [field: SerializeField] public GameObject Fishburger { get; private set; }

    [field: SerializeField] public Sprite FriedFish_Sprite { get; private set; }
    [field: SerializeField] public Sprite Sandwich_Sprite { get; private set; }
    [field: SerializeField] public Sprite Fishburger_Sprite { get; private set; }
}
