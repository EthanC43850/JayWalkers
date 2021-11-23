using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Plane : Powerup
{
    /*public Plane()
    {
        level = 5;
        cost = 17750;
        Debug.Log("First Plane constructor called");
    }*/

    public Plane(int newLevel, int newCost, Slider slider, Text levelTxt, Text costTxt) : base(newLevel, newCost, slider, levelTxt, costTxt)
    {
        Debug.Log("2nd Plane Constructor called");
    }
}
