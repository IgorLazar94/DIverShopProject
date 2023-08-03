using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerMovementControl : MonoBehaviour
    {
        private float speed;
        private float trainingFactorSpeed = 1f;
        private Rigidbody rb;
        private bool isReadyToMove = true;

        public static System.Action onPlayerStopped;

        private void Start()
        {
            speed = GameSettings.Instance.GetPlayerSpeed();
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            onPlayerStopped += EnableStopPlayer;

            UI.JoyStickInput.isHasInputDirection += PlayerMove;
            UI.JoyStickInput.isNotHasInputDirection += ResetSpeed;
            PlayerTrigger.onTriggerWater += OffsetDiving;
            TrainingZone.OnSpeedUpdateParameter += UpdateSpeedParameter;
        }

        private void OnDisable()
        {
            onPlayerStopped -= EnableStopPlayer;

            UI.JoyStickInput.isHasInputDirection -= PlayerMove;
            UI.JoyStickInput.isNotHasInputDirection -= ResetSpeed;
            PlayerTrigger.onTriggerWater -= OffsetDiving;
            TrainingZone.OnSpeedUpdateParameter -= UpdateSpeedParameter;
        }

        private void UpdateSpeedParameter()
        {
            speed += trainingFactorSpeed;
        }

        private void PlayerMove(Vector3 _inputDirection)
        {
            if (!isReadyToMove) return;

            Vector3 playerDirection = new Vector3(_inputDirection.x, 0f, _inputDirection.y);
            rb.velocity = playerDirection * speed;
            PlayerLookForward(playerDirection);
        }

        private void PlayerLookForward(Vector3 _playerDirection)
        {
            Vector3 lookAtPosition = transform.position + _playerDirection;
            transform.LookAt(lookAtPosition);
        }

        private void ResetSpeed()
        {
            rb.velocity = Vector3.zero;
        }

        private void EnableStopPlayer()
        {
            StartCoroutine(StopPlayer());
        }

        private IEnumerator StopPlayer()
        {
            isReadyToMove = false;
            ResetSpeed();
            yield return new WaitForSeconds(0.5f);
            isReadyToMove = true;
        }

        private void OffsetDiving()
        {
            transform.DOLocalMoveX(-9f, 0.5f);
        }
    }
}

