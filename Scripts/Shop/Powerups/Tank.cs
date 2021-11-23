using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank : Powerup
{
    /*public Tank()
    {
        level = 5;
        cost = 17750;
        Debug.Log("First tank constructor called");
    }*/

    public Tank(int newLevel, int newCost, Slider slider, Image sliderFill, Text levelTxt, Text costTxt) : base(newLevel, newCost, slider, sliderFill, levelTxt, costTxt){
        Debug.Log("2nd Tank Constructor called");
    }


}
