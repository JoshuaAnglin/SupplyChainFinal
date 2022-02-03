using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SCG.Combat;
using SCG.Stats;

namespace SCG.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        RaycastHit obj;
        float rayDist = 30f;

        Transform hit;
        Transform holding;

        [SerializeField] Text currentSelectedObject;

        void Update()
        {
            scanForItem();
        }

        // Picking up items
        void scanForItem()
        {
            // If raycast hits something
            if (Physics.Raycast(transform.position, transform.forward, out obj, rayDist))
            {
                // If it holds the interface 'IInteractWith'
                if (obj.transform.GetComponent<IInteractWith>() != null)
                {
                    if (obj.transform.GetComponent<Item>() != null)
                    {
                        // Change it's layer to 'Hovered
                        hit = obj.transform;
                        hit.transform.gameObject.layer = LayerMask.NameToLayer("Hovered");

                        // If the player isn't holding an item, set the raycasted item to 'holding'
                        if (!holding) holding = obj.transform;
                    }
                }

                else if (obj.transform.GetComponent<Fighter>() != null)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        Fighter fg = obj.transform.GetComponent<Fighter>();
                        fg.Attack();
                    }
                }

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
            currentSelectedObject.text = holding.name;
            rb.velocity = holding.GetComponent<Item>().launchSpeed * (transform.GetChild(0).position - holding.transform.position);
            rb.freezeRotation = true;
        }

        // Dropping items
        void DropConditions()
        {
            holding.GetComponent<Rigidbody>().freezeRotation = false;
            holding = null;
            currentSelectedObject.text = "---";
        }
    }
}
