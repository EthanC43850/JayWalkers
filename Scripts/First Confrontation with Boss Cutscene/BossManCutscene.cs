using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossManCutscene : MonoBehaviour
{

    public PlayableDirector bossManFirstCutscene;
    public DialogueTrigger cutsceneDialogue;
    public GameManager gameManagerScript;
    private bool hasPlayed = false;
    private bool play = true;
    private ThirdPersonMovement player;

    private void OnTriggerEnter(Collider other)
    {
        if(hasPlayed == true) { return; }
        player = other.gameObject.GetComponent<ThirdPersonMovement>();
        if(player == null) { return; }
        play = false;
        gameManagerScript.dialogueOpen = true;  
        cutsceneDialogue.TriggerDialogue();
        bossManFirstCutscene.Play();
        player.freeLookScript.SetActive(false);
        gameManagerScript.isMenuOpen = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.SetActive(false);

    }

    private void Update()
    {
        if (!gameManagerScript.dialogueOpen && hasPlayed == false && !play)
        {
            bossManFirstCutscene.Stop();
            gameManagerScript.isMenuOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if(player != null)
            {
                player.freeLookScript.SetActive(true);
                gameManagerScript.isMenuOpen = false;

            }
            hasPlayed = true;
        }


    }


}
