using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Very Important because this means the system will be able to store the data in a file
[System.Serializable]
public class PlayerData
{
    public string playerName;
    public string characterType;
    public int characterNumber;
    public float[] position;
    public int level;
    public int jailTime;
    public int networth;
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


    //Take data from player script and store inside these variables
    public PlayerData(Player player)
    {
        playerName = player.playerName;
        characterType = player.characterType;
        characterNumber = player.characterNumber;
        level = player.level;
        networth = player.networth;
        jailTime = player.jailTime;

        currentTotalCoinCount = player.currentTotalCoinCount;
        currentTotalSodaCount = player.currentTotalSodaCount;
        balloonUpgradeLevel = player.balloonUpgradeLevel;
        tankUpgradeLevel = player.tankUpgradeLevel;
        planeUpgradeLevel = player.planeUpgradeLevel;
        enduranceUpgradeLevel = player.enduranceUpgradeLevel;


        

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }







}
