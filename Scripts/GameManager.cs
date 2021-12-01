using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Playables;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public bool titleScreen;
    public bool mainMenu;
    public bool insideTown;
    public bool gameStart;
    public bool gameOver;
    public bool gameRestart;

    [SerializeField] CinemachineVirtualCamera gameplayCamera;
    public PlayableDirector cameraTransitionTimeline;

    [Header("Main Menu")]
    public GameObject titleScreenUI;
    public GameObject pressButtonText;

    [Header("Player In-Town UI")]
    public bool dialogueOpen;
    public bool shopOpen;
    public CinemachineVirtualCamera shopVirtualCam;
    public DialogueTrigger shopDialogueTrigger;

    public GameObject playerInfoUI;
    public Text levelUI;
    public Text currentCashAmtUI;
    public Text currentSodaAmtUI;
    public Text currentCashAmtShopUI;
    public Text currentSodaAmtShopUI;


    public GameObject thirdPersonCamera;
    public GameObject player;
    public GameObject returnToTownPosition;
    public PlayableDirector backInTownTimeline;

    [Header("Gameplay UI")]
    public GameObject gameOverUI;
    public Button backToTownButton;
    public TextMeshProUGUI drinkCount;
    public TextMeshProUGUI cashCountText;
    public Animator energyDrinkAnim;
    public Animator dollarSignAnim;
    public CoinMeter coinMeter;
    public GameObject drinkUI;
    public GameObject scoreUI;
    public GameObject cashUI;
    public GameObject coinMeterUI;
    public GameObject multiplierUI;
    public TextMeshProUGUI scoreText;


    [Header("Player Stats")]
    private int score;
    public int totalDrinks;
    public int totalCurrentCash;

    [Header("Outside Scripts")]
    private PlatformSpawner platformSpawnerScript;
    public PlayerController playerControllerScript;
    public MainCamera mainCameraScript;
    public TitleScreen titleScreenScript; //Only used to update player UI when "Return to town" is clicked, and add total jailtime
    

    [Header("Player Card UI")]
    public Text characterTypeCardUI;
    public Text characterNetworthCardUI;
    public Text characterLevelCardUI;
    public Text characterJailTimeCardUI;





    void Start()
    {
        titleScreen = true;
        titleScreenUI.SetActive(true);
        platformSpawnerScript = GameObject.Find("Platform Spawner").GetComponent<PlatformSpawner>();
    }




    void Update()
    {


        if (Input.GetKeyDown(KeyCode.P))
        {
            playerInfoUI.GetComponent<Animator>().SetBool("Open_b", false);

            gameplayCamera.Priority = 20;
            mainCameraScript.smoothSpeed = 1;
            mainCameraScript.StartCoroutine("CameraSmoothStart");
            mainCameraScript.player.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            playerControllerScript.EnableRunningState();
            //platformSpawnerScript.SpawnInitialPlatforms();

            insideTown = false;
            mainMenu = false;
            gameOver = false;
            gameStart = true;
            thirdPersonCamera.SetActive(false);

            titleScreenScript.playerScript.jailTime++;


        }



        if (gameStart == true)
        {

            drinkUI.SetActive(true);
            scoreUI.SetActive(true);
            cashUI.SetActive(true);
            coinMeterUI.SetActive(true);
            multiplierUI.SetActive(true);
        }

        if (gameOver)
        {
            gameOverUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            backToTownButton.gameObject.SetActive(true);

        }
        else if (!gameOver && !insideTown)
        {
            UpdateScore(1);
            gameOverUI.SetActive(false);
        }





    }


    public void RestartGame()
    {
        //Reset player stats
        score = 0;
        totalCurrentCash = 0;
        UpdateCashCount(0);
        totalDrinks = 0;
        UpdateDrinkCount(0);

        titleScreenScript.playerScript.jailTime++;


        if (insideTown != true)
        {
            mainCameraScript.transform.rotation = Quaternion.Slerp(mainCameraScript.transform.rotation, mainCameraScript.defaultCameraRotation, Time.deltaTime * mainCameraScript.cameraRotateSpeed);
            playerControllerScript.transform.rotation = Quaternion.Euler(0f, -90f, 0f);                //Make sure that plane power up did not tilt the player
            playerControllerScript.speed = playerControllerScript.startSpeed;
            gameOver = false;
            playerControllerScript.transform.position = new Vector3(playerControllerScript.playerStartPos.transform.position.x, playerControllerScript.playerStartPos.transform.position.y, playerControllerScript.playerStartPos.transform.position.z);
        }


        var platformClones = GameObject.FindGameObjectsWithTag("Platform");
        var elevatedPlatformClones = GameObject.FindGameObjectsWithTag("ElevatedPlatform");
        var obstacleClones = GameObject.FindGameObjectsWithTag("Obstacle");
        var cashClones = GameObject.FindGameObjectsWithTag("Money");
        var van_TreePlanks_Clones = GameObject.FindGameObjectsWithTag("Van_TreePlanks");
        foreach (var platformClone in platformClones)
        {
            Destroy(platformClone);
        }
        foreach (var obstacleClone in obstacleClones)
        {
            Destroy(obstacleClone);
        }
        foreach (var elevatedPlatformClone in elevatedPlatformClones)
        {
            Destroy(elevatedPlatformClone);
        }
        foreach (var cashClone in cashClones)
        {
            Destroy(cashClone);
        }
        foreach (var van_TreePlank in van_TreePlanks_Clones)
        {
            Destroy(van_TreePlank);
        }

        platformSpawnerScript.SpawnInitialPlatforms();
    }


    IEnumerator StartScoreCount()
    {
        yield return new WaitForSeconds(0.2f);
    }

    //Update in-game UI
    private void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "" + score;
    }

    public void UpdateDrinkCount(int addDrink)
    {
        totalDrinks += addDrink;
        drinkCount.text = totalDrinks + " /20";
        energyDrinkAnim.Play("Drink_Collect", -1, 0f);


    }

    public void UpdateCashCount(int addCash)
    {
        dollarSignAnim.Play("Cash_Collect", -1, 0f); 
        coinMeter.SetMeter(addCash);    //This function also adds cash to the totalcurrentcash count variable
        cashCountText.text = "" + totalCurrentCash;

    }

    

    public void BackToTown()
    {

        UpdatePlayerUI(titleScreenScript.playerScript);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        

        gameOver = false;
        gameStart = false;
        insideTown = true;

        //Disable In-Game UI
        gameOverUI.SetActive(false);
        drinkUI.SetActive(false);
        scoreUI.SetActive(false);
        cashUI.SetActive(false);
        coinMeterUI.SetActive(false);
        multiplierUI.SetActive(false);
        backToTownButton.gameObject.SetActive(false);


        StartCoroutine("PoliceStationTimeline");



    }


    IEnumerator PoliceStationTimeline()
    {

        cameraTransitionTimeline.Play();
        
        yield return new WaitForSeconds(1.5f);  //Amount of time it takes for camera to go complete black

        RestartGame();  //Reset the map after camera goes completely dark


        //Reposition player
        backInTownTimeline.Play();
        thirdPersonCamera.SetActive(true);

        playerControllerScript.transform.position = new Vector3(returnToTownPosition.transform.position.x, returnToTownPosition.transform.position.y, returnToTownPosition.transform.position.z);
        playerControllerScript.transform.rotation = Quaternion.Euler(returnToTownPosition.transform.rotation.x, returnToTownPosition.transform.rotation.y, returnToTownPosition.transform.rotation.z);

        playerControllerScript.playerAnim.SetBool("Jump_b", false);
        playerControllerScript.playerRb.isKinematic = true;     //Rigid body will no longer interfere with third person controller
        playerControllerScript.playerAnim.SetBool("Death_b", false);

        
        yield return new WaitForSeconds(4.4f);  //After timeline has ended
        playerInfoUI.GetComponent<Animator>().SetBool("Open_b", true);



        //Enable open-world controls
        player.GetComponent<CharacterController>().enabled = enabled;
        player.GetComponent<ThirdPersonMovement>().enabled = enabled;


    }


    //Update Open-world Player UI
    public void UpdatePlayerUI(Player playerScript)
    {
        characterLevelCardUI.text = playerScript.level.ToString();
        characterJailTimeCardUI.text = playerScript.jailTime.ToString();

        //Player UI
        levelUI.text = playerScript.level.ToString();
        currentCashAmtUI.text = playerScript.currentTotalCoinCount.ToString();
        currentSodaAmtUI.text = playerScript.currentTotalSodaCount.ToString();

        //Shop UI
        currentCashAmtShopUI.text = playerScript.currentTotalCoinCount.ToString();
        currentSodaAmtShopUI.text = playerScript.currentTotalSodaCount.ToString();

        //State ID UI
        characterTypeCardUI.text = playerScript.characterType;
        characterNetworthCardUI.text = "$ " + playerScript.networth.ToString();
        characterTypeCardUI.text = playerScript.characterType;
        characterLevelCardUI.text = playerScript.level.ToString();
        characterJailTimeCardUI.text = playerScript.jailTime.ToString();



    }

    public void CloseShop()
    {
        shopOpen = false;
        
    }
}
