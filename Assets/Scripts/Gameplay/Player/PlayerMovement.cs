using SCG.Combat;
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
    [SerializeField] float movementSpeed = 10;

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

        NearbyCraftableItem();
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

    void NearbyCraftableItem()
    {
        Collider[] coll = Physics.OverlapBox(transform.position, Vector3.one * 2);

        foreach(Collider c in coll)
        {
            if (c.gameObject.GetComponent<Thing>() != null)
            {
                c.transform.position = Vector3.MoveTowards(c.transform.position, gameObject.transform.position, 3 * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameplayUI.inst.GameIs(other.gameObject == winCollider, 2);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Fighter>() != null)
        {
            collision.transform.GetComponent<Health>().currentHealthPoints -= 5;
            float a = (100 / collision.transform.GetComponent<Health>().maxHealthPoints) * collision.transform.GetComponent<Health>().currentHealthPoints;
            healthBarValue.fillAmount = a / 100;

            GameplayUI.inst.GameIs(healthBarValue.fillAmount == 0, 1);
        }

        if (collision.transform.GetComponent<Thing>() != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
