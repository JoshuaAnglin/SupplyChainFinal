using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    bool moveCamera = true;

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
        if (moveCamera) CameraMovement();
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


    /// EVENT SUBSCRIPTION (GES - GAMEPLAY) /// 

    void OnEnable()
    {
        GameEventSystemGameplay.GESGameplay.onUnpaused += delegate { moveCamera = true; };
        GameEventSystemGameplay.GESGameplay.onPaused += delegate { moveCamera = false; };
    }

    void OnDisable()
    {
        GameEventSystemGameplay.GESGameplay.onUnpaused -= delegate { moveCamera = true; };
        GameEventSystemGameplay.GESGameplay.onPaused -= delegate { moveCamera = false; };
    }
}