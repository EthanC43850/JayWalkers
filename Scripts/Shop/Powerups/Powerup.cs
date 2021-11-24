using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup
{
    public int cost;
    public int level;
    public Slider slider;
    public Text levelTxt;
    public Text costTxt;

    /*public Powerup()
    {
        level = 0;
        cost = 0;
        Debug.Log("1st powerup constructor called");
    }*/

    public Powerup(int newLevel, int newCost, Slider slider, Text levelTxt, Text costTxt)
    {
        level = newLevel;
        cost = newCost;
        if (level < 10)
        {
            slider.value = level;
            levelTxt.text = level + "";
            costTxt.text = cost + "";
        }

        Debug.Log("2nd powerup constructor called");
    }

    public void Display()
    {
        Debug.Log("Level: " + level + "    Cost: " + cost);
    }


}
