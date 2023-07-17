using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivePoint : MonoBehaviour
{
    private Transform playerZoneSprite;

    private void Start()
    {
        playerZoneSprite = gameObject.GetComponentInChildren<SpriteRenderer>().transform;
    }

    private void FixedUpdate()
    {
        playerZoneSprite.Rotate(0f, 0f, 50f * Time.deltaTime);
    }
}
