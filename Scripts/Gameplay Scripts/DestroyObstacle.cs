using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    public bool platformExists;
    public int rayCastLength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = transform.position + Vector3.up;
        Vector3 direction = transform.TransformDirection(Vector3.down);
        Debug.DrawRay(origin, direction * rayCastLength, Color.red);
        platformExists = Physics.Raycast(origin, direction, rayCastLength);

        if (!platformExists)
        {
            Destroy(gameObject);
        }

    }
}
