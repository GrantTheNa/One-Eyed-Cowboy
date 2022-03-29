using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    // Variables that need assigning
    public GameObject door;
    [SerializeField] private GameObject openDoorText;
    [SerializeField] private GameObject closeDoorText;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private Animator doorAnimator;

    // Variables that need to be set
    public float doorOpeningTime = 0.5f;

    // Private Variables
    private bool lookingAtDoor, doorOpened;
    private float timerTemp;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("First Person Camera");
        openDoorText = GameObject.Find("OpenDoorText");
        closeDoorText = GameObject.Find("CloseDoorText");
        door = this.gameObject;
        doorAnimator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sets a raycast from the player's eyes out for 3 metres to see if they are hovering over the door
        RaycastHit hit;
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 3);
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 3f) == true && hit.collider.gameObject == door && timerTemp <= 0)
        {
            if(doorOpened == false && openDoorText != null)
            {
                openDoorText.SetActive(true);
            }
            else if (closeDoorText != null)
            {
                closeDoorText.SetActive(true);
            }

            lookingAtDoor = true;
            Debug.Log("Door Hit");
        }
        else
        {
            if (closeDoorText != null && openDoorText != null)
            {
                openDoorText.SetActive(false);
                closeDoorText.SetActive(false);
            }
            lookingAtDoor = false;
        }

        // Code to open and close the door (play the door animations)
        if(lookingAtDoor == true && Input.GetAxis("Fire2") != 0 && doorOpened == false)
        {
            Debug.Log("Opened Door");
            doorAnimator.SetTrigger("opened");
            doorOpened = true;
            timerTemp = doorOpeningTime;
        }
        else if (lookingAtDoor == true && Input.GetAxis("Fire2") != 0 && doorOpened == true)
        {
            Debug.Log("Closed Door");
            doorAnimator.ResetTrigger("opened");
            doorOpened = false;
            timerTemp = doorOpeningTime;
        }

        timerTemp -= Time.deltaTime;
    }
}
