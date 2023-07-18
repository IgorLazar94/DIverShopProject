using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerStateController))]
    public class PlayerAnimatorFXController : MonoBehaviour
    {
        private Animator animator;
        private PlayerStateController playerStateController;

        private void Start()
        {
            animator = GetComponent<Animator>();
            playerStateController = GetComponent<PlayerStateController>();
        }

        private void OnEnable()
        {
            UI.JoyStickInput.isHasInputDirection += StartRunAnimation;
            UI.JoyStickInput.isNotHasInputDirection += StopRunAnimation;

            Player.PlayerTrigger.onPlayClimbAnim += StartClimbesAnim;
            Player.PlayerTrigger.onTriggerWater += StartDivesAnim;
            Player.PlayerMovementControl.onPlayerStopped += StopRunAnimation;
        }

        private void OnDisable()
        {
            UI.JoyStickInput.isHasInputDirection -= StartRunAnimation;
            UI.JoyStickInput.isNotHasInputDirection -= StopRunAnimation;

            Player.PlayerTrigger.onPlayClimbAnim -= StartClimbesAnim;
            Player.PlayerTrigger.onTriggerWater -= StartDivesAnim;
            Player.PlayerMovementControl.onPlayerStopped -= StopRunAnimation;
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

        private void StartClimbesAnim()
        {
            if (OnTheWaterState.isInWater)
            {
                animator.SetTrigger(AnimParameters.Climbes);
            }

            // костыль?
            //PlayerMovementControl.onPlayerStopped.Invoke(0.1f);
        }

        private void StartDivesAnim()
        {
            animator.SetTrigger(AnimParameters.Dives);
        }












    }
}

