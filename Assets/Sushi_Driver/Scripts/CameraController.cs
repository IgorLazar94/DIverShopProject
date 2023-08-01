using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float minScale = 40f;
    [SerializeField] private float maxScale = 80f;
    [SerializeField] private float zoomSensitivity = 15f;
    private Vector3 offset;
    private Camera mainCamera;
    private float cameraScale;
    private bool isAndroidDevice;


    private void Start()
    {
        CheckRuntimePlatform();
        offset = transform.position;
        mainCamera = GetComponent<Camera>();
        cameraScale = mainCamera.fieldOfView;
    }

    private void CheckRuntimePlatform()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            isAndroidDevice = true;
        }
        else
        {
            isAndroidDevice = false;
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = targetPosition;
        }
        if (isAndroidDevice)
        {
            ZoomAndroid();
        }
        else
        {
            ZoomPC();
        }
    }

    private void ZoomAndroid()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOneLastPos = touchOne.position - touchOne.deltaPosition;

            float distanceTouch = (touchZeroLastPos - touchOneLastPos).magnitude;
            float currentDistanceTouch = (touchZero.position - touchOne.position).magnitude;


            float difference = currentDistanceTouch - distanceTouch;

            if (difference > 0)
            {
                cameraScale--;
            }
            else if (difference < 0)
            {
                cameraScale++;
            }
            difference = 0;
            cameraScale = Mathf.Clamp(cameraScale, minScale, maxScale);
            mainCamera.fieldOfView = cameraScale;
        }
    }

    private void ZoomPC()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        cameraScale -= scrollInput * zoomSensitivity;
        cameraScale = Mathf.Clamp(cameraScale, minScale, maxScale);
        mainCamera.fieldOfView = cameraScale;
    }

}
