using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class DebugMenu : MonoBehaviour
{
    // Variables that need assigning
    public GameObject debugMenu;
    public Text xSpeedText, ySpeedText, zSpeedText, isGroundedText;
    public SkinnedMeshRenderer monsterModel;
    public Outline monsterOutline;

    // Variables that need to be accessed
    public float xSpeed, ySpeed, zSpeed;
    public bool isGrounded;

    // Private Variables
    private bool menuActive;

    // Menu off by default
    private void Start()
    {
        menuActive = false;
        monsterModel = GameObject.Find("ShadowModel").GetComponent<SkinnedMeshRenderer>();
        monsterOutline = GameObject.Find("ShadowModel").GetComponent<Outline>();
    }

    void Update()
    {
        // Checks if the letters O and P are pressed
        if (Input.GetKey("o") == true && Input.GetKeyDown("p") == true ||
            Input.GetKeyDown("o") == true && Input.GetKey("p") == true)
        {
            menuActive = !menuActive;
        }

        // Turns on the debug menu
        if (menuActive == true)
        {
            debugMenu.SetActive(true);
            monsterModel.shadowCastingMode = ShadowCastingMode.On;
            monsterOutline.enabled = true;
        }
        // Turns it off
        else
        {
            debugMenu.SetActive(false);
            monsterModel.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            monsterOutline.enabled = false;
        }

        // Updates debug values if on. Values are sent through the controller script
        if (menuActive == true)
        {
            xSpeedText.text = "xSpeed: " + xSpeed.ToString("F2");
            ySpeedText.text = "ySpeed: " + ySpeed.ToString("F2");
            zSpeedText.text = "zSpeed: " + zSpeed.ToString("F2");
            isGroundedText.text = "isGrounded: " + isGrounded;
        }
    }
}
