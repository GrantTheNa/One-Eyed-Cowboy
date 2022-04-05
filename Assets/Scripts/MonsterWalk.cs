using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWalk : MonoBehaviour
{
    public Animator animator;

    public Monster MonsterScript; //Get player Script

    float velocity = 0.0f;
    public float acceleration = 0.01f;
    public float deceleration = 0.05f;
    int VelocityHash;

    public bool pressedCrouch = false;

    public bool isWalking;
    bool isRunning;
    bool playingSound;

    // Start is called before the first frame update
    void Start()
    {
        MonsterScript.GetComponent<Monster>(); //get the player script reference.


        VelocityHash = Animator.StringToHash("Velocity");

    }

    // Update is called once per frame
    void Update()
    {
        //Walking
        if (MonsterScript.velocity >= 0.5f && MonsterScript.velocity <= 1.5f)
        {
            if (velocity! < 0.5f)
            {
                velocity += Time.deltaTime * acceleration * 7;
                //Debug.Log(velocity);
                isWalking = true;
                isRunning = false;
            }
        }

        //Running
        if (MonsterScript.velocity >= 1.5f)
        {
            if (velocity! < 1f)
            {
                velocity += Time.deltaTime * acceleration * 7;
                //Debug.Log(velocity);
                isWalking = true;
                isRunning = true;
            }
        }

        //////walking sounds/////
        if (isWalking == true && GetComponent<AudioSource>().isPlaying == false && playingSound == false)
        {
            if (isRunning == false)
            {
                StartCoroutine(WalkSound());
            }
            else if (isRunning == true)
            {
                StartCoroutine(RunSound());

            }

        }

        IEnumerator WalkSound()
        {
            playingSound = true;
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1f);
            playingSound = false;
        }

        IEnumerator RunSound()
        {
            playingSound = true;
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.15f);
            playingSound = false;
        }


        animator.SetFloat(VelocityHash, velocity);

        //Standing
        if (MonsterScript.velocity <= 0.1f && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * deceleration * 4;
            isWalking = false;
        }
    }
}
