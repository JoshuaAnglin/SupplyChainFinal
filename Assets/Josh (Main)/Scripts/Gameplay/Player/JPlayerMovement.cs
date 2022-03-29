using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JPlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Transform orientation;
    [SerializeField] Camera cam;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float walkSpeed = 4f;
    float airMultiplier = 0.4f;
    float movementMultiplier = 10f;

    [Header("Sprinting")]
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;
    [SerializeField] float sprintFOV = 100f;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public float jumpRate = 15f;

    [Header("Crouching")]
    public float crouchScale = 0.75f;
    public float crouchSpeed = 1f;
    float crouchMultiplier = 5f;

    [Header("Activate Sprint")]
    float sprintActivation = 0f;
    float sprintLimit = 0.1f;
    int sprintPressed;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;

    static public bool pickupOnMe;

    [Header("Grounded")]
    int groundLayer = 7;

    bool isOnGround = true;
    bool isCrouching;

    Vector3 movement;

    enum moveStatus
    {
        Walking,
        Sprinting,
        Crouching
    }

    moveStatus moving;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        movement = orientation.forward * Input.GetAxisRaw("Vertical") + orientation.right * Input.GetAxisRaw("Horizontal");

        MovementState();
        ControlDrag();

        if (Input.GetKey(GlobalScript.jumpControl) && isOnGround)
        {
            Jump();
            isOnGround = false;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();

        /*if (Input.GetKey(GlobalScript.crouchControl)) Crouch();
        else UnCrouch();*/
    }

    void MovementState()
    {
        ActivateSprinting();

        switch (moving)
        {
            case moveStatus.Crouching:
                moveSpeed = Mathf.Lerp(moveSpeed, crouchSpeed, acceleration * Time.deltaTime);
                rb.AddForce(movement.normalized * moveSpeed * crouchMultiplier, ForceMode.Acceleration);
                break;

            case moveStatus.Walking:
                moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 90f, 8f * Time.deltaTime);
                break;

            case moveStatus.Sprinting:
                moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, sprintFOV, 8f * Time.deltaTime);

                if (movement == Vector3.zero)
                {
                    sprintPressed = 0;
                    moving = moveStatus.Walking;
                }
                break;
        }
    }

    // Double tap to sprint
    void ActivateSprinting()
    {
        if (moving == moveStatus.Walking)
        {
            switch (sprintPressed)
            {
                case 0:
                    if (movement != Vector3.zero)
                    {
                        sprintPressed = 1;
                        if (sprintActivation != 0) sprintActivation = 0;
                    }
                    break;

                case 1:
                    sprintActivation += Time.deltaTime;
                    if (movement == Vector3.zero)
                    {
                        if (sprintActivation < sprintLimit) sprintPressed = 2;
                        else if (sprintActivation > sprintLimit) sprintPressed = 0;

                        sprintActivation = 0;
                    }
                    break;

                case 2:
                    if (movement != Vector3.zero && sprintActivation < (sprintLimit)) moving = moveStatus.Sprinting;
                    else if (sprintActivation >= sprintLimit) sprintPressed = 0;
                    break;
            }
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void Crouch()
    {
        Vector3 _crouchScale = new Vector3(transform.localScale.x, crouchScale, transform.localScale.z);
        transform.localScale = _crouchScale;

        moving = moveStatus.Crouching;
    }

    void UnCrouch()
    {
        Vector3 normalScale = new Vector3(transform.localScale.x, 0.9f, transform.localScale.z);
        transform.localScale = normalScale;

        moving = moveStatus.Walking;
        sprintPressed = 0;
    }

    void ControlDrag()
    {
        if (isOnGround) rb.drag = groundDrag;
        else rb.drag = airDrag;
    }

    void MovePlayer()
    {
        if (isOnGround) rb.AddForce(movement.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);   
        else if (!isOnGround) rb.AddForce(movement.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == groundLayer)
            isOnGround = true;

        if (collision.gameObject.GetComponent<IPickUp>() != null)
        {
            pickupOnMe = true;
        }
        else pickupOnMe = false;
    }
}