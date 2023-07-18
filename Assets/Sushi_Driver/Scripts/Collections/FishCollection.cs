using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollection : MonoBehaviour
{
    [field: SerializeField] public GameObject FishA { get; private set; }
    [field: SerializeField] public GameObject FishB { get; private set; }
    [field: SerializeField] public GameObject FishC { get; private set; }


    [field: SerializeField] public Sprite FishA_Sprite { get; private set; }
    [field: SerializeField] public Sprite FishB_Sprite { get; private set; }
    [field: SerializeField] public Sprite FishC_Sprite { get; private set; }

}
