using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFastVehicleSpawn : MonoBehaviour
{

    Platform platformScript;

    void Start()
    {
        platformScript = gameObject.GetComponentInParent<Platform>();
    }

    // Update is called once per frame
    void Update()
    {
        



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && platformScript.randNum == 1){
            
            platformScript.startSpawning = true;
            platformScript.StartCoroutine("SpawnFastMovingVehicles");
            Debug.Log("THE LONG BRIDGE PLAFORM WILL BEGIN SPAWNING FAST VEHICLES");
        }
        else if(other.gameObject.CompareTag("Player") && platformScript.randNum == 0)
        {
            platformScript.startSpawning = true;
            platformScript.StartCoroutine("SpawnMovingVehicles");
        }
    }


}


