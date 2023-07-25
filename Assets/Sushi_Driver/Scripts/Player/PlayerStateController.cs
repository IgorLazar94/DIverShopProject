using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using DG.Tweening;

namespace Player
{
    public class PlayerStateController : MonoBehaviour
    {
        public static Action<bool> OnMaxFishBlocked;
        public bool isFishingBlock { get; private set; }
        [SerializeField] private PlayerFOV playerFOV;
        [SerializeField] private GameObject maxSprite;
        private PlayerStateMachine playerSM;
        private OnTheGroundState onTheGroundState;
        private OnTheWaterState onTheWaterState;

        private void Start()
        {
            playerSM = new PlayerStateMachine();
            onTheGroundState = new OnTheGroundState(this);
            onTheWaterState = new OnTheWaterState(this);
            playerSM.Initialize(new OnTheGroundState(this));
        }

        private void OnEnable()
        {
            PlayerTrigger.onTriggerGround += ChangeStateToGround;
            PlayerTrigger.onTriggerWater += ChangeStateToWater;
            OnMaxFishBlocked += MaxFishBlockedFishing;
        }

        private void OnDisable()
        {
            PlayerTrigger.onTriggerGround -= ChangeStateToGround;
            PlayerTrigger.onTriggerWater -= ChangeStateToWater;
            OnMaxFishBlocked -= MaxFishBlockedFishing;
        }

        private void Update()
        {
            playerSM.CurrentState.Update();
            if (isFishingBlock)
            {
                maxSprite.transform.LookAt(Camera.main.transform);
            }
        }

        private void ChangeStateToGround()
        {
            Debug.Log("Switch to ground");
            playerSM.ChangeState(onTheGroundState);
        }

        private void ChangeStateToWater()
        {
            playerSM.ChangeState(onTheWaterState);
        }

        public PlayerFOV ReturnSearchArea()
        {
            return playerFOV;
        }

        private void MaxFishBlockedFishing(bool isBlock)
        {
            if (isBlock)
            {
                isFishingBlock = true;
                maxSprite.transform.DOScale(Vector3.one, 0.5f).OnComplete(() => ShakingMaxSprite());
                playerFOV.viewRadius = 0;
            } else
            {
                isFishingBlock = false;
                maxSprite.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => maxSprite.SetActive(false));
            }
        }

        private void ShakingMaxSprite()
        {
            maxSprite.SetActive(true);
            maxSprite.transform.DOShakeScale(1f, 1, 2).SetLoops(-1).SetEase(Ease.InBounce);
        }
    }
}

