using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Player
{
    public class PlayerTrigger : MonoBehaviour
    {
        public static Action onTriggerGround;
        public static Action onTriggerWater;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagList.Water))
            {
                Debug.Log("Find Water");
                onTriggerWater.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagList.Water))
            {
                onTriggerGround.Invoke();
            }
        }



    }
}

