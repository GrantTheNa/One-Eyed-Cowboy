using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MonsterShowSelfScript : MonoBehaviour
{
    public SkinnedMeshRenderer monsterModel;
    public Material mat;
    public float A_Trans = 0.4f;
    private bool playerIn;

    private void Start()
    {
        StartCoroutine(MaterialFadeOut());

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIn = true;
            monsterModel.shadowCastingMode = ShadowCastingMode.On;
            StartCoroutine(MaterialFadeIn());
            Debug.Log("Show Yourself");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIn = false;
            StartCoroutine(MaterialFadeOut());
            Debug.Log("Away with Yourself");
        }
    }


    IEnumerator MaterialFadeIn()
    {
        float a = A_Trans; //Alpha
        a = 0;
        mat.color = new Color(0, 0, 0, a);
        while (mat.color.a <= A_Trans && playerIn)
        {
            a = a + 0.3f * Time.deltaTime;
            mat.color = new Color(0, 0, 0, a);
            yield return null; 
        }
    }

    IEnumerator MaterialFadeOut()
    {
        float a = A_Trans; //Alpha
        a = A_Trans;
        mat.color = new Color(0, 0, 0, a);
        while (mat.color.a >= 0f && !playerIn)
        {
            a = a - 0.3f * Time.deltaTime;
            mat.color = new Color(0, 0, 0, a);
            yield return null;
        }
        if (a <= 0)
        {
            Debug.Log("Cringe");
            mat.color = new Color(0, 0, 0, 1);
            monsterModel.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
        }
    }
}
