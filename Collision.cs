using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

    public float grabRadius = 0;
    public GameObject startObj;
    public LayerMask collMask;

    public float hitTime;
    public bool hitOccured = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //always casting ray
        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, collMask); //collMask is where terrain is on

        if (hits.Length > 0)
        {
            Debug.Log("Collision detected");

            hitTime = Time.time;
            hitOccured = true;
            int closestHit = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance < hits[closestHit].distance)
                    closestHit = i;
            }
            
            
            GetComponent<Rigidbody>().velocity = new Vector3 (0f, 0f, 0f);
            //if collide in terrain, gameover
            //tranport to reg dragon
            transform.position = startObj.transform.position;
           
        }

    }
}
