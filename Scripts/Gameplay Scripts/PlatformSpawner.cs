using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    public GameObject[] platforms;
    private Vector3 nextSpawnPos;
    public GameObject startPos;



    void Start()
    {
        SpawnInitialPlatforms();
    }

    public void SpawnInitialPlatforms()
    {
        nextSpawnPos = new Vector3(startPos.transform.position.x, startPos.transform.position.y, startPos.transform.position.z); //Create a start position for my platforms to spawn on.
        for (int i = 0; i < 4; i++)
        {
            if(i == 2)
            {
                GameObject temp = Instantiate(platforms[0], nextSpawnPos, Quaternion.identity); //Spawn mini bridge platform 2 times at the start
                nextSpawnPos = temp.transform.GetChild(1).position;
            }
            else
            {
                GameObject temp = Instantiate(platforms[0], nextSpawnPos, Quaternion.identity); //Spawn mini bridge platform 2 times at the start
                nextSpawnPos = temp.transform.GetChild(1).position;
            }
            
        }
        //SpawnPlatform(); delete me
    }


    public void SpawnPlatform() 
    {
        int platformIndex = Random.Range(0, platforms.Length);
        GameObject temp = Instantiate(platforms[platformIndex], nextSpawnPos, Quaternion.identity);
        nextSpawnPos = temp.transform.GetChild(1).position;
        
    }
    

}
