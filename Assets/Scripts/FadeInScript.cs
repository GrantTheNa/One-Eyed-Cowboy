using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInScript : MonoBehaviour
{
    Image sprite;
    public GameObject player;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<Image>();

        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Z))
        {
            sprite.color = new Color(1, 0, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            sprite.color = new Color(1, 0, 0, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            sprite.color = new Color(1, 0, 0, 0);
        }
    }

}
