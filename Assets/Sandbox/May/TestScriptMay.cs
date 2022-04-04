using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptMay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 x = Vector3.forward;
        Debug.DrawRay(transform.position, new Vector3(0.1f,0,1), Color.white, 10);
        Debug.DrawRay(transform.position, new Vector3(-0.1f, 0, 1), Color.white, 10);
        // Debug.DrawRay(transform.position, new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z), Color.white, 5);
    }
}
