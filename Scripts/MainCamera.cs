using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCamera : MonoBehaviour
{
    public GameObject player;
    //public GameObject elevatedPlatform;
    public GameObject vehicleGameObject;
    public bool inverseControls;

    public Vector3 cameraOffset;

    public float smoothSpeed = 10.0f;

    [HideInInspector]
    public Quaternion defaultCameraRotation = Quaternion.Euler(16.5f, -90, 0);
    private Quaternion cameraRotateUp = Quaternion.Euler(3, -90, 0);
    private Quaternion cameraRotateDown = Quaternion.Euler(17, -90, 0);
    [HideInInspector]
    public float cameraRotateSpeed = 7.0f;
    
    private GameManager gameManagerScript;
    public PlayerController playerControllerScript;

    public bool elevation;
    //private bool decline;
    private bool slowCameraTransition;
    //private float clock = 0.0f;




    private void Start()
    {
        
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
    }

    void Update()
    {
        if (gameManagerScript.gameOver == true)
        {
            //transform.LookAt(player.transform); //Change later for smooth camera transition
            //StartCoroutine("CameraDeathEffect");
        }
        

        


    }

    IEnumerator CameraSmoothStart()
    {
        while(smoothSpeed < 10)
        {
            smoothSpeed += 2f;
            yield return new WaitForSeconds(0.5f);
        }
    }


    void LateUpdate()
    {

        if (gameManagerScript.gameOver != true && gameManagerScript.mainMenu == false && slowCameraTransition ==false)
        {
            Vector3 desiredPosition = player.transform.position + cameraOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed); //The lower the smooth speed the slower the camera

            transform.position = smoothedPosition;
            
        }
        


        if (elevation != true  && gameManagerScript.gameOver != true && gameManagerScript.mainMenu == false) //deleted && decline != true
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultCameraRotation, Time.deltaTime * cameraRotateSpeed);

        }


        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inverseControls = true;
            cameraOffset = new Vector3(-25, 8, 0);
            transform.LookAt(vehicleGameObject.transform);
            defaultCameraRotation = Quaternion.Euler(10, 90, 0);
        }*/


        //Vector3 vehicleOffset = new Vector3(player.transform.position.x, vehicleGameObject.transform.position.y, player.transform.position.z);

        //create code here that makes the vehicle game object follow the player 
        //vehicleGameObject.transform.position = (Vector3.MoveTowards(vehicleGameObject.transform.position, vehicleOffset, 0.97f));
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("ElevatedPlatform")) //I can add another else if statement for when the player goes down a slope too  (This portion checks to see if player is on an elevated slope)
        {
            elevation = true;
            transform.rotation = Quaternion.Slerp(transform.rotation, cameraRotateUp, Time.deltaTime * cameraRotateSpeed);
            cameraOffset = new Vector3(16.0f, 9.3f, 0f);
            //Debug.Log("ON ELEVATED PLATFORM");
        }
        else if (other.gameObject.CompareTag("DeclinePlatform"))
        {
            //decline = true;
            transform.rotation = Quaternion.Slerp(transform.rotation, cameraRotateDown, Time.deltaTime * cameraRotateSpeed);


        }


    }

    

    private void OnTriggerExit(Collider other) //Detects when player is no longer on an elevated slope
    {
        elevation = false;
        //decline = false;
        cameraOffset = new Vector3(19.3f, 13.8f, 0.0f);
    }


}
