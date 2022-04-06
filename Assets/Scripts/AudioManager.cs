using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource BGM;

    public AudioSource HowlingWind;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeBGM(AudioClip music)
    {
        if (BGM.clip.name == music.name)
            return;
        BGM.Stop();
        BGM.clip = music;
        BGM.Play();
    }
    public void Silence()
    {
        BGM.Stop();
    }

    public void Play()
    {
        if (!BGM.isPlaying)
        {
            BGM.Play();
        }
    }

    //Fade out
    public void FadeOut() //Fade out
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        float startVolume = BGM.volume;
        float FadeTime = 500f;

        while (BGM.volume > 0)
        {
            BGM.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
    }

    //Fade In

    public void FadeIn()
    {
        StartCoroutine(FadeBack());
    }

    IEnumerator FadeBack()
    {
        float startVolume = BGM.volume + 0.01f;

        while (BGM.volume < 1.0f)
        {
            BGM.volume += startVolume * Time.deltaTime / 100;

            yield return null;
        }
    }



}
