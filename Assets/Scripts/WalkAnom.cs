using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnom : MonoBehaviour
{
    public Animator animator;
    public Animator shadow;

    public FirstPersonController playerScript;

    float velocity = 0.0f;
    public float acceleration = 0.01f;
    public float deceleration = 0.05f;
    int VelocityHash;

    bool isWalking;
    bool playingSound;

    // Start is called before the first frame update
    void Start()
    {
        // Get player script
        playerScript.GetComponent<FirstPersonController>();

        VelocityHash = Animator.StringToHash("Velocity");

        animator.SetBool("Crouch", false);
        shadow.SetBool("Crouch", false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckSprinting();
        CheckWalking();
        CrouchAnimation();

        //animation velocity change
        animator.SetFloat(VelocityHash, velocity);
        shadow.SetFloat(VelocityHash, velocity);

        //////walking sounds/////
        if (isWalking == true && playingSound == false && playerScript.isGrounded == true)
        {
            if (playerScript.isSprinting == false)
            {
                StartCoroutine(WalkSound());
            }
            else
            {
                StartCoroutine(SprintSound());
            }
        }

        IEnumerator WalkSound()
        {
            playingSound = true;
            FoodStepRock();
            yield return new WaitForSeconds(0.5f);
            playingSound = false;
        }

        IEnumerator SprintSound()
        {
            playingSound = true;
            FoodStepRock();
            yield return new WaitForSeconds(0.33333333333f);
            playingSound = false;
        }
    }

    private void CheckSprinting()
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
    }
    private void CheckWalking()
    {
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
            velocity -= Time.deltaTime * deceleration * 4;
            isWalking = false;
        }
    }
    private void CrouchAnimation()
    {
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

    private void FoodStepRock()
    {
        float randomNumber = Random.Range(1, 5);
        if (randomNumber == 1)
        {
            FindObjectOfType<MasterAudioManager>().Play("Footstep_Rock_Walk_01");
        }
        else if (randomNumber == 2)
        {
            FindObjectOfType<MasterAudioManager>().Play("Footstep_Rock_Walk_02");
        }
        else if (randomNumber == 3)
        {
            FindObjectOfType<MasterAudioManager>().Play("Footstep_Rock_Walk_03");
        }
        else if (randomNumber == 4)
        {
            FindObjectOfType<MasterAudioManager>().Play("Footstep_Rock_Walk_04");
        }
    }
}
