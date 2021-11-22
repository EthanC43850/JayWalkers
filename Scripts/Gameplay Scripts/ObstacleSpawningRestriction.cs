using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawningRestriction : MonoBehaviour
{
    Platform platformScript;
    

    void Start()
    {
        platformScript = gameObject.GetComponentInParent<Platform>();
    }
    private void OnTriggerStay(Collider other) //This script checks to make sure that moving vehicle objects do not overlap or crash into each other during playtime
    {
        if(other.gameObject.tag == "Obstacle")
        {
            platformScript.spaceTaken = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        platformScript.spaceTaken = false;
    }


}
