using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flying : MonoBehaviour {

    public GameObject oculusLeftEye;
    public GameObject oculusRightEye;
    public GameObject oculusLeftHand;
    public GameObject oculusRightHand;

    Rigidbody rr;

    public float forwardSpeed = 250f;
    float rotationSpeed = 1f;
    public float acclr = 0f;
    private float armAclr = 0f;

    Vector3 axis;
    float rotationY;
    float rotationX;
    float rotationZ;

    public float grabRadius = 0;
    public GameObject startObj;
    public LayerMask collMask;

    public float hitTime;
    private bool hitOccured = false;

    private bool isMoving = false;
    private bool isFlying = false;

    void FlightMode()
    {
        rr.useGravity = false;
        isFlying = true;
        //get rotation values for the LeftEye
        rotationX = oculusLeftEye.transform.localRotation.x / 2;
        rotationY = oculusLeftEye.transform.localRotation.y * 2; //make fly up/up easier
        rotationZ = oculusLeftEye.transform.localRotation.z;
        
        //put them into a vector
        axis = new Vector3(rotationX, rotationY, rotationZ);

        //Rotate
        transform.Rotate(axis * Time.deltaTime * rotationSpeed);
        
        
        //forward velocity added to the direction to go
        //distance between Eye and Hand
        Vector3 leftDir = oculusLeftHand.transform.position - oculusLeftEye.transform.position;
        Vector3 rightDir = oculusRightHand.transform.position - oculusRightEye.transform.position;
        Vector3 dir = leftDir + rightDir;
        if (dir.y > 0)
            armAclr = dir.y * 0.05f;
        else armAclr = 0f;

        acclr = Mathf.Max(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger), OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)) / 2;


        rr.velocity = oculusLeftEye.transform.forward * forwardSpeed * (acclr + armAclr);//dir.z as weight on forwardSpeed
                                                                                         //rr.velocity = oculusLeftEye.transform.forward * forwardSpeed * dir.z;
    }

    void stopFlight()
    {
        Debug.Log("inside stop flight");
        if (isMoving == true)
        {
            Debug.Log("inside isMoving");
            Debug.Log(rr.velocity);
            Vector3 currentVel = rr.velocity;
            rr.velocity = Vector3.Lerp(rr.velocity, Vector3.zero, .015f);
            Debug.Log(rr.velocity);
            if (rr.velocity.magnitude == 0)
            {
                isMoving = false;
                Debug.Log("0");
            }
        }
    
    }

    void DetectTerrainCollision() {

        //always casting ray
        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, collMask); //collMask is where terrain is on

        if (hits.Length > 0)
        {
            Debug.Log(hitOccured);

            hitOccured = true;

            Debug.Log(hitOccured);

            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            //if collide in terrain, gameover
            //tranport to reg dragon
            transform.position = startObj.transform.position;

        }

    }

    // Use this for initialization
    void Start () {
        rr = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {

        float primaryIndexPressure = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float secondaryIndexPressure = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        if (primaryIndexPressure == 0 && secondaryIndexPressure == 0)
        {
            if (hitOccured == true) //Hit the terrain
            {
                hitOccured = false;
            }
            else if (isFlying == true)
            {
                Debug.Log("inside isFlying");
                isMoving = true;
                Debug.Log(rr.velocity);
                isFlying = false;
                // stopFlight();
            }
        }

        if (primaryIndexPressure > .1 && secondaryIndexPressure > .1 && !hitOccured)
        {
            //always pressing when flying....
            FlightMode();
        }
        else   //when not pressing, stop
        {
            stopFlight();
        }

        //after died, when released index triggers, could fly again, otherwise stay still


        DetectTerrainCollision();
    }
}
