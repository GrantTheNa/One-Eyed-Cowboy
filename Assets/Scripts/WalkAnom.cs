using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAnom : MonoBehaviour
{
    public Animator animator;

    public FirstPersonController playerScript; //Get player Script

    float velocity = 0.0f;
    public float acceleration = 0.01f;
    public float deceleration = 0.05f;
    int VelocityHash;

    public bool pressedCrouch = false;


    // Start is called before the first frame update
    void Start()
    {
        playerScript.GetComponent<FirstPersonController>(); //get the player script reference.

        VelocityHash = Animator.StringToHash("Velocity");

        animator.SetBool("Crouch", false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (velocity! < 1.0f)
            {
                velocity += Time.deltaTime * acceleration * 7;
                Debug.Log(velocity);
            }

        }

        animator.SetFloat(VelocityHash, velocity);

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0 && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * deceleration * 4;
        }


        //Crouch Animation
        if (Input.GetAxisRaw("Fire1") != 0)
        {
            animator.SetBool("Crouch", true);
            pressedCrouch = true;

        }
        else if (pressedCrouch == true)
        {
            pressedCrouch = false;
            animator.SetBool("Crouch", false);
        }
    }
}
