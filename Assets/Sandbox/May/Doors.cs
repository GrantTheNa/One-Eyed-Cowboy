using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    // Variables that need assigning
    private GameObject door;
    private GameObject openDoorText;
    private GameObject closeDoorText;
    private GameObject playerCamera;
    private Animator doorAnimator;

    // Variables that need to be set
    public float doorOpeningTime = 0.5f;

    // Private Variables
    private bool lookingAtDoor, doorOpened;
    private float timerTemp;

    // Start is called before the first frame update
    void Start()
    {
        // Assigns all of the components used
        playerCamera = GameObject.Find("First Person Camera");
        openDoorText = GameObject.Find("OpenDoorText");
        closeDoorText = GameObject.Find("CloseDoorText");
        door = this.gameObject;
        doorAnimator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        // Debug ray is the same as the raycast below
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 3);
        // Sends a raycast 3 metres out from where the player is looking, checks if the raycast hits the door, and if the timer cooldown is not active
        // This raycast does not seem to work properly for displaying text, as it flickers when moving, but there have been no problems with opening the door
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 3f) == true && hit.collider.gameObject == door && timerTemp <= 0)
        {
            // Checks if the door is opened or closed to show appropiate text
            // Also checks if the text for "E to open/close door" is unassigned, and doesn't activate code if so
            if (doorOpened == false && openDoorText != null)
            {
                openDoorText.SetActive(true);
            }
            else if (closeDoorText != null)
            {
                closeDoorText.SetActive(true);
            }

            lookingAtDoor = true;
        }
        else
        {
            // Checks for unnassigned text first
            if (closeDoorText != null && openDoorText != null)
            {
                openDoorText.SetActive(false);
                closeDoorText.SetActive(false);
            }

            lookingAtDoor = false;
        }

        // Checks if player presses "E" on a closed door
        if (lookingAtDoor == true && Input.GetAxis("Fire2") != 0 && doorOpened == false)
        {
            // Triggers door animation to open and begins cooldown timer for the door
            doorAnimator.SetTrigger("opened");
            doorOpened = true;
            timerTemp = doorOpeningTime;
        }
        // Checks if player presses "E" on an open door
        else if (lookingAtDoor == true && Input.GetAxis("Fire2") != 0 && doorOpened == true)
        {
            // Triggers door animation to close and begins cooldown timer for the door
            doorAnimator.ResetTrigger("opened");
            doorOpened = false;
            timerTemp = doorOpeningTime;
        }

        // Reduce cooldown timer
        timerTemp -= Time.deltaTime;
    }
}
