using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomPowerup : MonoBehaviour
{
    public TitleScreen titleScreenScript;
    private bool spawned = false;
    private float randVal;
    public GameObject[] powerCollectibles;

    /*[SerializeField] GameObject sodaCollectible;
    [SerializeField] GameObject balloonCollectible;
    [SerializeField] GameObject tankCollectible;
    [SerializeField] GameObject planeCollectible;*/
    /*[SerializeField] float percentChanceOfBalloon = 0.3f;
    [SerializeField] float percentChanceOfTank = 0.3f;
    [SerializeField] float percentChanceOfPlane = 0.3f;*/

    private void Awake()
    {
        titleScreenScript = FindObjectOfType<TitleScreen>();
        randVal = Random.Range(0, 3);

    }

    void Start()
    {



        if (spawned) { return; }
        if(randVal == 0 && titleScreenScript.playerScript.planeUnlocked_b)
        {
            powerCollectibles[0].SetActive(true);
        }
        else if(randVal == 1)
        {
            powerCollectibles[1].SetActive(true);

        }
        else if (randVal == 2)
        {
            powerCollectibles[2].SetActive(true);

        }
        else
        {
            powerCollectibles[3].SetActive(true);

        }
        spawned = true;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Destroy(this.gameObject, 3.0f);
        }
    }

    //backup
    /*
     using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomPowerup : MonoBehaviour
{
    public TitleScreen titleScreenScript;
    private bool spawned = false;
    private float randVal;
    [SerializeField] GameObject sodaCollectible;
    [SerializeField] GameObject balloonCollectible;
    [SerializeField] GameObject tankCollectible;
    [SerializeField] GameObject planeCollectible;
    /*[SerializeField] float percentChanceOfBalloon = 0.3f;
    [SerializeField] float percentChanceOfTank = 0.3f;
    [SerializeField] float percentChanceOfPlane = 0.3f;*/
    /*
    private void Awake()
    {
        titleScreenScript = FindObjectOfType<TitleScreen>();
    }

    void Start()
    {
        randVal = Random.value;

        if (spawned) { return; }
        if (randVal <= 0.3 && titleScreenScript.playerScript != null)
        {
            if (titleScreenScript.playerScript.planeUnlocked_b)
            {
                planeCollectible.SetActive(true);

            }
        }
        else if (randVal > 0.3 && randVal <= 0.6)
        {
            balloonCollectible.SetActive(true);

        }
        else if (randVal > 0.6)
        {
            tankCollectible.SetActive(true);

        }
        else
        {
            sodaCollectible.SetActive(true);

        }
        spawned = true;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Destroy(this, 3.0f);
        }
    }


}
*/

}
