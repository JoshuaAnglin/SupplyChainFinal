using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SCG.Stats;

namespace SCG.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        RaycastHit itemHit;
        float rayDist = 4f;

        Transform hit;
        Transform holding;

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
            if (!Input.GetMouseButton(0) && holding) DropConditions();
        }

        // Picking up items
        void Pickup()
        {
            Rigidbody rb = holding.GetComponent<Rigidbody>();
            rb.velocity = holding.GetComponent<Item>().launchSpeed * (transform.GetChild(0).position - holding.transform.position);
            rb.freezeRotation = true;
            //currentSelectedObject.text = holding.name;
            
        }

        // Dropping items
        void DropConditions()
        {
            holding.GetComponent<Rigidbody>().freezeRotation = false;
            holding = null;
        }
    }
}