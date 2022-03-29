using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    //Interactable item
    public GameObject item;

    //Empty game object in front of player
    public GameObject tempParent;
    
    Vector3 objectPos;  

    //Strength of throw on object
    public float throwForce = 1200;  
    
    
    float distance;

    public bool canHold = true;    
    public bool isHolding = false;
  

    public void Update()
    {
        distance = Vector3.Distance(item.transform.position, tempParent.transform.position);
        
        if (distance >= 2f)
            {
                isHolding = false;
            }         


        //Check if isholding
        if (isHolding == true)
        {
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.transform.SetParent(tempParent.transform);
            item.GetComponent<Rigidbody>().useGravity = false;


            //if Players presses Right Mouse Button, throw held object
            if (Input.GetMouseButtonDown(1))
            {
                item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
                isHolding = false;
            }
        }

        //is isholding is false
        else
        {
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;
            Outline();
        }      
        
    }
    //if you are close enough to the object, and you click and hold LMB, you can pick up the object
    void OnMouseDown()
        {
            if (distance <= 2f)
            {
                isHolding = true;
            }
        }
    //if you release LMB, you drop the object
    void OnMouseUp()
    {
        isHolding = false;       
    }
    
    //Outlines the object
    public void Outline()
    {
        if (item.CompareTag("Item")) //|| item.CompareTag("ItemPile"))
        {
            if (distance <= 2f)
            {
                GetComponent<Outline>().enabled = true;
            }
            else
            {
                GetComponent<Outline>().enabled = false;
            }
        }
    }
}
