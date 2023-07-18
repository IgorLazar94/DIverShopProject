using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerMovementControl : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        private Rigidbody rb;
        private bool isReadyToMove = true;

        public static System.Action onPlayerStopped;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            onPlayerStopped += EnableStopPlayer;

            UI.JoyStickInput.isHasInputDirection += PlayerMove;
            UI.JoyStickInput.isNotHasInputDirection += ResetSpeed;
        }

        private void OnDisable()
        {
            onPlayerStopped -= EnableStopPlayer;

            UI.JoyStickInput.isHasInputDirection -= PlayerMove;
            UI.JoyStickInput.isNotHasInputDirection -= ResetSpeed;
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

        //private void Update()
        //{
        //    // Debug
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        EnableStopPlayer(1.0f);
        //    }
        //}
    }
}

