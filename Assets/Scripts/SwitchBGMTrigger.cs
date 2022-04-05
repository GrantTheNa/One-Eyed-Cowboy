using System.Collections;
using UnityEngine;

public class SwitchBGMTrigger : MonoBehaviour
{
    public AudioClip newTrack;

    private AudioManager theAM;

    private FadeInScript theme;


    // Start is called before the first frame update
    void Start()
    {
        theAM = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        theAM.ChangeBGM(newTrack);
    }

}
