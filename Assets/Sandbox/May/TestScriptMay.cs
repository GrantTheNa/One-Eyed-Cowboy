using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptMay : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f") == true)
        {
            Debug.Log("F");
            FindObjectOfType<MasterAudioManager>().Play("Window_Break");
        }
    }
}
