using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Player playerScript;

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
        if (playerScript.balloonUpgradeLevel < 10)
        {
            playerScript.balloonUpgradeLevel++;
            Balloon balloonPowerup = new Balloon(playerScript.balloonUpgradeLevel, 1250 * playerScript.balloonUpgradeLevel, balloonSlider, balloonSliderFill, balloonLevelTxt, balloonCostTxt);
            playerScript.SavePlayer();
        }
        else
        {
            balloonCostBox.SetActive(false);
            balloonMax.SetActive(true);
        }
    }

    public void UpgradeTankBtn()
    {
        if (playerScript.tankUpgradeLevel < 10)
        {
            playerScript.tankUpgradeLevel++;
            Tank tankPowerup = new Tank(playerScript.tankUpgradeLevel, 1250 * playerScript.tankUpgradeLevel, tankSlider, tankSliderFill, tankLevelTxt, tankCostTxt);
            playerScript.SavePlayer();
        }
        else
        {
            tankCostBox.SetActive(false);
            tankMax.SetActive(true);
        }
    }


    public void UpgradePlaneBtn()
    {
        if (playerScript.planeUpgradeLevel < 10)
        {
            playerScript.planeUpgradeLevel++;
            Plane planePowerup = new Plane(playerScript.planeUpgradeLevel, 1250 * playerScript.planeUpgradeLevel, planeSlider, planeSliderFill, planeLevelText, planeCostTxt);
            playerScript.SavePlayer();
        }
        else
        {
            planeCostBox.SetActive(false);
            planeMax.SetActive(true);
        }
    }


}
