using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBox : MonoBehaviour
{
    
    public float scrollSpeed = 0.5f;
    private float offset;
    public GameObject skyBox;
    public GameObject player;
    private Renderer rend;

    public GameManager gameManagerScript;


    void Start()
    {
        skyBox.transform.position = new Vector3(1672, -8.57f, 261);
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(gameManagerScript.gameStart == true)
        {
            skyBox.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, skyBox.transform.position.z);
            
        }
        else if (gameManagerScript.insideTown)
        {
            //Debug.Log("move skybox back to start");
            StartCoroutine("ReturnToOrigin");
        }
        offset = scrollSpeed * Time.time;
        //change skybox from day to night
        //rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));



    }

    IEnumerator ReturnToOrigin()
    {
        yield return new WaitForSeconds(1.7f); //Wait 1.5 seconds for the screen to go black before returning skybox to origin
        skyBox.transform.position = new Vector3(1672, -8.57f, 261);

    }

}
