using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggCashBonus : MonoBehaviour
{
    public ParticleSystem yellowFireworks;
    private bool collected = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() && collected == false)
        {
            other.gameObject.GetComponent<Player>().currentTotalCoinCount += 5000;
            FindObjectOfType<GameManager>().UpdatePlayerUI(other.gameObject.GetComponent<Player>());
            yellowFireworks.Play();
            Destroy(gameObject);
            collected = true;
        }
    }
}
