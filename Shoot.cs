using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public OVRInput.Controller controller;

    public string buttonName;

    private bool grabbing;

    public float grabRadius;

    private Quaternion lastRotation, currentRotation;

    private GameObject arrow;
    public LayerMask arrowMask;

    public float forwardSpeed = 10f;

    private bool hitTarget = false;
    public float hitRadius;
    public LayerMask targetMask;

    private GameObject hittenObject;

    //grab the arrow
    void GrabObject()
    {
        grabbing = true;

        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, arrowMask);

        if (hits.Length > 0)
        {
            int closestHit = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance < hits[closestHit].distance)
                    closestHit = i;
            }
            arrow = hits[closestHit].transform.gameObject;
            arrow.GetComponent<Rigidbody>().isKinematic = true;
            //grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);

            arrow.transform.position = transform.position;
            arrow.transform.parent = transform;
        }
    }



    void Shooting()
    {
        grabbing = false;

        if (arrow != null)
        {
            arrow.transform.parent = null;
            arrow.GetComponent<Rigidbody>().isKinematic = false;
            arrow.GetComponent<Rigidbody>().angularVelocity = GetAngularVelocity();
            arrow.GetComponent<Rigidbody>().velocity = transform.forward * forwardSpeed;

            //arrow should be flying
            RaycastHit[] hits;

            hits = Physics.SphereCastAll(arrow.transform.position, hitRadius, transform.forward, 0f, targetMask);


            //if hit target: 

            if (hits.Length > 0)
            {
                int closestHit = 0;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].distance < hits[closestHit].distance)
                        closestHit = i;
                }

                hitTarget = true;
                hittenObject = hits[closestHit].transform.gameObject;
                //hittenObject. triot's eyes change color
                //hittenObject.GetComponent<>().color = ;
                //arrow stands on hittenObj, become its child, nolonger fly, no longer obey gravity alnoe
                arrow.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                arrow.GetComponent<Rigidbody>().isKinematic = false;
                arrow.transform.position = hittenObject.transform.position;
                arrow.transform.parent = hittenObject.transform;


            }
        }
    }


    Vector3 GetAngularVelocity()
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (arrow != null)
        {
            lastRotation = currentRotation;
            currentRotation = arrow.transform.rotation;
        }

        if (!grabbing && Input.GetAxis(buttonName) == 1)
            GrabObject();
        if (grabbing && Input.GetAxis(buttonName) < 1)
            Shooting();
    }
}
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public OVRInput.Controller controller;

    public string buttonName;

    private GameObject grabbedObject;
    private bool grabbing;

    public float grabRadius;
    public LayerMask grabMask;

    private Quaternion lastRotation, currentRotation;

    void GrabObject()
    {
        grabbing = true;

        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask);

        if (hits.Length > 0)
        {
            int closestHit = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance < hits[closestHit].distance)
                    closestHit = i;
            }
            grabbedObject = hits[closestHit].transform.gameObject;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            //grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);

            grabbedObject.transform.position = transform.position;
            grabbedObject.transform.parent = transform;
        }
    }

    void DropObject()
    {
        grabbing = false;

        if (grabbedObject != null)
        {
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = GetAngularVelocity();

            Debug.Log(OVRInput.GetLocalControllerVelocity(controller));

            //grabbedObject = null;
        }
    }

    Vector3 GetAngularVelocity()
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (grabbedObject != null)
        {
            lastRotation = currentRotation;
            currentRotation = grabbedObject.transform.rotation;
        }

        if (!grabbing && Input.GetAxis(buttonName) == 1)
            GrabObject();
        if (grabbing && Input.GetAxis(buttonName) < 1)
            DropObject();

    }
}
*/
