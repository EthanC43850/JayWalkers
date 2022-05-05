using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Highlight : MonoBehaviour
{
    public GameObject groundMarker;
    public Button characterSelectBackBtn;
    public Button characterSelectBtn;
    public CinemachineVirtualCamera vcam;
    public BoxCollider highLightScriptBoxCollider;      //If issues arise with this collider, deactivate it on game start

    public TitleScreen titleScreenScript;
    //public Player playerDataScript;

    [Header("Character Info:")]
    [SerializeField] Color thugTitleColor = Color.blue;      //Public color does not work
    public string thugTitle;
    [TextArea(3, 10)]                            //Allows more room to edit dialogue in editor
    public string characterDescriptionText;
    public int characterNumber;


    void Start()
    {
        //using this code allows all Virtual cameras to reset back to 5 during character
        //selection when the back button is pressed
        characterSelectBtn.onClick.AddListener(SelectCharacter);
        characterSelectBackBtn.onClick.AddListener(ExitCharacter);
    }


    //Highlight the player being hovered over
    void OnMouseEnter()
    {
        if (titleScreenScript.characterSelectActive && !titleScreenScript.playerSelected)
        {
            groundMarker.SetActive(true);
        }
        
    }
    void OnMouseExit()
    {
        if (titleScreenScript.characterSelectActive && !titleScreenScript.playerSelected)
        {
            groundMarker.SetActive(false);

        }
    }


    //Display description of selected character
    private void OnMouseUpAsButton()
    {
        if (titleScreenScript.characterSelectActive && !titleScreenScript.playerSelected)
        {
            titleScreenScript.playerSelected = true;
            titleScreenScript.characterDescriptionBoxAnimator.SetBool("Is_Open", true);
            titleScreenScript.characterSelectUIAnimator.SetBool("Is_Open", false);

            vcam.Priority = 15;
            titleScreenScript.thugTitleText.color = thugTitleColor;
            titleScreenScript.thugTitleText.text = thugTitle;
            titleScreenScript.characterDescriptionText.text = characterDescriptionText;


            titleScreenScript.currentCharacterNumber = characterNumber;
            titleScreenScript.characterTypeUI.text = thugTitle;
            
            

            Debug.Log("selected " + gameObject.name);
            

        }
    }

    //Will run 4 times because of the number of gameobjects holding this script.
    public void SelectCharacter()
    {
        titleScreenScript.characterDescriptionBoxAnimator.SetBool("Is_Open", false);
        titleScreenScript.playerCardUI.SetActive(true);
        titleScreenScript.playerCardButtons.SetActive(true);
    }

    public void ExitCharacter()
    {
        groundMarker.SetActive(false);
        titleScreenScript.characterDescriptionBoxAnimator.SetBool("Is_Open", false);
        titleScreenScript.characterSelectUIAnimator.SetBool("Is_Open", true);

        titleScreenScript.playerSelected = false;
        
        vcam.Priority = 5;
        
    }

    


    



}
