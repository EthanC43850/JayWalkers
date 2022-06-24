using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpMeter : MonoBehaviour
{
    public Slider energySlider;
    public Slider slider;
    public Slider tankSlider;
    public Slider planeSlider;

    public Gradient gradient;
    public Image energyMeterFillImage;

    public PlayerController playerControllerScript;


    public void SetEnergyMeterMax(float totalDuration)
    {
        //Debug.Log("The max value of the slider is set to " + totalDuration);
        energySlider.maxValue = totalDuration;
        energySlider.value = totalDuration;
        energyMeterFillImage.color = gradient.Evaluate(1f);

    }

    public void UpdateEnergyMeter(float timeRemaining) // I could combine all powerups into one function but exiting the function needs different FX
    {
        //Debug.Log("time remaining = " + timeRemaining);
        energySlider.value = timeRemaining;
        energyMeterFillImage.color = gradient.Evaluate(energySlider.normalizedValue);

        if (timeRemaining <= 0)
        {
            playerControllerScript.GameOver();
            playerControllerScript.balloonDisappearFx.Play();
        }
    }



    public void SetBalloonMeterMax(float totalDuration)
    {
        //Debug.Log("The max value of the slider is set to " + totalDuration);
        slider.maxValue = totalDuration;
        slider.value = totalDuration;
    }

    public void UpdateBalloonPowerMeter(float timeRemaining) // I could combine all powerups into one function but exiting the function needs different FX
    {
        //Debug.Log("time remaining = " + timeRemaining);
        slider.value = timeRemaining;
        if(timeRemaining <= 0)
        {
            playerControllerScript.balloonPowerup = false;
            playerControllerScript.playerBalloons.SetActive(false);
            playerControllerScript.balloonPowerupMeterUI.SetActive(false);
            playerControllerScript.balloonDisappearFx.Play();
            playerControllerScript.balloonEasterEggCollider.SetActive(true);
        }
    }


    public void SetTankMeterMax(float totalDuration)
    {
        tankSlider.maxValue = totalDuration;
        tankSlider.value = totalDuration;
    }
    
    public void UpdateTankPowerMeter(float timeRemaining)
    {
        Debug.Log("time remaining = " + timeRemaining);
        tankSlider.value = timeRemaining;
        if(timeRemaining <= 0)
        {
            playerControllerScript.playerRb.constraints = RigidbodyConstraints.FreezeRotation;
            playerControllerScript.transform.localRotation = Quaternion.Slerp(Quaternion.identity, playerControllerScript.normalRotation, 1f);
            playerControllerScript.powerupSmoke.Play();
            playerControllerScript.tankPowerup = false;
            playerControllerScript.miniTank.SetActive(false);
            playerControllerScript.tankPowerUpMeterUI.SetActive(false);


            playerControllerScript.playerCollider.size = new Vector3(2, 3, 0.86f);  //returns player collider to normal size
            playerControllerScript.playerMesh.SetActive(true);
        }
    }


    public void SetPlanePowerMeterMax(float totalDuration)
    {
        planeSlider.maxValue = totalDuration;
        planeSlider.value = totalDuration;
    }


    public void UpdatePlanePowerMeter(float timeRemaining)
    {
        //Debug.Log("time remaining = " + timeRemaining);
        planeSlider.value = timeRemaining;
        if (timeRemaining <= 0)
        {
            playerControllerScript.powerupSmoke.Play();
            playerControllerScript.audioSource.loop = false;
            playerControllerScript.audioSource.Stop();
            playerControllerScript.transform.rotation = Quaternion.Euler(0f, -90f, 0f); //Correct the players tilt
            playerControllerScript.planePowerup = false;
            playerControllerScript.plane.SetActive(false);
            playerControllerScript.planePowerupMeterUI.SetActive(false);

            playerControllerScript.playerMesh.SetActive(true);
            playerControllerScript.playerRb.useGravity = true;
            playerControllerScript.speed = playerControllerScript.speedBeforePowerup;
            //playerControllerScript.playerCollider.size = new Vector3(2, 3, 0.86f);  //returns player collider to normal size
            
        }

    }


}
