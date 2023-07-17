using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfFood
{
    FriedFish,
    Sandwich,
    Fishburger
}

public class Food : MonoBehaviour
{
    [SerializeField] TypeOfFood typeOfFood;

}
