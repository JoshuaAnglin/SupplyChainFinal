
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
        Movement();
        lookAtMouse();

        OpenUI();
        CraftingMaterialPickup();
    }

    void Movement()
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
    }

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

    void OnCollisionEnter(Collision col)
    {
        // May replace with object pooling
        if (col.gameObject.GetComponent<CraftingMaterial>() != null)
        {
            HUD.hud.AddToInventory(col.gameObject.GetComponent<CraftingMaterial>());
            Destroy(col.gameObject);
        }

        if (col.gameObject.GetComponent<KeyItem>() != null)
        {
            HUD.hud.AddToInventory(col.gameObject.GetComponent<KeyItem>());
            Destroy(col.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //GameplayUI.inst.GameIs(other.gameObject == winCollider, 2);
    }

    #region UI Inventory
    void CraftingMaterialPickup()
    {

        Collider[] hits = Physics.OverlapBox(gameObject.transform.position, transform.localScale * 4, Quaternion.identity);
        
        foreach(Collider hit in hits)
        {
            if (hit.gameObject.GetComponent<CraftingMaterial>() != null)
            {
                hit.transform.position = Vector3.MoveTowards(hit.transform.position, transform.position, 2 * Time.deltaTime);
            }
        }
    }

    void OpenUI()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            GameEventSystemGameplay.GESGameplay.OpenInventory();

        if (Input.GetMouseButtonDown(2))
            HUD.hud.RemoveFromInventory(0);

        // pass into open inventory

    }
    #endregion
}
