using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInScript : MonoBehaviour
{
    Image sprite;
    public Image _sBack;
    public Image _sLeft;
    public Image _sRight;
    public Image _sNorth;
    public GameObject player;
    public GameObject enemy;

    public float distance;
    public float distancePercent;

    public float closestMonsterDistance = 4;
    public float furthestMonsterDistance = 16;

    float alphaValue;

    public Quaternion enemyDirection; // fade dir
    public Vector3 playerDirection; // fade dir

    public RectTransform test; //Test Direction to point at enemy

    public int fadeMode;

    //Direction = destination - source


    // Start is called before the first frame update
    void Start()
    {
        //sprite = GetComponent<Image>();
        ImageClear();

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

        ///sprite.color = new Color(1, 0, 0, alphaValue);

        //Debug.Log(alphaValue);

        ////////////direction
        ///
        playerDirection.z = player.transform.eulerAngles.y;

        Vector3 dir = player.transform.position - enemy.transform.position;
        enemyDirection = Quaternion.LookRotation(dir);

        enemyDirection.z =- enemyDirection.y;
        enemyDirection.x = 0;
        enemyDirection.y = 0;

        test.localRotation = enemyDirection * Quaternion.Euler(playerDirection);

        float _floatDirection = test.localRotation.z;

        Debug.Log(_floatDirection);


        //Setting Up the direction of FADE WILL BE FIXED


        if (_floatDirection >= 0.7f)
        {
            fadeMode = 1;
            StartCoroutine(FadeFront());
            StartCoroutine(DefadeLeft());
            StartCoroutine(DefadeBack());
            StartCoroutine(DefadeRight());
            //Mode Front
        }
        else if (_floatDirection <= 0.7 && _floatDirection >= 0.3)
        {
            fadeMode = 2;
            StartCoroutine(FadeLeft());
            StartCoroutine(DefadeLeft());
            StartCoroutine(DefadeFront());
            StartCoroutine(DefadeRight());
            //Mode Left
        }
        else if (_floatDirection <= 0.3 && _floatDirection >= -0.3)
        {
            fadeMode = 3;
            StartCoroutine(FadeBack());
            StartCoroutine(DefadeLeft());
            StartCoroutine(DefadeFront());
            StartCoroutine(DefadeRight());

            //Mode Back
        }
        else if (_floatDirection >= -0.8 && _floatDirection <= -0.3)
        {
            fadeMode = 4;
            StartCoroutine(FadeRight());
            StartCoroutine(DefadeLeft());
            StartCoroutine(DefadeBack());
            StartCoroutine(DefadeFront());
            //Mode Right
        }
        else
        {
            StartCoroutine(DefadeLeft());
            StartCoroutine(DefadeBack());
            StartCoroutine(DefadeFront());
            StartCoroutine(DefadeRight());
        }
    }


    void ImageClear()
    {
        _sBack.color = new Color(1, 0, 0, 0);
        _sLeft.color = new Color(1, 0, 0, 0);
        _sRight.color = new Color(1, 0, 0, 0);
        _sNorth.color = new Color(1, 0, 0, 0);
    }

    IEnumerator FadeFront()
    {
        float a = _sNorth.color.a;

        while (_sNorth.color.a < alphaValue && fadeMode == 1)
        {
            a = a + 0.6f * Time.deltaTime;
            _sNorth.color = new Color(0, 0, 0, a);
            yield return null;
        }

    }

    IEnumerator DefadeFront()
    {
        float a = _sNorth.color.a;

        while (_sNorth.color.a > 0 && fadeMode != 1)
        {
            a = a - 0.3f * Time.deltaTime;
            _sNorth.color = new Color(0, 0, 0, a);
            yield return null;
        }

    }

    IEnumerator FadeLeft()
    {
        float a = _sLeft.color.a;

        while (_sLeft.color.a < alphaValue && fadeMode == 2)
        {
            a = a + 0.6f * Time.deltaTime;
            _sLeft.color = new Color(0, 0, 0, a);
            yield return null;
        }
    }

    IEnumerator DefadeLeft()
    {
        float a = _sLeft.color.a;

        while (_sLeft.color.a > 0 && fadeMode != 2)
        {
            a = a - 0.3f * Time.deltaTime;
            _sLeft.color = new Color(0, 0, 0, a);
            yield return null;
        }

    }

    IEnumerator FadeBack()
    {
        float a = _sBack.color.a;

        while (_sBack.color.a < alphaValue && fadeMode == 3)
        {
            a = a + 0.6f * Time.deltaTime;
            _sBack.color = new Color(0, 0, 0, a);
            yield return null;
        }
    }

    IEnumerator DefadeBack()
    {
        float a = _sBack.color.a;

        while (_sBack.color.a > 0 && fadeMode != 3)
        {
            a = a - 0.3f * Time.deltaTime;
            _sBack.color = new Color(0, 0, 0, a);
            yield return null;
        }

    }

    IEnumerator FadeRight()
    {
        float a = _sRight.color.a;

        while (_sRight.color.a < alphaValue && fadeMode == 4)
        {
            a = a + 0.6f * Time.deltaTime;
            _sRight.color = new Color(0, 0, 0, a);
            yield return null;
        }
    }

    IEnumerator DefadeRight()
    {
        float a = _sRight.color.a;

        while (_sRight.color.a > 0 && fadeMode != 4)
        {
            a = a - 0.3f * Time.deltaTime;
            _sRight.color = new Color(0, 0, 0, a);
            yield return null;
        }

    }

}
