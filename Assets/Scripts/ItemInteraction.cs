using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    // Variables that need assigning
    public GameObject item;
    public GameObject playerCamera;

    // Variables that need adjusting
    public float grabDistance = 2;
    public float throwForce = 300;

    // Private variables
    private Vector3 objectPos;
    private bool highlighted, isHolding;
    private float timer;


    public void Start()
    {
        // Assigning Variables
        playerCamera = GameObject.Find("First Person Camera");
    }
    public void Update()
    {
        Highlight();
        HoldingCheck();
        Hold();
    }

    private void Highlight()
    {
        // Sets layermask to default, ignores player and UI layermask for the raycast
        int layerMask = 1 << 7;
        RaycastHit hit;
        // Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * grabDistance);
        // If raycast hits the item, item is highlighted
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, grabDistance, layerMask) && hit.collider.gameObject == item)
        {
            highlighted = true;
            timer = 0.1f;
            GetComponent<Outline>().enabled = true;
        }
        // Otherwise, item isn't highlighted
        else
        {
            highlighted = false;
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 0;
            }
            GetComponent<Outline>().enabled = false;
        }
    }

    private void HoldingCheck()
    {
        // Checks for dropping object, or if object is no longer highlighted through highlight timer (timer is used due to reasons with item not being highlighted for a frame)
        if (isHolding == true && Input.GetMouseButtonDown(0) == true || timer == 0)
        {
            isHolding = false;
        }
        // Checks for throwing object
        else if (isHolding == true && Input.GetMouseButtonDown(1) == true)
        {
            item.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * throwForce);
            isHolding = false;
        }
        // Checks for picking up an object
        else if (highlighted == true && Input.GetMouseButtonDown(0) == true)
        {
            isHolding = true;
        }

    }

    private void Hold()
    {
        // Code for holding an object
        if (isHolding == true)
        {
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.transform.SetParent(playerCamera.transform);
            item.GetComponent<Rigidbody>().useGravity = false;
        }

        // Code when no longer holding object
        else
        {
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;
        }
    }
}
