using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpin : MonoBehaviour
{
    private float speed;



    void Start()
    {
        speed = Random.Range(1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speed);
        transform.RotateAround(transform.position, Vector3.up, speed);
    }
}
