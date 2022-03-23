using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JPlayerMovement : MonoBehaviour
{
    [Header("Components")]

        // Self
    Rigidbody rb;
    Transform orientation;

    // Item
    [SerializeField] LayerMask groundMask;
    Transform holdPosition;
    RaycastHit info;
    Rigidbody keyItemRb;

    [Header("Movement\n")]

    [SerializeField] float moveSpeed;
    Vector3 direction;

    [Header("Ground")]
    float groundLevel;
    [SerializeField] float groundrag;

    [Header("Jumping\n")]

    [SerializeField] float jumpForce;
    [SerializeField] float airSpeed;
    bool isOnGround;

    enum CharacterState
    {
        Offense,
        Pickup
    }

    CharacterState state;

    // -----------------------------------------------------------------

    void Awake()
    {
        orientation = transform.GetChild(0);
        holdPosition = transform.GetChild(2);
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        groundLevel = GetComponent<Renderer>().bounds.max.y;
    }

    void Update()
    {
        switch (state)
        {
            case CharacterState.Offense:
                    break;

            case CharacterState.Pickup:
                break;
        }
    }

    void FixedUpdate()
    {
        Movement();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Debug.Log(2);
            if (!isOnGround) isOnGround = true;
        }
    }

    #region Player Movement
    void Movement()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");
        float yMovement = Input.GetAxisRaw("Vertical");

        direction = orientation.forward * yMovement + orientation.right * xMovement;

        if (isOnGround) rb.AddForce(direction.normalized * moveSpeed, ForceMode.Force);
        else if (!isOnGround) rb.AddForce(direction.normalized * moveSpeed * airSpeed, ForceMode.Force);

        if (Physics.Raycast(transform.position, Vector3.down, groundLevel + 1f, groundMask))
            rb.drag = groundrag;
        else rb.drag = 0;

        Vector3 currentVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (currentVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        Jumping();
    }

    void Jumping()
    {
        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }
    #endregion
}