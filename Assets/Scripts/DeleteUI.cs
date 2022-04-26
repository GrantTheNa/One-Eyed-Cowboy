using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeleteThis());
    }

    IEnumerator DeleteThis()
    {
        yield return new WaitForSeconds(5);
        this.gameObject.SetActive(false);
    }
}
