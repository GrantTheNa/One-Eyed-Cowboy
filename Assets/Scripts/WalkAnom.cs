using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnom : MonoBehaviour
{
    public Animator animator;
    public Animator shadow;

    public FirstPersonController playerScript; //Get player Script

    float velocity = 0.0f;
    public float acceleration = 0.01f;
    public float deceleration = 0.05f;
    int VelocityHash;

    bool isWalking;
    bool playingSound;

    // Start is called before the first frame update
    void Start()
    {
        playerScript.GetComponent<FirstPersonController>(); //get the player script reference.

        VelocityHash = Animator.StringToHash("Velocity");

        animator.SetBool("Crouch", false);
        shadow.SetBool("Crouch", false);
    }

    // Update is called once per frame
    void Update()
    {

        if (playerScript.isSprinting == true)
        {
            animator.SetBool("Sprint", true);
            shadow.SetBool("Sprint", true);
        }
        else
        {
            animator.SetBool("Sprint", false);
            shadow.SetBool("Sprint", false);
        }

        //walking 
        if (playerScript.xSpeed >= 0.5 || playerScript.xSpeed <= -0.5 || playerScript.zSpeed >= 0.5 || playerScript.zSpeed <= -0.5)
        {
            if (velocity! < 1.0f)
            {
                velocity += Time.deltaTime * acceleration * 7;
                // Debug.Log(velocity);
                isWalking = true;
            }
        }
        else
        {
            //stop walking
            velocity -= Time.deltaTime * deceleration * 4;
            isWalking = false;

        }

        //animation velocity change
        animator.SetFloat(VelocityHash, velocity);
        shadow.SetFloat(VelocityHash, velocity);

        //////walking sounds/////
        if (isWalking == true && playingSound == false && playerScript.isGrounded == true && playerScript.isSprinting == false)
        {
            StartCoroutine(WalkSound());
        }
        else if (isWalking == true && playingSound == false && playerScript.isGrounded == true && playerScript.isSprinting == true)
        {
            StartCoroutine(SprintSound());
        }

        IEnumerator WalkSound()
        {
            playingSound = true;
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.5f);
            playingSound = false;
        }

        IEnumerator SprintSound()
        {
            playingSound = true;
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.33333333333f);
            playingSound = false;
        }




        //Crouch Animation
        if (playerScript.pressedCrouch == true)
        {
            animator.SetBool("Crouch", true);
            shadow.SetBool("Crouch", true);

        }
        else
        {
            animator.SetBool("Crouch", false);
            shadow.SetBool("Crouch", false);
        }
    }
}
