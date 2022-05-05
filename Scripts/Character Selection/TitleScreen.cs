using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Cinemachine;

public class TitleScreen : MonoBehaviour
{
    #region Outside Scripts
    [HideInInspector]
    public GameManager gameManagerScript;
    [HideInInspector]
    public TimelineController timeLineControllerScript;
    [HideInInspector]
    public DialogueTrigger dialogueTriggerScript;
    [HideInInspector]
    public DialogueManager dialogueManagerScript;
    [HideInInspector]
    public CinemachineFreeLook thirdPersonCameraScript;

    public ShopUI shopUIScript;

    public Highlight highLightScript;

    public Player playerScript;

    private PlayerController playerControllerScript;

    public MainCamera gameplayCameraScript;

    public PowerUpMeter balloonPowerMeter;
    public PowerUpMeter tankPowerMeter;
    public PowerUpMeter planePowerMeter;

    public SkyBox skyBoxScript;
    #endregion

    #region Dialogue Trigger Objects
    [HideInInspector]
    public List<DialogueTrigger> dialogueTriggers;
    [HideInInspector]
    public GameObject beginDialogue;        //This is used as a flag and is activated in timeline
    #endregion

    #region Character Select GameObjects
    [HideInInspector]
    public GameObject dialogueBoxGameObject;

    [HideInInspector]
    public Text thugTitleText;

    [HideInInspector]
    public Text characterDescriptionText;

    [HideInInspector]
    public GameObject characterDescriptionBox;

    [HideInInspector]
    public Animator characterDescriptionBoxAnimator;

    [HideInInspector]
    [SerializeField] GameObject characterSelectUI;

    [HideInInspector]
    public Animator characterSelectUIAnimator;

    public GameObject playerCardUI;

    public GameObject playerCardButtons;


    //public Button acceptCharacterBtn;
    //public Button declineCharacterBtn;

    //public Animator enterNameBoxAnimator;
    #endregion

    public PlayableDirector titleScreenPlayableDirector;
    public GameObject cutSceneTools;

    public bool characterSelectActive;
    public bool playerSelected;

    public int currentCharacterNumber;
    public int characterNumber = -1;
    private string nameInput;

    public Text nameUI;
    public Text cardNameUI;
    public Text characterTypeUI;

    public List<GameObject> characterList;

    public GameObject skipCutsceneBtn;
    [SerializeField] GameObject hintsPanel;

    //public Button loadBtn;
    public Button saveBtn;

    //public GameObject loadButton;
    //public GameObject saveButton;

    private float timeLeft = 0.5f;
    public int count = 0;

    [Header("Leaderboard Functionality")]
    public GameObject leaderBoardCanvas;
    public GameObject settingsMenu;

    void Start()
    {
        //gameManagerScript.titleScreenUI.SetActive(true);

        characterSelectUI.SetActive(true);
        dialogueBoxGameObject.SetActive(true);
        characterDescriptionBox.SetActive(true);
        hintsPanel.SetActive(true);
        
    }

    void Update()
    {

        OpenSettings();

        //If beginDialgue (The gameobject acting as my trigger in the timelines) activates, start dialogue
        if (beginDialogue.activeSelf && timeLeft <= 0.0f)
        {
            if (count < 4)
            {
                dialogueTriggerScript.TriggerSpecificDialogue(dialogueTriggers[count].dialogue);
            }
            count++;

            //Prevents the dialogueTrigger function to be called through multiple frames from timeline. 
            //How this works is that the 'beginDialogue' gameobject is activated for only a frame at the start of the next playable director.
            timeLeft = 0.5f;
        }
        if(timeLeft > 0.0f)
        {
            timeLeft -= Time.deltaTime;
        }


        //Start Character Selection
        //CREATE A PANEL THAT GIVES THE PLAYER THE DIRECTIONS TO "Hover your mouse over a character to select one"
        if(count == 5)
        {

            skipCutsceneBtn.SetActive(false);
            for (int i = 1; i < 5; i++)
            {
                timeLineControllerScript.playableDirectors[i].gameObject.SetActive(false);  //disable all time lines completely to prevent future camera interference 
            }                                                                               //(This only happened because scene count went up after titlescreen cutscene was skipped)
            characterSelectUIAnimator.SetBool("Is_Open", true);
            characterSelectActive = true;
            count++;
        }


    }

