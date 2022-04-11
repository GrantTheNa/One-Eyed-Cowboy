using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenPlankScript : MonoBehaviour
{
    // Variables that need assigning
    public GameObject plank;
    public GameObject playerCamera;
    public FirstPersonController firstPersonController;

    // Variables that need adjusting
    public float grabDistance = 2;
    public float throwForce = 300;
    public GameObject woodTrigger;


    // Private variables
    private Vector3 objectPos;
    private bool highlighted, isHolding, plankPositionTrigger;

    public void Start()
    {
        // Assigning Variables
        playerCamera = GameObject.Find("First Person Camera");
        woodTrigger = GameObject.Find("WoodTrigger");
        firstPersonController = GameObject.FindObjectOfType<FirstPersonController>();
    }
    public void Update()
    {
        CheckTrigger();
        Highlight();
        HoldingCheck();
        Hold();
    }

    private void CheckTrigger()
    {
        plankPositionTrigger = woodTrigger.GetComponent<PlankTrigger>().plankPositionTrigger;
        // Debug.Log(plankPositionTrigger);
    }

    private void Highlight()
    {
        // Sets layermask to default, ignores player and UI layermask for the raycast
        int layerMask = 1 << 0;
        RaycastHit hit;
        // Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * grabDistance);
        // If raycast hits the item, item is highlighted
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, grabDistance, layerMask) && hit.collider.gameObject == plank && plankPositionTrigger == false)
        {
            highlighted = true;
            GetComponent<Outline>().enabled = true;
        }
        // Otherwise, item isn't highlighted
        else
        {
            highlighted = false;
            GetComponent<Outline>().enabled = false;
        }
    }

    private void HoldingCheck()
    {
        // Checks for dropping object, or if object is no longer highlighted through highlight timer (timer is used due to reasons with item not being highlighted for a frame)
        if (isHolding == true && Input.GetMouseButtonDown(0) == true || plankPositionTrigger == true)
        {
            isHolding = false;
            firstPersonController.holdingWood = false;
        }
        // Checks for picking up an object
        else if (highlighted == true && Input.GetMouseButtonDown(0) == true)
        {
            isHolding = true;
            plank.transform.SetParent(playerCamera.transform);
            plank.transform.position = woodTrigger.transform.position;
            plank.transform.rotation = woodTrigger.transform.rotation;
            firstPersonController.holdingWood = true;
        }

    }

    private void Hold()
    {
        // Code for holding an object
        if (isHolding == true)
        {
            plank.GetComponent<Rigidbody>().velocity = Vector3.zero;
            plank.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            plank.GetComponent<Rigidbody>().useGravity = false;
        }

        // Code when no longer holding object
        else
        {
            objectPos = plank.transform.position;
            plank.transform.SetParent(null);
            plank.GetComponent<Rigidbody>().useGravity = true;
            plank.transform.position = objectPos;
        }
    }
}
