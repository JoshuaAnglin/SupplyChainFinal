
using SCG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;

    Transform cam;
    float camRotation = 0f;

    float mouseSensitivity = 400f;
    float movementSpeed = 10;

    [Header("Smoothing\n")]
    // Player movement
    public float Smoothment = 0.3f;
    Vector2 curDir = Vector2.zero;
    Vector2 curDirVelocity = Vector2.zero;

    // Mouse movement
    public float mouseSmoothment = 0.03f;
    Vector2 curMouseMove = Vector2.zero;
    Vector2 curMouseMoveVelocity = Vector2.zero;

    [SerializeField] Image healthBarValue;
    [SerializeField] GameObject winCollider;


    // Jumping
    float jumpForce = 2f;

    // Gravity
    float yVel; // Keeps track of downward speed
    float gravity = -13f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cam = transform.GetChild(0).transform;
    }

    void Update()
    {
        movement();
        lookAtMouse();
    }

    void movement()
    {
        // Gets the current keyboard input
        Vector2 target = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Normalizes the movement through each direction (since moving diagonal can make the player go farther, which makes it unbalanced)
        target.Normalize();

        // Smoothens the transition between vectors
        curDir = Vector2.SmoothDamp(curDir, target, ref curDirVelocity, Smoothment);

        if (!controller.isGrounded) yVel += gravity * Time.deltaTime;
        else yVel = 0f;

        // Directional movement
        // 'Vector3.up' instead of down because 'Yvel' is a minus (player would go up if they weren't grounded)
        Vector3 move = ((transform.forward * curDir.y + transform.right * curDir.x) * movementSpeed + Vector3.up * yVel) * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && controller.isGrounded)
        {
            move.y = jumpForce;
        }
        else yVel += gravity * Time.deltaTime;

        // Character controller handles movement and collision
        controller.Move(move);

        //--------------------------------------------------------------

        /*float horizontal = Input.GetAxis("Horizontal") * movementSpeed;
        float vertical = Input.GetAxis("Vertical") * movementSpeed;
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            curDir.y = jumpForce;
        }
        else curDir.y += gravity * Time.deltaTime;

        controller.Move(move.normalized * movementSpeed * Time.deltaTime);
        controller.Move(curDir * Time.deltaTime);*/

        //--------------------------------------------------------------
    }

    //--------------------------------------------------------------

    /*Vector2 target = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    curDir = Vector2.SmoothDamp(curDir, target, ref curDirVelocity, Smoothment);

    Vector3 move = (transform.right * curDir.x + transform.forward * curDir.y + Vector3.up * yVel) * Time.deltaTime;

    if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
    {
        move.y = jumpForce;
    }
    else yVel += gravity * Time.deltaTime;

    controller.Move(move.normalized * movementSpeed);
    controller.Move(curDir * Time.deltaTime); */


    void lookAtMouse()
    {
        // Gets the current mouse input
        Vector2 target = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Smoothens the transition between vectors
        curMouseMove = Vector2.SmoothDamp(curMouseMove, target, ref curMouseMoveVelocity, mouseSmoothment);

        // y-axis of mouse to effect the value of camera pitch
        // '-=' instead of '+=' to solve the inverse problem (unless intentional)
        camRotation -= curMouseMove.y * mouseSensitivity * Time.deltaTime;
        camRotation = Mathf.Clamp(camRotation, -90, 90);

        // Set the camera's angles
        cam.localEulerAngles = Vector3.right * camRotation;

        // Rotates the player upwards by the x-axis of the mouse
        transform.Rotate(Vector3.up * curMouseMove.x * mouseSensitivity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameplayUI.inst.GameIs(other.gameObject == winCollider, 2);
    }
}
