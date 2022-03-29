using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float xSense = 10;
    float ySense = 10;
    float xRot, yRot;

    [SerializeField] Camera cam;
    [SerializeField] Transform orientation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        // Mouse Input
        float xMovement = Input.GetAxisRaw("Mouse X") * xSense;
        float yMovement = Input.GetAxisRaw("Mouse Y") * ySense;

        xRot -= yMovement;
        yRot += xMovement;

        xRot = Mathf.Clamp(xRot, -90, 90);

        // Rotation
        cam.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }
}