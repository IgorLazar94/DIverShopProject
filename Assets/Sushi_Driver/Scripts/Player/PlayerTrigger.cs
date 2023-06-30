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
        public static Action onPlayClimbAnim;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagList.Water))
            {
                onTriggerWater.Invoke();
            }

            if (other.CompareTag(TagList.GroundTriggerZone))
            {
                Debug.LogWarning("!");
                onPlayClimbAnim.Invoke();
                onTriggerGround.Invoke();
            }

        }
        //private void OnTriggerExit(Collider other)
        //{
        //    if (other.CompareTag(TagList.Water))
        //    {
        //        onTriggerGround.Invoke();
        //    }
        //}


    }
}

