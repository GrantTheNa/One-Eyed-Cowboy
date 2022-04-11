using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource BGM;

    public AudioSource HowlingWind;

    public float maxBGMVol = 0.5f;

    public void ChangeWindBGM(AudioClip music)
    {
        float clipLength = HowlingWind.time;
        if (HowlingWind.clip.name == music.name)
            return;
        HowlingWind.Stop();
        HowlingWind.clip = music;
        HowlingWind.Play();
        HowlingWind.time = clipLength;
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

        while (BGM.volume < maxBGMVol || BGM.volume <= maxBGMVol - 0.05f)
        {
            BGM.volume += startVolume * Time.deltaTime / 100;

            yield return null;
        }
    }



}
