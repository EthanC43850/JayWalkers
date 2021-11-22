using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThrow : MonoBehaviour
{
    private float shotForce = 4;
    public GameObject vanLogs;
    public GameObject vanPlanks;
    public GameObject blueCar;
    //public GameObject greenCar;

    public ParticleSystem explosionFx;

    void Start()
    {

    }


    void Update()
    {

        transform.Translate(Vector3.forward * shotForce, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("TreeLogs"))
        {
            Instantiate(explosionFx, transform.position, Quaternion.identity);
            Destroy(collision.transform.parent.gameObject); //Destroys game objects with tree logs/ planks on top
            Destroy(gameObject);
            Debug.Log("DESTROYED LOGS");

        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Instantiate(explosionFx, transform.position, Quaternion.identity);
            Destroy(collision.gameObject); //destroys objects without tree logs/planks
            Destroy(gameObject);
            Debug.Log("DESTORYED BASIC CAR");
        }
        else 
        {
            Instantiate(explosionFx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        

    }
}
