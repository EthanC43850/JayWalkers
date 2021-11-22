using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinMeter : MonoBehaviour
{
    public Slider slider;
    public int maxCount;
    //public int currentCoinCount;
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
        slider.value = gameManagerScript.totalCurrentCash;
    }









}
