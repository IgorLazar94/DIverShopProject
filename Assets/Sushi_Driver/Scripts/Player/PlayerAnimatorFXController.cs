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

            Player.PlayerTrigger.onTriggerGround += StartClimbesAnim;
            Player.PlayerTrigger.onTriggerWater += StartDivesAnim;
        }

        private void OnDisable()
        {
            UI.JoyStickInput.isHasInputDirection -= StartRunAnimation;
            UI.JoyStickInput.isNotHasInputDirection -= StopRunAnimation;

            Player.PlayerTrigger.onTriggerGround -= StartClimbesAnim;
            Player.PlayerTrigger.onTriggerWater -= StartDivesAnim;
        }

        private void StartRunAnimation(Vector3 direction)
        {
            // start particles in direction
            animator.SetBool(AnimParameters.isRun, true);
        }

        private void StopRunAnimation()
        {
            // stop particles
            animator.SetBool(AnimParameters.isRun, false);
        }

        private void EnableBusyHands()
        {
            animator.SetBool(AnimParameters.isBusyHands, true);
        }

        private void DisableBusyHands()
        {
            animator.SetBool(AnimParameters.isBusyHands, false);
        }

        private void StartClimbesAnim ()
        {
            animator.SetTrigger(AnimParameters.Climbes);
        }

        private void StartDivesAnim()
        {
            animator.SetTrigger(AnimParameters.Dives);
        }












    }
}

