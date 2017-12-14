using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {



    public float hitRadius = 0f;
    public LayerMask grabMask;
    private GameObject hitObj;
    private bool hit = false;


    // Use this for initialization
    void Start () {
        // gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {

        //gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);


        if (gameObject.GetComponent<Rigidbody>() && hit == false) {

            // gameObject.transform.forward = Vector3.Slerp(gameObject.transform.forward, gameObject.GetComponent<Rigidbody>().velocity.normalized, Time.deltaTime);
            gameObject.transform.RotateAround(transform.position, -Vector3.up,Time.deltaTime*100);
            gameObject.transform.rotation = Quaternion.LookRotation(gameObject.GetComponent<Rigidbody>().velocity);

        }
        //Object.Destroy(gameObject, 5.0f);

        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, hitRadius, transform.forward, 0f, grabMask);

        if (hits.Length > 0)
        {
            int closestHit = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance < hits[closestHit].distance)
                    closestHit = i;
            }
            hitObj = hits[closestHit].transform.gameObject;
            gameObject.GetComponent<Rigidbody>().useGravity= false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (0f, 0f, 0f);

            hit = true;

       

            

            //grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);

            gameObject.transform.position = hitObj.transform.position;
            gameObject.transform.parent = hitObj.transform;
        }


    }

    void AttachArrow()
    {
        //ArrowManager.Instance.AttachBowToArrow();
    }
}
