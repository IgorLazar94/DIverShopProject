using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fish Config", menuName = "Gameplay/New Fish Config")]
public class FishConfig : ScriptableObject
{

    [SerializeField] private GameObject _fishPrefab;
    [SerializeField] private float _price;
    [SerializeField] private byte _availableLevel;
    public GameObject fishPrefab => this._fishPrefab;
    public float price => this._price;
    public byte availableLevel => this._availableLevel;
}
