using System.Collections;
using UnityEngine;

public class SwitchBGMTrigger : MonoBehaviour
{
    public AudioClip Ambient;
    public AudioClip Closeby;
    public AudioClip Touch;

    private AudioManager theAM;

    private FadeInScript theme;

    private GameObject monsterGameObject;
    private Monster monsterScript;

    bool chasedOST = false;


    // Start is called before the first frame update
    void Start()
    {
        theAM = FindObjectOfType<AudioManager>();
        theme = this.gameObject.GetComponent<FadeInScript>();
        monsterGameObject = GameObject.FindGameObjectWithTag("Enemy");
        monsterScript = monsterGameObject.GetComponent<Monster>(); //get the monster script reference.
    }

    // Update is called once per frame
    void Update()
    {
        if (monsterScript.foundPlayer)
        {
            chasedOST = true;
            theAM.Play();
            theAM.ChangeBGM(Touch);
            theAM.FadeIn();
        }
        else if (!monsterScript.foundPlayer && chasedOST)
        {
            //give room time for OST to keep playing
            ChasedOSTDelay();
        }

        if (!chasedOST)
        {
            if (theme.distancePercent == 0)
            {
                theAM.Silence();
            }
            else
            {
                theAM.FadeIn();
                theAM.Play();
                theAM.ChangeBGM(Closeby);
            }
        }

    }


    public void ChasedOSTDelay()
    {
        StartCoroutine(DelayFade());
    }

    IEnumerator DelayFade()
    {
        theAM.FadeOut();
        yield return new WaitForSeconds(8f);
        if (!monsterScript.foundPlayer)
        {
            chasedOST = false;
        }
    }


}
