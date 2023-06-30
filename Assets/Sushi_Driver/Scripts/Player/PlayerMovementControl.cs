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

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            UI.JoyStickInput.isHasInputDirection += PlayerMove;
            UI.JoyStickInput.isNotHasInputDirection += PlayerStop;
        }

        private void OnDisable()
        {
            UI.JoyStickInput.isHasInputDirection -= PlayerMove;
            UI.JoyStickInput.isNotHasInputDirection -= PlayerStop;
        }

        private void PlayerMove(Vector3 _inputDirection)
        {
            Vector3 playerDirection = new Vector3(_inputDirection.x, 0f, _inputDirection.y);
            rb.velocity = playerDirection * speed;

            PlayerLookForward(playerDirection);
        }

        private void PlayerLookForward(Vector3 _playerDirection)
        {
            Vector3 lookAtPosition = transform.position + _playerDirection;
            transform.LookAt(lookAtPosition);
        }

        private void PlayerStop()
        {
            rb.velocity = Vector3.zero;
        }
    }
}

