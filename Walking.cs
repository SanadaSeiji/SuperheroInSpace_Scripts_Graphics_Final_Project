using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour {

    public GameObject oculusLeftEye;

    public float forwardSpeed = 1f;
    float rotationSpeed = 5f;

    Vector3 axis;
    float rotationY;
    //float rotationX;
    //float rotationZ;

    //bool start = false;

    bool isWalking = false;
    //Rigidbody rigidBody;


    void WalkingMode()
    {

        // rotation first : in 2D, where do you want to rotate...

        //get rotation values for the LeftEye
        rotationY = oculusLeftEye.transform.localRotation.y/2; //rdw already
        
        //put them into a vector
        axis = new Vector3 (0f, rotationY, 0f);

        //Rotate
        transform.Rotate(axis * Time.deltaTime * rotationSpeed);

        //another approach
        //rotationY = oculusLeftEye.transform.localRotation.y * rotationSpeed*Mathf.Deg2Rad;
        //transform.RotateAround (Vector3.left, rotationY);

        //translation then
        //!!!ouside: input- H/V wire up with oculus touch
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp)) //left Hand
        {
            transform.Translate(new Vector3(forwardSpeed * Time.deltaTime, 0f, 0f));

            //transform.Position += new Vector3 (Input.getAxis("Horizontal") * forwardSpeed * Time.deltaTime, 0f, Input.getAxis("Vertical") * forwardSpeed * Time.deltaTime);
        }

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft)) //left hand
        {
            transform.Translate(new Vector3(0f, 0f, -forwardSpeed * Time.deltaTime));

            //transform.Position += new Vector3 (Input.getAxis("Horizontal") * forwardSpeed * Time.deltaTime, 0f, Input.getAxis("Vertical") * forwardSpeed * Time.deltaTime);
        }

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight)) // right hand
        {

            transform.Translate(new Vector3(0f, 0f, forwardSpeed * Time.deltaTime));
        }

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
        { //right hand

            transform.Translate(new Vector3(-forwardSpeed * Time.deltaTime, 0f, 0f));
        }

    }   
   



    // Use this for initialization
    void Start () {

       // rigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        
            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight) || OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown)) {

                if (isWalking == false)
                    isWalking = true;
                else
                    isWalking = false;

            }// outside: wire input: vertical and horizontal with proper joystick buttons
             // say, button- one on right hand

            if (isWalking == true)
            {
                WalkingMode();
            }
        
    }

    /*
    private void OnCollisionStay(Collision collision)
    {
        walkBool = true;
        rigidBody.useGravity = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        walkBool = false;
        rigidBody.useGravity = true;
    }
    */
}
