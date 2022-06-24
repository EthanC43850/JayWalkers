using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinMeter : MonoBehaviour
{
    public Slider slider;
    public int maxCount;
    public int coinBonusAmount = 250;
    public int currentCoinSliderCount; // Used so that the coin meter can be reset after reaching max
    public GameManager gameManagerScript;


    void Start()
    {
        //currentCoinCount = maxCount;
        slider.maxValue = maxCount;

    }

    public void SetMeterMax(int coin)
    {
        slider.maxValue = coin;
        slider.value = coin;
    }

    public void SetMeter(int coin)
    {

        gameManagerScript.totalCurrentCash += coin;
        currentCoinSliderCount += coin;
        slider.value = currentCoinSliderCount;

        if(slider.value == maxCount)
        {
            gameManagerScript.totalCurrentCash += coinBonusAmount;


            gameManagerScript.playerControllerScript.playerScript.currentTotalCoinCount += coinBonusAmount;
            gameManagerScript.playerControllerScript.playerScript.networth += coinBonusAmount;
            currentCoinSliderCount = 0;
            slider.value = currentCoinSliderCount;
        }

    }









}
