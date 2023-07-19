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
            CheckWaterGround(other);

            if (other.gameObject.TryGetComponent(out ReceivePoint receivePoint))
            {
                CheckTypeOfReceiveBuild(other.gameObject.transform.parent.gameObject);
            }
            if (other.gameObject.TryGetComponent(out ProductPoint productPoint))
            {
                CheckTypeOfProductBuild(other.gameObject.transform.parent.gameObject);
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagList.Water))
            {
                onTriggerGround.Invoke();
            }
        }

        private void CheckWaterGround(Collider collider)
        {
            if (collider.CompareTag(TagList.Water))
            {
                onTriggerWater.Invoke();
            }

            if (collider.CompareTag(TagList.GroundTriggerZone))
            {
                onPlayClimbAnim.Invoke();

                if (OnTheWaterState.isInWater)
                {
                    onTriggerGround.Invoke();
                }
            }
        }

        private void CheckTypeOfReceiveBuild(GameObject parentBuild)
        {
            if (parentBuild.TryGetComponent(out Kitchen kitchen))
            {
                kitchen.GetFishFromPlayer();
            }
        }

        private void CheckTypeOfProductBuild(GameObject parentBuild)
        {
            if (parentBuild.TryGetComponent(out Kitchen kitchen))
            {
                kitchen.SetFoodForPlayer();
            }
        }
    }
}

