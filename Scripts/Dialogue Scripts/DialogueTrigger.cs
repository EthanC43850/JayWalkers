using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //Self-made dialogue class holds data like the characters' names and sentences.
    //This data will need to be fed to the dialogue manager
    public Dialogue dialogue;                   

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void TriggerSpecificDialogue(Dialogue dialogue)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }


}
