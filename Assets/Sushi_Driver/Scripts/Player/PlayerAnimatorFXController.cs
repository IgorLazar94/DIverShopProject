using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAnimatorFXController : MonoBehaviour
    {
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            UI.JoyStickInput.isHasInputDirection += StartRunAnimation;
            UI.JoyStickInput.isNotHasInputDirection += StopRunAnimation;
        }

        private void OnDisable()
        {
            UI.JoyStickInput.isHasInputDirection -= StartRunAnimation;
            UI.JoyStickInput.isNotHasInputDirection -= StopRunAnimation;

        }

        private void StartRunAnimation(Vector3 direction)
        {
            // particles in direction
            animator.SetBool(AnimParameters.isRun, true);
        }

        private void StopRunAnimation()
        {
            // stop particles in direction
            animator.SetBool(AnimParameters.isRun, false);
        }

        private void StartBusyHands()
        {
            animator.SetBool(AnimParameters.isBusyHands, true);
        }

        private void StopBusyHands()
        {
            animator.SetBool(AnimParameters.isBusyHands, false);
        }

        private void Climbes ()
        {
            animator.SetTrigger(AnimParameters.Climbes);
        }

        private void Dives()
        {
            animator.SetTrigger(AnimParameters.Dives);
        }












    }
}

