using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{


    public string playerName;
    public string characterType;
    public int characterNumber;
    public float[] position;
    public int level;
    public int networth;
    public int jailTime;

    public int currentTotalCoinCount;
    //Capable of buying sodas or planes before a run
    public int currentTotalSodaCount;
    public int currentTotalPlaneCount;
    public int currentTotalTankCount;

    

    //Powerup upgrades
    public int planeUpgradeLevel;
    public int tankUpgradeLevel;
    public int balloonUpgradeLevel;

    public int enduranceUpgradeLevel;


    public GameManager gameManagerScript;
    public TitleScreen titleScreenScript;
    /*
    public Text nameUI;
    public Text levelUI;
    public Text coinCountUI;

    public Text cardNameUI;
    public Text cardLevelUI;
    #endregion*/



    public void Start()
    {

        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        titleScreenScript = GameObject.Find("TitleScreen").GetComponent<TitleScreen>();
    }

    //Create update function that saves player data every 10 seconds or whenever something is bought, or whenever a run is finished

    public void SavePlayer()
    {
        Debug.Log("Saving character number " + characterNumber);
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        

        playerName = data.playerName;
        characterType = data.characterType;
        characterNumber = data.characterNumber;
        level = data.level;
        currentTotalCoinCount = data.currentTotalCoinCount;
        currentTotalSodaCount = data.currentTotalSodaCount;
        networth = data.networth;
        jailTime = data.jailTime;

        currentTotalCoinCount = data.currentTotalCoinCount;
        balloonUpgradeLevel = data.balloonUpgradeLevel;
        tankUpgradeLevel = data.tankUpgradeLevel;
        planeUpgradeLevel = data.planeUpgradeLevel;
        enduranceUpgradeLevel = data.enduranceUpgradeLevel;

        //Get the character from the titleScreenScript and update the UI
        Debug.Log("Character number to load is " + characterNumber);
        //gameManagerScript.UpdatePlayerUI(titleScreenScript.characterList[characterNumber].GetComponent<Player>());

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;

        


    }

    public void UpdateUserInterface()
    {


    }







}
