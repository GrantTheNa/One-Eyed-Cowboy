using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWayPoints : MonoBehaviour
{
    GameObject wayPoints;
    // Start is called before the first frame update
    void Start()
    {
        wayPoints = this.gameObject;
        wayPoints.SetActive(false);
    }
}
