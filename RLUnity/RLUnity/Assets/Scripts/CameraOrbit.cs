using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    /// <summary>
    /// Camera transform
    /// </summary>
    private Transform _CameraXForm;

    /// <summary>
    /// Focal point transform
    /// </summary>
    private Transform _FocusXForm;

    /// <summary>
    /// Local rotation values
    /// </summary>
    public Vector3 _LocalRotation;

    /// <summary>
    /// The distance from the focal point to the camera
    /// </summary>
    public float CameraDistance = 10f;

    /// <summary>
    /// The sensitity of the camera to mouse movement when orbiting
    /// </summary>
    public float OrbitSensitivity = 4f;

    /// <summary>
    /// The sensitivity of the camera to mouse movement when zooming
    /// </summary>
    public float ZoomSensitity = 2f;

    /// <summary>
    /// The speed of orbiting
    /// </summary>
    public float OrbitSpeed = 10f;

    /// <summary>
    /// The speed of zooming
    /// </summary>
    public float ZoomSpeed = 6f;

    /// <summary>
    /// The minimum distance from the camera to the focus
    /// </summary>
    public float NearDistance = 0.0f;

    // Use this for initialization
    public void Start()
    {
        _CameraXForm = transform;
        _FocusXForm = transform.parent;
    }

    // Update is called once per frame
    public void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            // Rotate based on mouse movement
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
            if (mouseX != 0 || mouseY != 0)
            {
                _LocalRotation.x += mouseX * OrbitSensitivity;
                _LocalRotation.y -= mouseY * OrbitSensitivity;

                // Clamp to 0-90 degrees pitch:
                _LocalRotation.y = Mathf.Clamp(_LocalRotation.y, 0f, 90f);
            }

            if (mouseWheel != 0)
            {
                float scrollAmount = mouseWheel * ZoomSensitity * (CameraDistance * 0.3f);
                CameraDistance -= scrollAmount;
                if (CameraDistance < NearDistance) CameraDistance = NearDistance;
            }
        }

        // Update camera position:
        Quaternion q = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        _FocusXForm.rotation = Quaternion.Lerp(_FocusXForm.rotation, q, Time.deltaTime * OrbitSpeed);

        if (_CameraXForm.localPosition.z != -CameraDistance)
        {
            _CameraXForm.localPosition = new Vector3(0, 0, Mathf.Lerp(_CameraXForm.localPosition.z, -CameraDistance, Time.deltaTime * ZoomSpeed));
        }
    }
}
