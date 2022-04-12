using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDelete : MonoBehaviour
{
    public GameObject soundObject;
    public AudioSource sound;

    private float soundTimer;
    // Start is called before the first frame update
    private void Awake()
    {
        soundTimer = sound.clip.length;
        Debug.Log(soundTimer);
        PlaySound();
    }

    public void PlaySound()
    {
        StartCoroutine(DeleteSound());
    }
    IEnumerator DeleteSound()
    {
        yield return new WaitForSeconds(soundTimer);
        Destroy(soundObject);
    }


}
