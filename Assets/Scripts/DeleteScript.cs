using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteScript : MonoBehaviour
{
    public GameObject prefab;
    public Transform monster;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Instantiate(prefab, monster);
            FindObjectOfType<MasterAudioManager>().Play3D("Window_Break", monster, prefab);
        }
    }
}
