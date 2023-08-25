using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace Player
{
    [RequireComponent(typeof(PlayerStateController))]
    public class PlayerAnimatorFXController : MonoBehaviour
    {
        public static Action OnPlayerHandsTook;
        public static Action OnPlayerHandsFree;
        private Animator animator;
        private ParticleSystem rippleSplashFX;

        private void Start()
        {
            rippleSplashFX = GetComponentInChildren<ParticleSystem>();
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            UI.JoyStickInput.isHasInputDirection += StartRunAnimation;
            UI.JoyStickInput.isNotHasInputDirection += StopRunAnimation;

            Player.PlayerTrigger.onPlayClimbAnim += StartClimbesAnim;
            Player.PlayerTrigger.onTriggerWater += StartDivesAnim;
            Player.PlayerMovementControl.onPlayerStopped += StopRunAnimation;
            OnPlayerHandsTook += EnableBusyHands;
            OnPlayerHandsFree += DisableBusyHands;
        }

        private void OnDisable()
        {
            UI.JoyStickInput.isHasInputDirection -= StartRunAnimation;
            UI.JoyStickInput.isNotHasInputDirection -= StopRunAnimation;

            Player.PlayerTrigger.onPlayClimbAnim -= StartClimbesAnim;
            Player.PlayerTrigger.onTriggerWater -= StartDivesAnim;
            Player.PlayerMovementControl.onPlayerStopped -= StopRunAnimation;
            OnPlayerHandsTook -= EnableBusyHands;
            OnPlayerHandsFree -= DisableBusyHands;
        }

        private void StartRunAnimation(Vector3 direction)
        {
            animator.SetBool(AnimParameters.isRun, true);
        }

        private async void StopRunAnimation()
        {
            animator.SetBool(AnimParameters.isRun, false);
            await PlayerReadyToMoveAsync();
        }

        private async Task PlayerReadyToMoveAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.25f));
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
        }

        private void StartDivesAnim()
        {
            animator.SetTrigger(AnimParameters.Dives);
            Invoke(nameof(PlayRippleFX), 0.85f);
        }

        private void PlayRippleFX()
        {
            rippleSplashFX.Play();
            AudioManager.instance.PlaySFX(AudioCollection.InSplash_1);
        }












    }
}

