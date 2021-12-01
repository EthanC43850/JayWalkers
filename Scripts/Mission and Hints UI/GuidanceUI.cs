using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidanceUI : MonoBehaviour
{

    public TitleScreen titleScreenScript;
    [SerializeField] private Animator animator;

    [TextArea(3, 10)]                            //Allows more room to edit dialogue in editor
    public string[] sentences;

    [SerializeField] private Text guidanceText;
    public int dialogueNumber;


    void Update()
    {

        if (dialogueNumber == 0 && titleScreenScript.count == 6)
        {
            StartCoroutine(DisplayHint(3.0f, dialogueNumber));
            dialogueNumber++;       //Don't place in coroutine
        }

        if (dialogueNumber == 1 && titleScreenScript.count == 6 && titleScreenScript.playerSelected == true) {
            StartCoroutine(DisplayHint(9.0f, dialogueNumber));
            dialogueNumber++;

        }

        if (dialogueNumber == 2 && titleScreenScript.count == 7 && titleScreenScript.characterSelectActive != true)
        {
            StartCoroutine(DisplayHint(9.0f, dialogueNumber));
            dialogueNumber++;

        }

    }

    IEnumerator DisplayHint(float duration, int num)
    {
        Debug.Log("Display Hint " + dialogueNumber);
        yield return new WaitForSeconds(1.0f);
        guidanceText.text = sentences[num];
        animator.SetBool("Open_b", true);
        yield return new WaitForSeconds(duration);
        animator.SetBool("Open_b", false);
        

    }

    //Disable this game object for better runtime?






}
