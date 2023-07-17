using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfBuild
{
    Kitchen,
    Shop
}

public abstract class GenericBuild : MonoBehaviour
{
    [field: SerializeField] private TypeOfBuild typeOfBuild;
    [field: SerializeField] public PlayerInventoryModel playerInventory { get; private set; } 

}
