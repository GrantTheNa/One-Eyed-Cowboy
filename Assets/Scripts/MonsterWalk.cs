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


    // Start is called before the first frame update
    void Start()
    {
        MonsterScript.GetComponent<Monster>(); //get the player script reference.


        VelocityHash = Animator.StringToHash("Velocity");

    }

    // Update is called once per frame
    void Update()
    {
        if (MonsterScript.velocity >= 0.5f && MonsterScript.velocity <= 1.5f)
        {
            if (velocity! < 0.5f)
            {
                velocity += Time.deltaTime * acceleration * 7;
                Debug.Log(velocity);
            }
        }
        if (MonsterScript.velocity >= 1.5f)
        {
            if (velocity! < 1f)
            {
                velocity += Time.deltaTime * acceleration * 7;
                Debug.Log(velocity);
            }
        }

        animator.SetFloat(VelocityHash, velocity);

        if (MonsterScript.velocity <= 0.1f && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * deceleration * 4;
        }
    }
}
