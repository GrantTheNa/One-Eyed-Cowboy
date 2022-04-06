using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWindBGM : MonoBehaviour
{
    private AudioManager theAM;

    public AudioClip Outside;
    public AudioClip Inside;

    // Start is called before the first frame update
    void Start()
    {
        theAM = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            theAM.ChangeWindBGM(Inside);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            theAM.ChangeWindBGM(Outside);
        }
    }
}
