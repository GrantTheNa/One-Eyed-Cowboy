using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTriggerBox : MonoBehaviour
{
    public FirstPersonController fpsScript;

    public GameObject woodenPlankBridge;
    [SerializeField] private GameObject invisWall;

    public void Start()
    {
        fpsScript = FindObjectOfType<FirstPersonController>();
        woodenPlankBridge.SetActive(false);
        invisWall = this.transform.Find("InvisWall").gameObject;
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Plank")
        {
            Debug.Log("Plank entered trigger");
            Destroy(collider.gameObject);
            Destroy(this.gameObject);
            fpsScript.holdingWood = false;
            woodenPlankBridge.SetActive(true);
        }
    }

}
