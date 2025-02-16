using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody theRB;

    public float maxSpeed;


    public float forwardAccel = 8f, reverseAccel = 4f;
    
    private float speedInput;

    public float turnStrength = 180f; 

    private float turnInput;


    public Transform leftFrontWheel, rightFrontWheel;
    public float maxWheelTur = 25f;


    // Start is called before the first frame update
    void Start()
    {
        theRB.transform.parent = null;


    }

    // Update is called once per frame
    void Update()
    {
        speedInput = 0f;
        if(Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel;
        } else if(Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel;
        }

        turnInput = Input.GetAxis("Horizontal");

        if(Input.GetAxis("Horizontal") != 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Mathf.Sign(speedInput) * (theRB.velocity.magnitude / maxSpeed), 0f));
        }



        //turning the wheel
        leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTur) - 180, leftFrontWheel.localRotation.eulerAngles.z);
        rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTur), rightFrontWheel.localRotation.eulerAngles.z);


        transform.position = theRB.position;

    }

    private void FixedUpdate()
    {
        theRB.AddForce(transform.forward * speedInput* 1000f);

        if(theRB.velocity.magnitude > maxSpeed)
        {
            theRB.velocity = theRB.velocity.normalized;
        }

        //Debug.Log(theRB.velocity.magnitude);
    }

}
