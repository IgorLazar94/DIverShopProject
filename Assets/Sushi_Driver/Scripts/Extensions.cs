using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Vector3 Reverse (this Vector3 currentVector)
    {
        Vector3 reverseDirection = -currentVector;
        return reverseDirection;
    }
}
