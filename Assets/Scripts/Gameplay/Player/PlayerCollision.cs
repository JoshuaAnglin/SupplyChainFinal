using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    RaycastHit itemHit;
    float rayDist = 20f;

    Transform hit;
    Transform holding;
    Rigidbody rbHolding;

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
                // Change it's layer to 'Hovered
                hit = itemHit.transform;
                hit.transform.gameObject.layer = LayerMask.NameToLayer("Hovered");

                // If the player isn't holding an item, set the raycasted item to 'holding'
                if (!holding) holding = itemHit.transform;
            }

            // {CAUSES FLICKERING}
            else if (hit)
            {
                hit.transform.gameObject.layer = LayerMask.NameToLayer("Default");
                hit = null;
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