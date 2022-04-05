using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInScript : MonoBehaviour
{
    Image sprite;
    public GameObject player;
    public GameObject enemy;

    float distance;

    float alphaValue;
    float targetValue;

    float full = 1;
    float half = 0.5f;
    float none = 0;

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
        distance = Vector3.Distance(player.transform.position, enemy.transform.position);
        Debug.Log(distance + "is how far the enemy is");

        if (distance >16)
        {
            targetValue = 0;
        }
        else if (distance <16 && distance >8)
        {
            targetValue = 0.3f;
        }
        else if (distance < 8 && distance > 4)
        {
            targetValue = 0.75f;
        }
        else if (distance <4)
        {
            targetValue = 1;
        }


        float delta = targetValue - alphaValue;
        delta *= Time.deltaTime;

        alphaValue += delta;

        sprite.color = new Color(1, 0, 0, alphaValue);
    }

}
