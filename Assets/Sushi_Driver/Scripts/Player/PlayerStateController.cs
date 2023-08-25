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
        [SerializeField] private GameObject maxText;
        private PlayerStateMachine playerSM;
        private OnTheGroundState onTheGroundState;
        private OnTheWaterState onTheWaterState;
        private RectTransform textRectTransfrom;
        private Transform mainCameraTransform;

        private void Start()
        {
            playerSM = new PlayerStateMachine();
            onTheGroundState = new OnTheGroundState(this);
            onTheWaterState = new OnTheWaterState(this);
            playerSM.Initialize(new OnTheGroundState(this));
            textRectTransfrom = maxText.GetComponent<RectTransform>();
            mainCameraTransform = Camera.main.transform;
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
                Vector3 toCamera = mainCameraTransform.position - textRectTransfrom.position;
                Quaternion lookRotation = Quaternion.LookRotation(-toCamera, mainCameraTransform.up);
                textRectTransfrom.rotation = lookRotation;
            }
        }

        private void ChangeStateToGround()
        {
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
                maxText.transform.DOScale(Vector3.one, 0.5f).OnComplete(() => ShakingMaxText());
                playerFOV.currentViewRadius = 0;
            } else
            {
                isFishingBlock = false;
                maxText.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => maxText.SetActive(false));
            }
        }

        private void ShakingMaxText()
        {
            maxText.SetActive(true);
            maxText.transform.DOShakeScale(1f, 0.5f, 1).SetLoops(-1).SetEase(Ease.InBounce);
        }
    }
}

