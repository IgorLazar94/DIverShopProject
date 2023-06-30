using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

namespace Player
{
    public class PlayerStateController : MonoBehaviour
    {

        private PlayerStateMachine _SM;
        private OnTheGroundState onTheGroundState;

        private void OnEnable()
        {
            PlayerTrigger.onTriggerGround += ChangeStateToGround;
            PlayerTrigger.onTriggerWater += ChangeStateToWater;
        }

        private void OnDisable()
        {
            PlayerTrigger.onTriggerGround -= ChangeStateToGround;
            PlayerTrigger.onTriggerWater -= ChangeStateToWater;
        }

        private void Start()
        {
            _SM = new PlayerStateMachine();
            _SM.Initialize(new OnTheGroundState());
        }

        private void Update()
        {
            _SM.CurrentState.Update();
        }

        private void ChangeStateToGround()
        {
            _SM.ChangeState(new OnTheGroundState());
        }

        private void ChangeStateToWater()
        {
            _SM.ChangeState(new OnTheWaterState());
        }


    }
}

