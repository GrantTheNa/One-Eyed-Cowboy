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

    public bool pressedCrouch = false;

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
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (velocity! < 1.0f)
            {
                velocity += Time.deltaTime * acceleration * 7;
                Debug.Log(velocity);
                isWalking = true;
            }

        }

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


        //animation velocity change
        animator.SetFloat(VelocityHash, velocity);
        shadow.SetFloat(VelocityHash, velocity);

        //stop walking
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * deceleration * 4;
            isWalking = false;
        }


        //Crouch Animation
        if (Input.GetAxisRaw("Fire1") != 0)
        {
            animator.SetBool("Crouch", true);
            shadow.SetBool("Crouch", true);
            pressedCrouch = true;

        }
        else if (pressedCrouch == true)
        {
            pressedCrouch = false;
            animator.SetBool("Crouch", false);
            shadow.SetBool("Crouch", false);
        }
    }
}
