using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Player playerScript;
    public GameManager gameManagerScript;
    public AudioSource playerAudio;
    public AudioClip powerUpSound;

    [Header("Tabs")]
    public GameObject itemsTab;
    public GameObject upgradesTab;
    public GameObject soundTracksTab;
    public GameObject playerTab;
    public Text TabDescription;

    [Header("SoundTrackUI")]
    public Image soundTrack1_SliderFill;
    public Image soundTrack2_SliderFill;
    public Image soundTrack3_SliderFill;
    public bool soundTrack1_IsActive;
    public bool soundTrack2_IsActive;
    public bool soundTrack3_IsActive;
    public int soundTrack1_completionValue;
    public int soundTrack2_completionValue;
    public int soundTrack3_completionValue;
    public Slider track1_Progress_Slider;
    public Slider track2_Progress_Slider;
    public Slider track3_Progress_Slider;
    public Text soundTrack1Progress_Text;
    public Text soundTrack2Progress_Text;
    public Text soundTrack3Progress_Text;

    public int soundTrack1Cost;
    public int soundTrack2Cost;
    public int soundTrack3Cost;
    public GameObject soundTrack1BoughtButton;
    public GameObject soundTrack2BoughtButton;
    public GameObject soundTrack3BoughtButton;
    public Text soundTrack1BoughtButton_Text;
    public Text soundTrack2BoughtButton_Text;
    public Text soundTrack3BoughtButton_Text;
    public GameObject soundTrack1CostBox;
    public GameObject soundTrack2CostBox;
    public GameObject soundTrack3CostBox;
    public Text soundTrack1Cost_Text;
    public Text soundTrack2Cost_Text;
    public Text soundTrack3Cost_Text;
    public AudioClip soundTrack1;
    public AudioClip soundTrack2;
    public AudioClip soundTrack3;
    



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

    [Header("Endurance Card UI")]
    public Slider enduranceSlider;
    public Image enduranceSliderFill;
    public Text enduranceLevelText;
    public Text enduranceCostText;
    public GameObject enduranceMax;
    public GameObject enduranceCostBox;



    public int upgradeCost = 75;


    void Start()
    {
        Balloon balloonPowerup = new Balloon(1, upgradeCost, balloonSlider, balloonLevelTxt, balloonCostTxt);
        Tank tankPowerup = new Tank(1, upgradeCost, tankSlider, tankLevelTxt, tankCostTxt);
        Plane planePowerup = new Plane(1, upgradeCost, planeSlider, planeLevelText, planeCostTxt);
        Powerup endurancePowerUp = new Powerup(1, upgradeCost, enduranceSlider, enduranceLevelText, enduranceCostText);

        soundTrack1Cost_Text.text = soundTrack1Cost.ToString();
        soundTrack2Cost_Text.text = soundTrack2Cost.ToString();
        soundTrack3Cost_Text.text = soundTrack3Cost.ToString();

        track1_Progress_Slider.value = 0;
        track2_Progress_Slider.value = 0;
        track3_Progress_Slider.value = 0;


        track1_Progress_Slider.maxValue = soundTrack1_completionValue;
        track2_Progress_Slider.maxValue = soundTrack2_completionValue;
        track3_Progress_Slider.maxValue = soundTrack3_completionValue;

    /*  Use to test how Constructors work
    Powerup myPowerup = new Powerup();
    Balloon balloonPowerup = new Balloon();
    Tank tankPowerup = new Tank();
    Plane planePowerup = new Plane();*/
}

