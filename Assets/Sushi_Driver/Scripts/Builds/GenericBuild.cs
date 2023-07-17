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
    [SerializeField] private TypeOfBuild typeOfBuild;
    [SerializeField] private PlayerInventoryModel inventoryModel;

}
