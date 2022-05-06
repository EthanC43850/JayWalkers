using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossStreetTrigger : MonoBehaviour
{

    public GameManager gameManagerScript;
    [SerializeField] private List<BoxCollider> streetTriggers;



    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DisableStreetTriggers());
        gameManagerScript.playerInfoUI.GetComponent<Animator>().SetBool("Open_b", false);

        gameManagerScript.gameplayCamera.Priority = 20;
        gameManagerScript.mainCameraScript.smoothSpeed = 1;
        gameManagerScript.mainCameraScript.StartCoroutine("CameraSmoothStart");
        gameManagerScript.mainCameraScript.player.transform.rotation = Quaternion.Euler(0f, -90f, 0f);    //Ensure player faces the right way after jaywalking
        gameManagerScript.playerControllerScript.EnableRunningState();

        gameManagerScript.insideTown = false;
        gameManagerScript.mainMenu = false;
        gameManagerScript.gameOver = false;
        gameManagerScript.gameStart = true;
        gameManagerScript.thirdPersonCamera.SetActive(false);

        gameManagerScript.musicSource.Play();
        gameManagerScript.cityAmbiance.volume = 0.2f;
        gameManagerScript.cityMenuMusic.volume = 0f;


        gameManagerScript.titleScreenScript.playerScript.jailTime++;

    }


    IEnumerator DisableStreetTriggers()
    {
        foreach (BoxCollider trigger in streetTriggers)
        {
            trigger.enabled = false;

        }

        yield return new WaitForSeconds(18.0f);

        foreach (BoxCollider trigger in streetTriggers)
        {
            trigger.enabled = true;

        }


    }




}
