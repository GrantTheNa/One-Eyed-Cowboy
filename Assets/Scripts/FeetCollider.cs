using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollider : MonoBehaviour
{
    // Variables that need assigning
    public bool isGrounded;

    //check if Player is on the Wood
    public bool woodF;

    // This script will detect if the player is grounded or not by using a mesh collider cylinder below the player.
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player" && other.tag != "Trigger" && other.tag != "Enemy")
        {
            isGrounded = true;
        }
        if(other.tag == "WoodF")
        {
            woodF = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Trigger" && other.tag != "Enemy")
        {
            isGrounded = true;
        }
        if (other.tag == "WoodF")
        {
            woodF = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Trigger" && other.tag != "Enemy")
        {
            isGrounded = false;
        }
        if (other.tag == "WoodF")
        {
            woodF = false;
        }
    }
}
