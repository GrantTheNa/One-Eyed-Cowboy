using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {

        }
    }
}
