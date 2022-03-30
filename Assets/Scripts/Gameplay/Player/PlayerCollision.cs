using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    RaycastHit itemHit;
    float rayDist = 15f;
    HUD playerHUD;

    //Transform hit;
    Transform holding;
    Rigidbody rbHolding;

    private void Awake()
    {playerHUD = transform.parent.GetComponent<JPlayerMovement>().playerHUD;}

    void Update()
    {
        scanForItem();
    }

    // Picking up items
    void scanForItem()
    {
        // If raycast hits something
        if (Physics.Raycast(transform.position, transform.forward, out itemHit, rayDist))
        {
            // If it holds the interface 'IPickUp'
            if (itemHit.transform.GetComponent<IPickUp>() != null)
            {
                if (!holding)
                {
                    holding = itemHit.transform;
                    holding.transform.gameObject.layer = LayerMask.NameToLayer("Hovered");
                }
            }

            // IT'S THIS THAT'S THE ISSUE WITH THE ITEM BEING GRABBED WHILST RAYCASTING ISN'T ON IT
            else if (itemHit.transform.gameObject == null & holding != null)
            {
                holding.transform.gameObject.layer = LayerMask.NameToLayer("Default");
                holding = null;
            }
        }

        // Hold the item when pressed with the left button
        if (Input.GetMouseButton(0) && holding) Pickup();

        // Let go of the item when left button isn't pressed
        else if (!Input.GetMouseButton(0) && holding) Drop();
    }

    void Pickup()
    {
        if (rbHolding == null)
        {
            rbHolding = holding.GetComponent<Rigidbody>();
            holding.GetComponent<Rigidbody>().freezeRotation = true;

            playerHUD.AddToHUD(holding.GetComponent<KeyItem>());
        }

        Physics.IgnoreCollision(transform.parent.GetComponent<Collider>(), holding.GetComponent<Collider>(), true);
        rbHolding.velocity = holding.GetComponent<IPickUp>().LaunchSpeed() * (transform.GetChild(0).position - holding.transform.position);
        if (holding.GetComponent<IPickUp>().CanRotate()) RotateHeld();

        if (Vector3.Distance(transform.position, holding.transform.position) > 50f)
        {
            rbHolding.velocity = Vector3.zero;
            Drop();
        }
    }

    // Dropping items
    void Drop()
    {
        Physics.IgnoreCollision(transform.parent.GetComponent<Collider>(), holding.GetComponent<Collider>(), false);

        if (rbHolding != null)
        {
            playerHUD.RemoveFromHUD();
            holding.transform.gameObject.layer = LayerMask.NameToLayer("Default");
            rbHolding.freezeRotation = false;
            rbHolding = null;
            holding = null;
        }
    }

    void RotateHeld()
    {
        if (Input.GetKey(KeyCode.Z)) holding.Rotate(transform.right, 1, Space.World);
        if (Input.GetKey(KeyCode.X)) holding.Rotate(transform.forward, 1, Space.World);
        if (Input.GetKey(KeyCode.C)) holding.Rotate(transform.up, 1, Space.World);
    }
}