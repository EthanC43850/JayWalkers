using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balloon : Powerup
{
    /*public Balloon()
    {
        level = 5;
        cost = 17750;
        Debug.Log("First Balloon constructor called");
    }*/

    public Balloon(int newLevel, int newCost, Slider slider, Image sliderFill, Text levelTxt, Text costTxt) : base(newLevel, newCost, slider, sliderFill, levelTxt, costTxt)
    {
        Debug.Log("2nd Balloon Constructor called");
    }
}
