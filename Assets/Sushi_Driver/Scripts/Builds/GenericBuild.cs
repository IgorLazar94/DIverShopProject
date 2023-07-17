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
    [SerializeField] private Transform playerInteractionPoint;
    [SerializeField] private Transform finishedProductPoint;
}
