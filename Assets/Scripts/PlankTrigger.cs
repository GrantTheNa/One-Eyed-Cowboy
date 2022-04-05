using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankTrigger : MonoBehaviour
{
    public bool plankPositionTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Plank" && other.gameObject.tag != "Player")
        {
            plankPositionTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Plank" && other.gameObject.tag != "Player")
        {
            plankPositionTrigger = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Plank" && other.gameObject.tag != "Player")
        {
            plankPositionTrigger = true;
        }
    }
}
