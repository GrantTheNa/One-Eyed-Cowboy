using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
    // Variables that need assigning
    public CharacterController playerCharacterController;
    public FeetCollider playerFeet;
    public DebugMenu debugMenu;
    public Camera playerCamera;
    public GameObject animator;
    public Slider leftStaminaSlider;
    public Slider rightStaminaSlider;
    public GameObject leftMouseButton;
    public GameObject rightMouseButton;

    // Variables that need adjusting
    public float movementSpeed = 10;
    public float crouchSpeedMultiplier = 0.4f;
    public float woodSpeedMultiplier = 0.5f;
    public float backwardsSpeedMultiplier = 0.75f;
    public float mouseSensitivity = 2;
    public float jumpHeight = 2;
    public float fallSpeed = 5;
    public float staminaMax = 10;
    public float sprintJump = 1.5f;
    public float sprintSpeedMultiplier = 1.6f;
    public float sprintUsage = 2;
    public float sprintRegen = 0.5f;

    // Variables that need accessing
    public float xSpeed, ySpeed, zSpeed;
    public bool isGrounded, isSprinting, isMovingOnGround, pressedCrouch, holdingWood, isHolding;

    // Private Variables
    private float mouseX, mouseY, stepOffset, speedMultiplier, stamina, staminaCooldown;

    // Start is called before the first frame update
    void Start()
    {
        // Deactivates the cursor and locks it to the middle of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Assigning variables
        leftStaminaSlider = GameObject.Find("StaminaMeterLeft").GetComponent<Slider>();
        rightStaminaSlider = GameObject.Find("StaminaMeterRight").GetComponent <Slider>();
        debugMenu = GameObject.Find("Canvas").GetComponent<DebugMenu>();

        // Sets the step off set to what it is set in Unity
        stepOffset = playerCharacterController.stepOffset;

        // Sets the speed multiplier to be regular at 1
        speedMultiplier = 1;

        // Sets stamina to max, and assigns stamina value to sliders
        stamina = staminaMax;
        leftStaminaSlider.maxValue = staminaMax;
        rightStaminaSlider.maxValue = staminaMax;

        // Sets mouse control images
        leftMouseButton = GameObject.Find("MouseLeftClick");
        rightMouseButton = GameObject.Find("MouseRightClick");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is grounded
        isGrounded = playerFeet.isGrounded;

        if (xSpeed != 0 && isGrounded == true)
        {
            isMovingOnGround = true;
        }
        else if (zSpeed != 0 && isGrounded == true)
        {
            isMovingOnGround = true;
        }
        else
        {
            isMovingOnGround = false;
        }

        // Checks is ctrl is held and player is not in midair
        if (Input.GetAxisRaw("Fire1") != 0 && isGrounded == true && ySpeed == 0)
        {
            Crouch();
        }
        // Checks if player is already crouched
        else if (pressedCrouch == true)
        {
            pressedCrouch = false;
            Stand();
        }
        // allows sprint if player is not crouched or in midair
        else if (isGrounded == true && ySpeed == 0)
        {
            Sprint();
        }

        FirstPersonCamera();
        if (holdingWood == true)
        {
            speedMultiplier *= woodSpeedMultiplier;
        }

        HorizontalMovement();
        VerticalMovement();
        Movement();
        StaminaRegen();
        UpdateSprintBar();

        if (speedMultiplier == sprintSpeedMultiplier && isGrounded == true && ySpeed == 0)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
        MouseControls();
        DebugMenu();
    }
    void Movement()
    {
        // Sets move as a Vector 3 to gather all speeds into one variable
        Vector3 move = transform.right * xSpeed + transform.forward * zSpeed + transform.up * ySpeed;

        // Connects to the character controller and makes the player move
        playerCharacterController.Move(move * Time.deltaTime * movementSpeed);
    }

    void FirstPersonCamera()
    {
        // Gets mouse movements
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Clamps and restricts mouse movements
        mouseY = Mathf.Clamp(mouseY, -80, 80);

        // Applies mouse movements to the player and camera
        playerCamera.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        playerCharacterController.transform.rotation = Quaternion.Euler(0, mouseX, 0);
    }

    void HorizontalMovement()
    {
        // Gets the movement of wsad
        xSpeed = Input.GetAxis("Horizontal");
        zSpeed = Input.GetAxis("Vertical");

        xSpeed = xSpeed * speedMultiplier;
        zSpeed = zSpeed * speedMultiplier;

        // Detects if player is running backwards, and slows their speed down
        if(zSpeed < 0)
        {
            zSpeed *= backwardsSpeedMultiplier;
        }

        // Checks if both horizontal and vertical movement is active
        if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
        {
            // Speeds are divided to prevent diagonal movement from being faster
            xSpeed *= 0.75f;
            zSpeed *= 0.75f;
        }
    }

    void VerticalMovement()
    {
        // Checks if the player can jump
        if (Input.GetAxis("Jump") != 0 && isGrounded == true && ySpeed == 0)
        {
            // Starts the jump and prevents stepping
            ySpeed = jumpHeight;
            playerCharacterController.stepOffset = 0;
        }

        if (isGrounded == false)
        {
            // Makes the player fall and prevents stepping
            ySpeed -= fallSpeed * Time.deltaTime;
            playerCharacterController.stepOffset = 0;
        }

        // Checks if player lands from a jump
        if (isGrounded == true && ySpeed < 0)
        {
            // Removes falling speed and enables stepping
            ySpeed = 0;
            playerCharacterController.stepOffset = stepOffset;
        }

    }

    private void Crouch()
    {
        if (pressedCrouch == false)
        {
            // Finds the y position of the animator, and moves it down
            float yposition;
            yposition = animator.transform.position.y;
            yposition -= 0.7f;
            animator.transform.position = new Vector3(animator.transform.position.x, yposition, animator.transform.position.z);
            // pressedCrouch ensures the camera only moves down once
            pressedCrouch = true;
        }
        // Sets the speed multiplier to be the crouch speed
        speedMultiplier = crouchSpeedMultiplier;
    }

    private void Stand()
    {
        // Will undo the camera movement from crouching
        float yposition;
        yposition = animator.transform.position.y;
        yposition += 0.7f;
        animator.transform.position = new Vector3(animator.transform.position.x, yposition, animator.transform.position.z);
        // Sets speed back to regular
        speedMultiplier = 1;
    }

    private void DebugMenu()
    {
        // Sends values to the debug menu script
        debugMenu.xSpeed = xSpeed;
        debugMenu.ySpeed = ySpeed;
        debugMenu.zSpeed = zSpeed;
        debugMenu.isGrounded = isGrounded;
    }

    private void Sprint()
    {
        // Checks if player is holding sprint, has stamina, and is not in a cooldown period
        if (Input.GetAxisRaw("Fire3") != 0 && stamina > 0 && staminaCooldown == 0 & isMovingOnGround == true)
        {
            // Sets speed to sprint
            speedMultiplier = sprintSpeedMultiplier;
            // Checks if the player has jumped during the sprint
            if (Input.GetAxis("Jump") != 0 && isGrounded == true && ySpeed == 0)
            {
                stamina -= sprintJump;
            }
            // Use stamina and prevent stamina from being a negative number
            stamina -= Time.deltaTime * sprintUsage;
            if (stamina < 0)
            {
                stamina = 0;
            }    
        }
        // Checks is stamina is 0 and player is not on a cooldown period, activates cooldown if so
        else if (stamina == 0 && staminaCooldown == 0)
        {
            staminaCooldown = 2;
            speedMultiplier = 1;
        }
        else
        {
            // Sets to regular speed
            speedMultiplier = 1;
        }
    }

    private void StaminaRegen()
    {
        // Checks if player isn't sprinting first
        if (speedMultiplier != sprintSpeedMultiplier)
        {
            if (staminaCooldown > 0)
            {
                // Reduces cooldown, and prevents it from becoming a negative number. Increases stamina once at 0
                staminaCooldown -= Time.deltaTime;
                if (staminaCooldown <= 0)
                {
                    staminaCooldown = 0;
                    stamina += Time.deltaTime * sprintRegen;
                }
            }
            else
            {
                // Stamina regeneration
                stamina += Time.deltaTime * sprintRegen;
                // Prevents it from going over set stamina maximum
                if (stamina > staminaMax)
                {
                    stamina = staminaMax;
                }
            }
        }

    }

    private void UpdateSprintBar()
    {
        // Updates the sliders to show sprint level
        leftStaminaSlider.value = stamina;
        rightStaminaSlider.value = stamina;
    }

    private void MouseControls()
    {
        if (isHolding == true || holdingWood == true)
        {
            leftMouseButton.SetActive(true);
        }
        else
        {
            leftMouseButton.SetActive(false);
        }

        if (isHolding == true)
        {
            rightMouseButton.SetActive(true);
        }
        else
        {
            rightMouseButton.SetActive(false);
        }
    }
}
