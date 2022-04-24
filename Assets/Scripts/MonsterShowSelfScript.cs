using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MonsterShowSelfScript : MonoBehaviour
{
    public SkinnedMeshRenderer monsterModel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            monsterModel.shadowCastingMode = ShadowCastingMode.On;
            Debug.Log("Show Yourself");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            monsterModel.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            Debug.Log("Away with Yourself");
        }
    }
}
