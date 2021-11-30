using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public bool isVehicleBehind;
    public float rayCastLength;
    public float wheelSpinSpeed;
    public int laneNumber;
    
    RaycastHit hit;

    public GameManager gameManagerScript;

    public GameObject[] wheelList;
    public Transform[] lanes;  //Do not manually assign lanes in the inspector. This program automatically finds the transforms.


    [SerializeField] LayerMask vehicleMask;

    void Start()
    {
        //Debug.Log("Game started");
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        wheelSpinSpeed = Random.Range(300.0f, 800.0f);
        for(int i = 0; i < 4; i++)
        {
            lanes[i] = GameObject.Find("RoadLanes").transform.GetChild(i); //This allows each prefab to recognize which location each lane is in and assigns it to an array
        }


        //transform.position = new Vector3(transform.position.x, transform.position.y, lanes[laneNumber].transform.position.z); //Places vehicle in lane number

    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, lanes[laneNumber].transform.position.z); //keeps vehicle in lane number
        WheelSpin();
        Vector3 origin = transform.position + Vector3.up * 2.0f;
        Vector3 direction = transform.TransformDirection(Vector3.back);
        isVehicleBehind = Physics.Raycast(origin, direction, rayCastLength, vehicleMask);
        Debug.DrawRay(transform.position + Vector3.up * 2.0f, transform.TransformDirection(Vector3.back) * rayCastLength, Color.blue);

        //If a fast car comes up behind a slow vehicle it will honk and make the vehicle in front go faster
        if (Physics.Raycast(origin, direction, out hit, rayCastLength, vehicleMask) && gameManagerScript.gameOver != true)
        {
            speed = hit.collider.gameObject.GetComponentInParent<ObstacleMovement>().speed;
            //Debug.Log("BEEP BEEP!");
        }
        else if (Physics.Raycast(origin, direction, out hit, rayCastLength, vehicleMask) && gameManagerScript.gameOver == true)//If game over is true then faster cars should slow down to the speed of the car in front of it to prevent crashing
        {
            hit.collider.gameObject.GetComponentInParent<ObstacleMovement>().speed = speed;
            //hit.collider.gameObject.GetComponentInParent<ObstacleMovement>().wheelSpinSpeed = 0;
            
        }
        
        

       
    }

    


    private void WheelSpin()
    {
        for (int i = 0; i < 4; i++)
        {
            wheelList[i].transform.Rotate(Vector3.right * wheelSpinSpeed * Time.deltaTime);
        }
    } //This makes the car wheels spin



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("DestroyVehicleTrigger"))
        {
            Destroy(gameObject);
        }
    }

}