public void UpgradeBalloonBtn()
    {
        if (playerScript.balloonUpgradeLevel < 10 && playerScript.currentTotalCoinCount > upgradeCost * playerScript.balloonUpgradeLevel)
        {
            playerAudio.PlayOneShot(powerUpSound, 1.0f);
            playerScript.currentTotalCoinCount -= upgradeCost * playerScript.balloonUpgradeLevel;

            playerScript.balloonUpgradeLevel++;
            Balloon balloonPowerup = new Balloon(playerScript.balloonUpgradeLevel, upgradeCost * playerScript.balloonUpgradeLevel, balloonSlider, balloonLevelTxt, balloonCostTxt);
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
        if (playerScript.tankUpgradeLevel < 10 && playerScript.currentTotalCoinCount > upgradeCost * playerScript.tankUpgradeLevel)
        {
            playerAudio.PlayOneShot(powerUpSound, 1.0f);
            playerScript.currentTotalCoinCount -= upgradeCost * playerScript.tankUpgradeLevel;

            playerScript.tankUpgradeLevel++;
            Tank tankPowerup = new Tank(playerScript.tankUpgradeLevel, upgradeCost * playerScript.tankUpgradeLevel, tankSlider, tankLevelTxt, tankCostTxt);
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
        if (playerScript.planeUpgradeLevel < 10 && playerScript.currentTotalCoinCount > upgradeCost * playerScript.planeUpgradeLevel)
        {
            playerAudio.PlayOneShot(powerUpSound, 1.0f);
            playerScript.currentTotalCoinCount -= upgradeCost * playerScript.planeUpgradeLevel;

            playerScript.planeUpgradeLevel++;
            Plane planePowerup = new Plane(playerScript.planeUpgradeLevel, upgradeCost * playerScript.planeUpgradeLevel, planeSlider, planeLevelText, planeCostTxt);
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

    public void UpgradeEnduranceBtn()
    {
        if (playerScript.enduranceUpgradeLevel < 10 && playerScript.currentTotalCoinCount > upgradeCost * playerScript.enduranceUpgradeLevel)
        {
            playerAudio.PlayOneShot(powerUpSound, 1.0f);
            playerScript.currentTotalCoinCount -= upgradeCost * playerScript.enduranceUpgradeLevel;
            playerScript.enduranceUpgradeLevel++;
            Powerup endurancePowerup = new Powerup(playerScript.enduranceUpgradeLevel, upgradeCost * playerScript.enduranceUpgradeLevel, enduranceSlider, enduranceLevelText, enduranceCostText);
            gameManagerScript.UpdatePlayerUI(playerScript);
            playerScript.SavePlayer();
            
        }

        if (playerScript.enduranceUpgradeLevel == 10)
        {
            enduranceCostBox.SetActive(false);
            enduranceMax.SetActive(true);
            enduranceLevelText.text = playerScript.enduranceUpgradeLevel + "";
            enduranceSlider.value = enduranceSlider.maxValue;
            enduranceSliderFill.color = Color.green;
        }
    }




    public void Cheat()
    {
        playerScript.currentTotalCoinCount = 999999;
        playerScript.currentTotalSodaCount = 9999;
        gameManagerScript.UpdatePlayerUI(playerScript);




    }

   public void OpenItemsTab()
    {
        itemsTab.SetActive(true);
        upgradesTab.SetActive(false);
        soundTracksTab.SetActive(false);
        playerTab.SetActive(false);

        TabDescription.text = "Buy Items and run to the sun!";
    }


    public void OpenUpgradesTab()
    {
        itemsTab.SetActive(false);
        upgradesTab.SetActive(true);
        soundTracksTab.SetActive(false);
        playerTab.SetActive(false);

        TabDescription.text = "Upgrade the duration of a powerup!";

    }


    public void OpenSoundTracksTab()
    {
        itemsTab.SetActive(false);
        upgradesTab.SetActive(false);
        soundTracksTab.SetActive(true);
        playerTab.SetActive(false);

        TabDescription.text = "Switch Up the Tunes!";

    }


    public void OpenPlayerTab()
    {
        itemsTab.SetActive(false);
        upgradesTab.SetActive(false);
        soundTracksTab.SetActive(false);
        playerTab.SetActive(true);
        TabDescription.text = "Upgrade Player Attributes!";


    }


    public void SoundTrack_1_BuyButton()
    {
        
        if(playerScript.currentTotalCoinCount >= soundTrack1Cost)
        {
            soundTrack1CostBox.SetActive(false);
            soundTrack1BoughtButton.SetActive(true);
            playerAudio.PlayOneShot(powerUpSound, 1.0f);
            playerScript.currentTotalCoinCount -= soundTrack1Cost;
            gameManagerScript.musicSource.clip = soundTrack1;
            gameManagerScript.UpdatePlayerUI(playerScript);
            playerScript.SavePlayer();

            soundTrack1_IsActive = true;
            soundTrack2_IsActive = false;
            soundTrack3_IsActive = false;

            soundTrack1BoughtButton_Text.text = "Active";
            soundTrack2BoughtButton_Text.text = "Inactive";
            soundTrack3BoughtButton_Text.text = "Inactive";

        }


    }

    public void SoundTrack_2_BuyButton()
    {
        if (playerScript.currentTotalCoinCount >= soundTrack2Cost)
        {
            soundTrack2CostBox.SetActive(false);
            soundTrack2BoughtButton.SetActive(true);
            playerAudio.PlayOneShot(powerUpSound, 1.0f);
            playerScript.currentTotalCoinCount -= soundTrack2Cost;
            gameManagerScript.musicSource.clip = soundTrack2;
            gameManagerScript.UpdatePlayerUI(playerScript);
            playerScript.SavePlayer();

            soundTrack1_IsActive = false;
            soundTrack2_IsActive = true;
            soundTrack3_IsActive = false;

            soundTrack1BoughtButton_Text.text = "Inactive";
            soundTrack2BoughtButton_Text.text = "Active";
            soundTrack3BoughtButton_Text.text = "Inactive";


        }

    }

    public void SoundTrack_3_BuyButton()
    {
        if (playerScript.currentTotalCoinCount >= soundTrack3Cost)
        {
            soundTrack3CostBox.SetActive(false);
            soundTrack3BoughtButton.SetActive(true);
            playerAudio.PlayOneShot(powerUpSound, 1.0f);
            playerScript.currentTotalCoinCount -= soundTrack3Cost;
            gameManagerScript.musicSource.clip = soundTrack3;
            gameManagerScript.UpdatePlayerUI(playerScript);
            playerScript.SavePlayer();

            soundTrack1_IsActive = false;
            soundTrack2_IsActive = false;
            soundTrack3_IsActive = true;

            soundTrack1BoughtButton_Text.text = "Inactive";
            soundTrack2BoughtButton_Text.text = "Inactive";
            soundTrack3BoughtButton_Text.text = "Active";

        }


    }

    public void SoundTrack_1_Activate()
    {
        soundTrack1_IsActive = true;
        soundTrack2_IsActive = false;
        soundTrack3_IsActive = false;
        gameManagerScript.musicSource.clip = soundTrack1;
        soundTrack1BoughtButton_Text.text = "Active";
        soundTrack2BoughtButton_Text.text = "Inactive";
        soundTrack3BoughtButton_Text.text = "Inactive";

    }

    public void SoundTrack_2_Activate()
    {
        soundTrack1_IsActive = false;
        soundTrack2_IsActive = true;
        soundTrack3_IsActive = false;
        gameManagerScript.musicSource.clip = soundTrack2;

        soundTrack1BoughtButton_Text.text = "Inactive";
        soundTrack2BoughtButton_Text.text = "Active";
        soundTrack3BoughtButton_Text.text = "Inactive";

    }

    public void SoundTrack_3_Activate()
    {
        soundTrack1_IsActive = false;
        soundTrack2_IsActive = false;
        soundTrack3_IsActive = true;
        gameManagerScript.musicSource.clip = soundTrack3;

        soundTrack1BoughtButton_Text.text = "Inactive";
        soundTrack2BoughtButton_Text.text = "Inactive";
        soundTrack3BoughtButton_Text.text = "Active";

    }

    public void AddProgressToSoundTrack(int scoreToAdd)
    {
        if (soundTrack1_IsActive)
        {

            gameManagerScript.playerControllerScript.soundTrack1_Progress += scoreToAdd;
            track1_Progress_Slider.value = gameManagerScript.playerControllerScript.soundTrack1_Progress;
            soundTrack1Progress_Text.text = gameManagerScript.playerControllerScript.soundTrack1_Progress.ToString();
            if(gameManagerScript.playerControllerScript.soundTrack1_Progress >= soundTrack1_completionValue)
            {
                soundTrack1_SliderFill.color = Color.green;
            }

        }
        else if (soundTrack2_IsActive)
        {
            gameManagerScript.playerControllerScript.soundTrack2_Progress += scoreToAdd;
            track2_Progress_Slider.value = gameManagerScript.playerControllerScript.soundTrack2_Progress;
            soundTrack2Progress_Text.text = gameManagerScript.playerControllerScript.soundTrack2_Progress.ToString();
            if (gameManagerScript.playerControllerScript.soundTrack2_Progress >= soundTrack2_completionValue)
            {
                soundTrack2_SliderFill.color = Color.green;
            }
        }
        else if (soundTrack3_IsActive)
        {
            gameManagerScript.playerControllerScript.soundTrack3_Progress += scoreToAdd;
            track3_Progress_Slider.value = gameManagerScript.playerControllerScript.soundTrack3_Progress;
            soundTrack3Progress_Text.text = gameManagerScript.playerControllerScript.soundTrack3_Progress.ToString();
            if (gameManagerScript.playerControllerScript.soundTrack3_Progress >= soundTrack3_completionValue)
            {
                soundTrack3_SliderFill.color = Color.green;
            }
        }




    }



}
