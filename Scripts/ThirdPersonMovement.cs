using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;



public class ThirdPersonMovement : MonoBehaviour
{
    public GameObject freeLookScript;
    public GameManager gameManagerScript;
    public GameObject cam;
    public GameObject lookPos;


    public GameObject interactUI;

    [Header("Player Controls")]
    public CharacterController controller;
    public float speed;
    public float smoothTime = 0.1f;
    float turnSmoothVelocity;
    public Animator playerAnim;

    [Header("Shop")]
    public LayerMask storeLayer;
    public GameObject shopUI;
    public DialogueTrigger shopDialogueTrigger;

    [Header("Firestation")]
    public LayerMask fireStationEntrance;
    public LayerMask fireStationExit;
    public LayerMask brokenPlane;
    public GameObject planeCrashTimeline;
    public ParticleSystem planeSmokeFx;
    private bool playedBefore = false;

    [SerializeField] Transform downStairs;
    [SerializeField] Transform upstairs;


    [Header("Player HUD")]

    public GameObject playerID;
    public bool displayCard;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //playerAnim.SetBool("Static_b", true);
    }

    void Update()
    {
        #region Third Person movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;



        if (direction.magnitude > 0.1 && gameManagerScript.shopOpen == false && gameManagerScript.dialogueOpen == false)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnim.SetBool("Running_b", true);
                playerAnim.SetBool("Walk_b", false);

                speed = 1000;
            }
            else
            {
                playerAnim.SetBool("Walk_b", true);
                playerAnim.SetBool("Running_b", false);

                speed = 500;

            }

            float lookDirection = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookDirection, ref turnSmoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, lookDirection, 0f) * Vector3.forward;
            controller.SimpleMove(moveDirection.normalized * speed * Time.deltaTime);
        }
        else if (direction.magnitude <= 0.1)
        {
            playerAnim.SetBool("Running_b", false);
            playerAnim.SetBool("Walk_b", false);
        }
        #endregion


        //Allow player to open and close ID Card
        if (Input.GetKeyDown(KeyCode.C) && displayCard)
        {
            playerID.SetActive(false);
            displayCard = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if(Input.GetKeyDown(KeyCode.C) && !displayCard)
        {
            playerID.SetActive(true);
            displayCard = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        //Check if player is near merchant NPC
        Debug.DrawRay(lookPos.transform.position, transform.TransformDirection(Vector3.forward)* 1.5f, Color.red, 0.1f);
        RaycastHit hit;
        if (Physics.Raycast(lookPos.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f, storeLayer))
        {
            if(gameManagerScript.shopOpen == true)
            {
                interactUI.SetActive(false);
            }
            else
            {
                interactUI.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                //Must call functions from gamemanager because there are 4 different characters
                gameManagerScript.shopOpen = true;  
                gameManagerScript.dialogueOpen = true;  //Makes sure shop only opens when dialogue is over
                gameManagerScript.shopVirtualCam.Priority = 25;
                gameManagerScript.shopDialogueTrigger.TriggerDialogue();

                //Disable Normal Player Controls
                freeLookScript.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                speed = 0;

            }

            if (gameManagerScript.dialogueOpen == false && gameManagerScript.shopOpen == true) //Makes sure shop only opens when dialogue is over
            {

                shopUI.SetActive(true);
                gameManagerScript.playerInfoUI.GetComponent<Animator>().SetBool("Open_b", false);
            }

            if(gameManagerScript.dialogueOpen == false && gameManagerScript.shopOpen == false) //Close shop
            {
                shopUI.SetActive(false);
                freeLookScript.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                gameManagerScript.playerInfoUI.GetComponent<Animator>().SetBool("Open_b", true);
                gameManagerScript.shopVirtualCam.Priority = 5;
            }
        }
        else if(Physics.Raycast(lookPos.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f, fireStationEntrance))
        {
            interactUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameObject.transform.position = upstairs.transform.position;
            }
        }
        else if(Physics.Raycast(lookPos.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f, fireStationExit))
        {
            interactUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameObject.transform.position = downStairs.transform.position;
            }
        }
        else if(Physics.Raycast(lookPos.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f, brokenPlane))
        {
            interactUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                planeSmokeFx.Stop();
            }
        }
        else
        {
            interactUI.SetActive(false);
        }




    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlaneTimelineTrigger") && playedBefore == false)
        {
            planeCrashTimeline.SetActive(true);
            playedBefore = true;
        }
    }





}




