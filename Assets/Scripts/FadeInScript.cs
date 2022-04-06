using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInScript : MonoBehaviour
{
    Image sprite;
    public GameObject player;
    public GameObject enemy;

    public float distance;
    public float distancePercent;

    public float closestMonsterDistance = 4;
    public float furthestMonsterDistance = 16;

    float alphaValue;

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

        // Determines how close the monster is as a percentage based on minimun and maximum distances given.
        // Using this is more accurate than the previous targetValue code, and more adjustable
        distancePercent = 1 - (distance - closestMonsterDistance) / (furthestMonsterDistance - closestMonsterDistance);
        if (distancePercent < 0) {distancePercent = 0;}
        else if (distancePercent > 1) {distancePercent = 1;}
        //Debug.Log(distancePercent);

        float delta = distancePercent - alphaValue;
        delta *= Time.deltaTime;

        alphaValue += delta;

        sprite.color = new Color(1, 0, 0, alphaValue);
    }

}
