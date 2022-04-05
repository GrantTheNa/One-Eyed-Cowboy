using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public Transform wayPoint;
    public GameObject monsterGameObject;
    public Monster monsterScript;
    // Start is called before the first frame update
    void Start()
    {
        monsterGameObject = GameObject.FindGameObjectWithTag("Enemy");
        monsterScript = monsterGameObject.GetComponent<Monster>(); //get the monster script reference.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Thrown")
        {
            Destroy(this.gameObject);
            monsterScript.distractionPoint = wayPoint;
            monsterScript.distracted = true;
        }

    }
}
