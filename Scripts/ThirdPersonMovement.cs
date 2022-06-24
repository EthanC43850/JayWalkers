using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.UI;

public class ThirdPersonMovement : MonoBehaviour
{
    public GameObject freeLookScript;
    public GameManager gameManagerScript;
    public GameObject cam;
    public GameObject lookPos;

    [Header("End Game objects")]
    public Text hintsUI_text;
    public bool finishedGame_b;
    public GameObject oldbossColliderObject;
    public GameObject bossFinalConversationCollider;
    public GameObject endGamePlaneCollider;
    public GameObject oldPlaneCollider;

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
    public GameObject shopPlaneCard;

    [Header("Firestation")]
    public LayerMask fireStationEntrance;
    public LayerMask fireStationExit;
    public LayerMask brokenPlane;
    public LayerMask bossMan;
    public LayerMask bossManLastConversation;
    public LayerMask endGameCutscene;
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
        Debug.Log("LOCKED");
        //playerAnim.SetBool("Static_b", true);
    }

    void Update()
    {

        CheckEndGame();

        OpenSettings();

        if (gameManagerScript.isMenuOpen == true)
        {
            freeLookScript.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {

            freeLookScript.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        #region Third Person movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction;
        if (gameManagerScript.isMenuOpen == false)
        {
            direction = new Vector3(horizontal, 0f, vertical).normalized;

        }
        else
        {
            direction = Vector3.zero;
        }



        if (direction.magnitude > 0.1 && gameManagerScript.shopOpen == false && gameManagerScript.dialogueOpen == false)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnim.SetBool("Running_b", true);
                playerAnim.SetBool("Walk_b", false);

                speed = 20;
            }
            else
            {
                playerAnim.SetBool("Walk_b", true);
                playerAnim.SetBool("Running_b", false);

                speed = 10;

            }

            float lookDirection = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookDirection, ref turnSmoothVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, lookDirection, 0f) * Vector3.forward;
            controller.SimpleMove(moveDirection.normalized * speed );
        }
        else if (direction.magnitude <= 0.1)
        {
            playerAnim.SetBool("Running_b", false);
            playerAnim.SetBool("Walk_b", false);
        }
        #endregion


        //Allow player to open and close ID Card
        if (Input.GetKeyDown(KeyCode.C) && displayCard && gameManagerScript.insideTown)
        {
            playerID.SetActive(false);
            displayCard = false;
            gameManagerScript.isMenuOpen = false;
            freeLookScript.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if(Input.GetKeyDown(KeyCode.C) && displayCard == false && gameManagerScript.insideTown)
        {
            playerID.SetActive(true);
            displayCard = true;
            gameManagerScript.isMenuOpen = true;
            freeLookScript.SetActive(true);
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

            if (Input.GetKeyDown(KeyCode.E)&& gameManagerScript.shopOpen == false)
            {
                //Must call functions from gamemanager because there are 4 different characters
                gameManagerScript.shopOpen = true;  
                gameManagerScript.dialogueOpen = true;  //Makes sure shop only opens when dialogue is over
                gameManagerScript.shopVirtualCam.Priority = 25;
                gameManagerScript.shopDialogueTrigger.TriggerDialogue();

                //Disable Normal Player Controls
                gameManagerScript.isMenuOpen = true;
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
                gameManagerScript.isMenuOpen = false;
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
                gameManagerScript.cameraTransitionTimeline.Play();
                StartCoroutine("TeleportUpstairs");
            }
        }
        else if(Physics.Raycast(lookPos.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f, fireStationExit))
        {
            interactUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameManagerScript.cameraTransitionTimeline.Play();
                StartCoroutine("TeleportDownstairs");
            }
        }
        else if(Physics.Raycast(lookPos.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f, brokenPlane))
        {
            interactUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                planeSmokeFx.Stop();
                shopPlaneCard.SetActive(true);
                gameManagerScript.titleScreenScript.playerScript.planeUnlocked_b = true;
            }
        }
        else if (Physics.Raycast(lookPos.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f, endGameCutscene))
        {
            interactUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                hintsUI_text.text = "Unlocked New plane powerup in the shop!";

                gameManagerScript.playerInfoUI.SetActive(false);
                interactUI.SetActive(false);
                // Unlock plane
                planeSmokeFx.Stop();
                shopPlaneCard.SetActive(true);
                gameManagerScript.titleScreenScript.playerScript.planeUnlocked_b = true;

                oldPlaneCollider.SetActive(false);
                endGamePlaneCollider.SetActive(false);

                StartCoroutine(EndGameTimeLine());
                

            }
        }
        else
        {
            interactUI.SetActive(false);
        }

       


        if (Physics.Raycast(lookPos.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f, bossMan))
        {
            interactUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                //Must call functions from gamemanager because there are 4 different characters
                gameManagerScript.dialogueOpen = true;
                gameManagerScript.bossManVirtualCamera.Priority = 25;
                gameManagerScript.bossManDialogueTrigger.TriggerDialogue();


                //Disable Normal Player Controls
                gameManagerScript.isMenuOpen = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                freeLookScript.SetActive(false);
            }

            if (gameManagerScript.dialogueOpen == false)
            {
                gameManagerScript.isMenuOpen = false;
                freeLookScript.SetActive(true);
                gameManagerScript.bossManVirtualCamera.Priority = 5;
            }
        }


        if (Physics.Raycast(lookPos.transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f, bossManLastConversation))
        {
            interactUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                
                //Must call functions from gamemanager because there are 4 different characters
                gameManagerScript.dialogueOpen = true;
                gameManagerScript.bossManVirtualCamera.Priority = 25;
                gameManagerScript.bossManLastConversationTrigger.TriggerDialogue();


                //Disable Normal Player Controls
                gameManagerScript.isMenuOpen = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                freeLookScript.SetActive(false);
            }

            if (gameManagerScript.dialogueOpen == false)
            {
                gameManagerScript.isMenuOpen = false;
                freeLookScript.SetActive(true);
                gameManagerScript.bossManVirtualCamera.Priority = 5;
            }
        }


        
        



    }

    IEnumerator EndGameTimeLine()
    {

        gameManagerScript.cameraTransitionTimeline.Play();


        yield return new WaitForSeconds(1.5f);  //Amount of time it takes for camera to go complete black

        gameManagerScript.endgameCutsceneTimeline.Play();


    }


    public void CheckEndGame()
    {
        if(gameManagerScript.playerControllerScript.soundTrack1_Progress >= gameManagerScript.titleScreenScript.shopUIScript.soundTrack1_completionValue && finishedGame_b == false)
        {
            if(gameManagerScript.playerControllerScript.soundTrack2_Progress >= gameManagerScript.titleScreenScript.shopUIScript.soundTrack2_completionValue)
            {
                if(gameManagerScript.playerControllerScript.soundTrack3_Progress >= gameManagerScript.titleScreenScript.shopUIScript.soundTrack3_completionValue)
                {
                    oldbossColliderObject.SetActive(false);
                    bossFinalConversationCollider.SetActive(true);
                    oldPlaneCollider.SetActive(false);
                    endGamePlaneCollider.SetActive(true);
                    finishedGame_b = true;
                    //gameManagerScript.playerInfoUI.SetActive(false);


                }



            }



        }


    }



    IEnumerator TeleportUpstairs()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.transform.position = upstairs.transform.position;

    }

    IEnumerator TeleportDownstairs()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.transform.position = downStairs.transform.position;

    }

    public void CloseSettings()
    {
        //Disable Normal Player Controls
        gameManagerScript.isMenuOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameManagerScript.titleScreenScript.settingsMenu.SetActive(false);
    }

    public void OpenSettings()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameManagerScript.shopOpen == false && gameManagerScript.insideTown == true && gameManagerScript.player != null)
        {
            gameManagerScript.save_text.text = "Save Game";
            if (gameManagerScript.titleScreenScript.settingsMenu.activeInHierarchy)
            {
                //Enable Normal Player Controls
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                gameManagerScript.isMenuOpen = false;

                gameManagerScript.titleScreenScript.settingsMenu.SetActive(false);
                gameManagerScript.playerControllerScript.playerScript.SavePlayer(); // Game saves whenever escape is pressed

            }
            else
            {
                //Disable Normal Player Controls
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                gameManagerScript.isMenuOpen = true;

                gameManagerScript.titleScreenScript.settingsMenu.SetActive(true);

            }

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




