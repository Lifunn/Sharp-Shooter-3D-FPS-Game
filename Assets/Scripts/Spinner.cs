using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float spinSpeed = 3600; // degrees per second
    public bool doSpin = false;

    private Rigidbody rb;

    public GameObject playerGhrapics;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (doSpin)
        {
            playerGhrapics.transform.Rotate(new Vector3(0, spinSpeed * Time.deltaTime, 0));
        }
    }
}
