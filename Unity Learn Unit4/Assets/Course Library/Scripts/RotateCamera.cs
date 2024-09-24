using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float horizontalSepeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up, horizontalSepeed * horizontalMove * Time.deltaTime);
    }
}
