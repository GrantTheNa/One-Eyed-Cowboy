using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTriggerBox : MonoBehaviour
{
    public FirstPersonController fpsScript;
    public GameObject triggerBox;
    public BridgeScript bridgeScript;

    public void Start()
    {
        triggerBox = GameObject.Find("BridgeTrigger");
        bridgeScript = FindObjectOfType<BridgeScript>();
        fpsScript = FindObjectOfType<FirstPersonController>();
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Plank")
        {
            Debug.Log("Plank entered trigger");
            Destroy(collider.gameObject);
            bridgeScript.woodCount++;
            Destroy(this.gameObject);
            fpsScript.holdingWood = false;
        }
    }

}