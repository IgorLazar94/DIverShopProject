using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class JoyStickInput : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private Image joystick;
        [SerializeField] private Image knob;
        private Vector3 inputDirection;

        public static Action<Vector3> isHasInputDirection;
        public static Action isNotHasInputDirection;

        void Start()
        {
            inputDirection = Vector3.zero;
            joystick.gameObject.SetActive(false);
        }

        public void OnDrag(PointerEventData ped)
        {
            Vector2 touchPosition = Vector2.zero;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                joystick.rectTransform,
                ped.position,
                ped.pressEventCamera,
                out touchPosition))
            {
                inputDirection = new Vector3(touchPosition.x / joystick.rectTransform.sizeDelta.x,
                                             touchPosition.y / joystick.rectTransform.sizeDelta.y,
                                             0);
                inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;
                isHasInputDirection?.Invoke(inputDirection);
                knob.rectTransform.anchoredPosition = new Vector3(inputDirection.x * (joystick.rectTransform.sizeDelta.x / 3),
                                                                 inputDirection.y * (joystick.rectTransform.sizeDelta.y) / 3);
            }
        }

        public void OnPointerDown(PointerEventData ped)
        {
            joystick.gameObject.SetActive(true);
            Vector2 touchPosition = Vector2.zero;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
               joystick.rectTransform,
               ped.position,
               ped.pressEventCamera,
               out touchPosition))
                joystick.rectTransform.anchoredPosition = touchPosition;

            OnDrag(ped);
        }

        public void OnPointerUp(PointerEventData ped)
        {
            inputDirection = Vector3.zero;
            isNotHasInputDirection?.Invoke();
            joystick.gameObject.SetActive(false);

            joystick.rectTransform.anchoredPosition = Vector3.zero;
            knob.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}