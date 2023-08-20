using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCantoGem : MonoBehaviour
{
    public Transform rotationCenter; 
    public float omega = 30f;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(rotationCenter.position, Vector3.forward, omega * Time.deltaTime);
    }
}
