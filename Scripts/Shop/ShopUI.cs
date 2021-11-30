using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Player playerScript;
    public GameManager gameManagerScript;

    [Header("Balloon Card UI")]
    public Slider balloonSlider;
    public Image balloonSliderFill;
    public Text balloonLevelTxt;
    public Text balloonCostTxt;
    public GameObject balloonMax;
    public GameObject balloonCostBox;

    [Header("Tank Card UI")]
    public Slider tankSlider;
    public Image tankSliderFill;
    public Text tankLevelTxt;
    public Text tankCostTxt;
    public GameObject tankMax;
    public GameObject tankCostBox;


    [Header("Plane Card UI")]
    public Slider planeSlider;
    public Image planeSliderFill;
    public Text planeLevelText;
    public Text planeCostTxt;
    public GameObject planeMax;
    public GameObject planeCostBox;






    void Start()
    {
        /*  Use to test how Constructors work
        Powerup myPowerup = new Powerup();
        Balloon balloonPowerup = new Balloon();
        Tank tankPowerup = new Tank();
        Plane planePowerup = new Plane();*/
    }

    public void UpgradeBalloonBtn()
    {
        if (playerScript.balloonUpgradeLevel < 10 && playerScript.currentTotalCoinCount > 250 * playerScript.balloonUpgradeLevel)
        {
            playerScript.balloonUpgradeLevel++;
            Balloon balloonPowerup = new Balloon(playerScript.balloonUpgradeLevel, 250 * playerScript.balloonUpgradeLevel, balloonSlider, balloonLevelTxt, balloonCostTxt);
            playerScript.currentTotalCoinCount -= 250 * playerScript.balloonUpgradeLevel;
            gameManagerScript.UpdatePlayerUI(playerScript);
            playerScript.SavePlayer();
        }
        
        //Max level is 10
        if(playerScript.balloonUpgradeLevel == 10)
        {
            balloonCostBox.SetActive(false);
            balloonMax.SetActive(true);
            balloonLevelTxt.text = playerScript.balloonUpgradeLevel +"";
            balloonSlider.value = balloonSlider.maxValue;
            balloonSliderFill.color = Color.green;
        }
    }

    public void UpgradeTankBtn()
    {
        if (playerScript.tankUpgradeLevel < 10 && playerScript.currentTotalCoinCount > 250 * playerScript.tankUpgradeLevel)
        {
            playerScript.tankUpgradeLevel++;
            Tank tankPowerup = new Tank(playerScript.tankUpgradeLevel, 250 * playerScript.tankUpgradeLevel, tankSlider, tankLevelTxt, tankCostTxt);
            playerScript.currentTotalCoinCount -= 250 * playerScript.tankUpgradeLevel;
            gameManagerScript.UpdatePlayerUI(playerScript);
            playerScript.SavePlayer();
        }
        
        if(playerScript.tankUpgradeLevel == 10)
        {
            tankCostBox.SetActive(false);
            tankMax.SetActive(true);
            tankLevelTxt.text = playerScript.tankUpgradeLevel + "";
            tankSlider.value = tankSlider.maxValue;
            tankSliderFill.color = Color.green;
            
        }
    }


    public void UpgradePlaneBtn()
    {
        if (playerScript.planeUpgradeLevel < 10 && playerScript.currentTotalCoinCount > 250 * playerScript.planeUpgradeLevel)
        {
            playerScript.planeUpgradeLevel++;
            Plane planePowerup = new Plane(playerScript.planeUpgradeLevel, 250 * playerScript.planeUpgradeLevel, planeSlider, planeLevelText, planeCostTxt);
            playerScript.currentTotalCoinCount -= 250 * playerScript.planeUpgradeLevel;
            gameManagerScript.UpdatePlayerUI(playerScript);
            playerScript.SavePlayer();
        }
        
        if(playerScript.planeUpgradeLevel == 10)
        {
            planeCostBox.SetActive(false);
            planeMax.SetActive(true);
            planeLevelText.text = playerScript.planeUpgradeLevel + "";
            planeSlider.value = planeSlider.maxValue;
            planeSliderFill.color = Color.green;
        }
    }

    public void Cheat()
    {
        playerScript.currentTotalCoinCount = 999999;
        playerScript.currentTotalSodaCount = 9999;
        gameManagerScript.UpdatePlayerUI(playerScript);




    }


}
