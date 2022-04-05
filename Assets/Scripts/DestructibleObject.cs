using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    GameObject DestrObject;
    bool isDestroyed;

    public Transform wayPoint;
    public GameObject monsterGameObject;
    public Monster monsterScript;
    public MonsterWalk monsterWalkScript;

    //bool while check
    bool whileLoop;
    // Start is called before the first frame update
    void Start()
    {
        DestrObject = this.gameObject;

        monsterGameObject = GameObject.FindGameObjectWithTag("Enemy");
        monsterScript = monsterGameObject.GetComponent<Monster>(); //get the monster script reference.
        monsterWalkScript = monsterGameObject.GetComponent<MonsterWalk>(); //get the monster script reference.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Thrown" && !isDestroyed)
        {
            DestrObject.GetComponent<MeshRenderer>().enabled = false;
            isDestroyed = true;
            GetComponent<AudioSource>().Play();
            if (monsterWalkScript.isWalking)
            {
                DistractMonster();
                Debug.Log("DO THAT");
            }
            else if (!monsterWalkScript.isWalking)
            {
                StartCoroutine(WhileLoop());
            }

        }

    }

    IEnumerator WhileLoop()
    {
        whileLoop = true;

        Debug.Log("PreWhile");
        while (whileLoop)
        {
            Debug.Log("LOOP THAT SHIT");
            yield return new WaitForSeconds(0.2f);
            DistractMonster();
        }
    }

    public void DistractMonster()
    {
        if (monsterWalkScript.isWalking)
        {
            Debug.Log("Distraction on Sight");
            whileLoop = false;
            monsterScript.distractionPoint = wayPoint;
            monsterScript.distracted = true;
        }

    }
}
