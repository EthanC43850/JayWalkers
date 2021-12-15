using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    
    PlatformSpawner platformSpawnerScript;
    GameManager gameManagerScript;
    public int randNum = 0;
    public bool horizontalObstacles;
    public bool spaceTaken;
    public bool startSpawning;
    public GameObject[] obstacleArray;
    public GameObject[] slowMovingObstacleArray;
    public GameObject[] fastMovingObstacleArray;


    public GameObject movingObjectSpawnPoint;
    public GameObject staticObstacleSpawnPoint;

    int obstacleIndex;
    public float fastObstacleSpawnRate;
        

    /*
    Obstacle Key:
    * randNum = 0 - this 
    *
    *
    *
    *
    *
    */

    void Start()
    {
        platformSpawnerScript = GameObject.Find("Platform Spawner").GetComponent<PlatformSpawner>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();

        //randNum = Random.Range(0, 2);


        /*if (obstacleArray.Length > 0)  //randNum == 1 && movingObstacleArray.Length > 0 <----- change to this
        {
            //StartCoroutine("SpawnMovingVehicles");

        }
        else if(slowMovingObstacleArray.Length > 0 && randNum == 1)
        {
            Debug.Log("spawn fast vehicles"); //the code for this is in another script
            
        }
        else
        {
            
        }*/

        SpawnObstacles();

    }

    void SpawnObstacles()
    {
            obstacleIndex = Random.Range(0, obstacleArray.Length-1);
            //Debug.Log("Obstacle index number is " + obstacleIndex);
            Instantiate(obstacleArray[obstacleIndex], staticObstacleSpawnPoint.transform.position, Quaternion.identity);
    }
    

    IEnumerator SpawnSlowMovingVehicles()
    {
        while (startSpawning == true)
        {
            obstacleIndex = Random.Range(0, slowMovingObstacleArray.Length);
            if(spaceTaken == true)
            {
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                Instantiate(slowMovingObstacleArray[obstacleIndex], movingObjectSpawnPoint.transform.position, Quaternion.identity); // Multidimensional array to create cool obstacle patterns
            }
            yield return new WaitForSeconds(2.0f);
        }
    }   //W.I.P

    
    IEnumerator SpawnFastMovingVehicles()
    {
        //Begin spawning when player hits trigger
        while (startSpawning == true)
        {
            obstacleIndex = Random.Range(0, fastMovingObstacleArray.Length);
            if (spaceTaken == true)                                                 //Ensure vehicles dont over spawn
            {
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                Instantiate(fastMovingObstacleArray[obstacleIndex], movingObjectSpawnPoint.transform.position, Quaternion.identity); // Multidimensional array to create cool obstacle patterns
            }

            yield return new WaitForSeconds(fastObstacleSpawnRate);
        }
    }





    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            startSpawning = false;
        }
    }


    




}