    public void StartGame()
    {

        timeLineControllerScript.PlayFromDirectors(0);
        dialogueManagerScript.IncrementSceneNumber();

        skipCutsceneBtn.SetActive(true);
        gameManagerScript.titleScreen = false;
        gameManagerScript.titleScreenUI.SetActive(false);
        titleScreenPlayableDirector.Stop();
    }


    public void SkipCutscene()
    {
        count = 5;
        
    }


    //When player finishes typing in text box, assign name to local variable
    public void AssignPlayerName(string name)
    {
        nameInput = name;
        nameUI.text = nameInput;
        cardNameUI.text = nameInput;
        AuthManager.userName = name;
        Debug.Log("Player name is " + name);
    }



    /*public void SelectCharacter()
    {
        characterDescriptionBoxAnimator.SetBool("Is_Open", false);
        count++;
    }*/


    //Give control to the character that the player selected
    public void ConfirmCharacterSelection()
    {

        characterSelectActive = false;
        characterNumber = currentCharacterNumber;


        //Enable open-world UI
        gameManagerScript.playerInfoUI.SetActive(true);
        gameManagerScript.playerInfoUI.GetComponent<Animator>().SetBool("Open_b", true);
        characterDescriptionBoxAnimator.SetBool("Is_Open", false);
        characterSelectUIAnimator.SetBool("Is_Open", false);
        playerCardButtons.SetActive(false);
        playerCardUI.SetActive(false);

        //Assign player controller script
        playerControllerScript = characterList[characterNumber].GetComponent<PlayerController>();
        gameManagerScript.playerControllerScript = playerControllerScript;

        gameplayCameraScript.playerControllerScript = playerControllerScript;                      
        gameplayCameraScript.player = characterList[characterNumber];                               //Tell gameplay camera which character to follow
        gameManagerScript.player = characterList[characterNumber];                                  //Tell GameManager which character was selected
        skyBoxScript.player = characterList[characterNumber];                                       //Tell the skybox which character to follow on start of run

        //Each power meter needs access to which playercontroller script is being used
        balloonPowerMeter.playerControllerScript = playerControllerScript;
        tankPowerMeter.playerControllerScript = playerControllerScript;
        planePowerMeter.playerControllerScript = playerControllerScript;


        //Create and assign new player data
        characterList[characterNumber].AddComponent<Player>();
        playerScript = characterList[characterNumber].GetComponent<Player>();
        playerControllerScript.playerScript = playerScript;
        shopUIScript.playerScript = playerScript;
        playerScript.playerName = nameInput;
        playerScript.characterNumber = characterNumber;
        playerScript.characterType = characterTypeUI.text;     //characterClassCardUI is initialized when a character is clicked on
        //Initialize levels to 1
        playerScript.balloonUpgradeLevel = 1;
        playerScript.tankUpgradeLevel = 1;
        playerScript.planeUpgradeLevel = 1;

        saveBtn.onClick.AddListener(playerScript.SavePlayer);   //Saving will be turned on

        //Make sure the ground marker and the box collider trigger is disabled
        highLightScript = characterList[characterNumber].GetComponent<Highlight>();
        highLightScript.highLightScriptBoxCollider.enabled = !enabled;
        highLightScript.groundMarker.SetActive(false);


        timeLineControllerScript.playableDirectors[0].Stop();   //All characters can stop looking left

        
        // Activate third person controls and set camera priority on selected character
        characterList[characterNumber].GetComponent<CharacterController>().enabled = enabled;
        characterList[characterNumber].GetComponent<ThirdPersonMovement>().enabled = enabled;
        thirdPersonCameraScript.Follow = characterList[characterNumber].transform;
        thirdPersonCameraScript.LookAt = characterList[characterNumber].transform;
        thirdPersonCameraScript.Priority = 20;

        count++;
    }

    public void DeclineCharacterSelection()
    {
        characterDescriptionBoxAnimator.SetBool("Is_Open", true);
        playerCardUI.SetActive(false);
    }

    public void DisplayLeaderboard()
    {
        
        leaderBoardCanvas.SetActive(true);

    }

