//
//  Tutorial Help from Brackeys
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameManager gameManagerScript;
    //Queue of sentences of type string
    private Queue<string> sentences;

    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    public int sceneNumber;

    public TimelineController timeLineControllerScript;


    void Start()
    {
        sentences = new Queue<string>();            //Initialize variable
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("Is_Open", true);
        nameText.text = dialogue.name;

        //Clear sentences from a previous conversation
        //and enqueue sentences from new character
        sentences.Clear();                          
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }


        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //If this is true, the end of the queue was reached
        if(sentences.Count == 0)
        {
            EndDialogue();
            IncrementSceneNumber();
            return;
        }


        //Get the next sentence from the queue and display it
        string sentence = sentences.Dequeue();

        //If the player interupts a sentence the current sentence 
        //being written out will stop
        StopAllCoroutines();

        //Sets dialgue text
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        //sentence is transformed into an array of characters
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null; 
        }
    }


    public void EndDialogue()
    {
        animator.SetBool("Is_Open", false);
        gameManagerScript.dialogueOpen = false;
    }

    public void IncrementSceneNumber()
    {
        sceneNumber += 1;
        timeLineControllerScript.PlayFromDirectors(sceneNumber);
    }



}
