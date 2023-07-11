using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [SerializeField] private Transform playerPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out GenericFish fish))
        {
            fish.StartCoroutine(fish.StartRunFromPlayer(playerPos));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out GenericFish fish))
        {
            fish.StopRunFromPlayer();
        }
    }
}