    public void CloseLeaderBoard()
    {
        leaderBoardCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CloseSettings()
    {
        gameManagerScript.isMenuOpen = false;
        settingsMenu.SetActive(false);

    }

    public void OpenSettings()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenu.activeInHierarchy)
            {
                //Disable Normal Player Controls
                gameManagerScript.isMenuOpen = false;
                settingsMenu.SetActive(false);

            }
            else
            {
                gameManagerScript.isMenuOpen = true;
                settingsMenu.SetActive(true);

            }

        }

    }

    public void TitleScreenLoadData()
    {
        cutSceneTools.SetActive(false);

        //Load previously saved data
        PlayerData data = SaveSystem.LoadPlayer();
        currentCharacterNumber = data.characterNumber;
        characterNumber = data.characterNumber;
        characterList[characterNumber].AddComponent<Player>();
        playerScript = characterList[characterNumber].GetComponent<Player>();

        //Assign which character the gameplay camera needs to follow
        gameplayCameraScript.player = characterList[characterNumber];
        gameManagerScript.player = characterList[characterNumber];
        skyBoxScript.player = characterList[characterNumber];                                       //Tell the skybox which character to follow on start of run

        //Make sure the ground marker and the second box collider (which is the trigger) is disabled
        highLightScript = characterList[characterNumber].GetComponent<Highlight>();
        highLightScript.highLightScriptBoxCollider.enabled = !enabled;

        //Assign which ThirdPersonPlayer script and playercontroller script is being used
        playerControllerScript = characterList[characterNumber].GetComponent<PlayerController>();
        gameplayCameraScript.playerControllerScript = playerControllerScript;
        gameManagerScript.playerControllerScript = playerControllerScript;

        balloonPowerMeter.playerControllerScript = playerControllerScript;
        tankPowerMeter.playerControllerScript = playerControllerScript;
        planePowerMeter.playerControllerScript = playerControllerScript;

        
        characterList[characterNumber].GetComponent<CharacterController>().enabled = enabled;
        characterList[characterNumber].GetComponent<ThirdPersonMovement>().enabled = enabled;

        thirdPersonCameraScript.Follow = characterList[characterNumber].transform;
        thirdPersonCameraScript.LookAt = characterList[characterNumber].transform;
        thirdPersonCameraScript.Priority = 20;

        //Activate open-world UI
        gameManagerScript.playerInfoUI.SetActive(true);
        gameManagerScript.playerInfoUI.GetComponent<Animator>().SetBool("Open_b", true);
        gameManagerScript.titleScreen = false;
        gameManagerScript.titleScreenUI.SetActive(false);
        titleScreenPlayableDirector.Stop();
        count = 7;  //Increment count to skip player select

        //Give the player controller and shop scripts the data stored inside the player
        playerControllerScript.playerScript = playerScript;



        //Update the shop UI after loading player because I can't move shopUIScript into the player load script (Unless I pass a parameter)
        shopUIScript.playerScript = playerScript;
        Balloon balloonPowerup = new Balloon(data.balloonUpgradeLevel, 1000 * data.balloonUpgradeLevel, shopUIScript.balloonSlider, shopUIScript.balloonLevelTxt, shopUIScript.balloonCostTxt);
        Tank tankPowerup = new Tank(data.tankUpgradeLevel, 1250 * data.tankUpgradeLevel, shopUIScript.tankSlider, shopUIScript.tankLevelTxt, shopUIScript.tankCostTxt);
        Plane planePowerup = new Plane(data.planeUpgradeLevel, 1500 * data.tankUpgradeLevel, shopUIScript.planeSlider, shopUIScript.planeLevelText, shopUIScript.planeCostTxt);

        if (data.balloonUpgradeLevel == 10)
        {
            shopUIScript.balloonCostBox.SetActive(false);
            shopUIScript.balloonMax.SetActive(true);
            shopUIScript.balloonLevelTxt.text = data.balloonUpgradeLevel + "";
            shopUIScript.balloonSlider.value = shopUIScript.balloonSlider.maxValue;
            shopUIScript.balloonSliderFill.color = Color.green;
        }

        if (data.tankUpgradeLevel == 10)
        {
            shopUIScript.tankCostBox.SetActive(false);
            shopUIScript.tankMax.SetActive(true);
            shopUIScript.tankLevelTxt.text = data.tankUpgradeLevel + "";
            shopUIScript.tankSlider.value = shopUIScript.tankSlider.maxValue;
            shopUIScript.tankSliderFill.color = Color.green;

        }

        if (data.planeUpgradeLevel == 10)
        {
            shopUIScript.planeCostBox.SetActive(false);
            shopUIScript.planeMax.SetActive(true);
            shopUIScript.planeLevelText.text = data.planeUpgradeLevel + "";
            shopUIScript.planeSlider.value = shopUIScript.planeSlider.maxValue;
            shopUIScript.planeSliderFill.color = Color.green;
        }


        saveBtn.onClick.AddListener(playerScript.SavePlayer);
        
        playerScript.LoadPlayer();

        


        gameManagerScript.UpdatePlayerUI(playerScript);


    }

}


