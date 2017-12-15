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
       
    }
	
	// Update is called once per frame
	void Update () {

        if (gameObject.GetComponent<Rigidbody>() && hit == false) {
            //gameObject.transform.RotateAround(transform.position, -Vector3.up,Time.deltaTime*100);
            gameObject.transform.rotation = Quaternion.LookRotation(gameObject.GetComponent<Rigidbody>().velocity);

        }
    }

    void AttachArrow()
    {
        //ArrowManager.Instance.AttachBowToArrow();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.gameObject.GetComponent<Arrow>())
        {
            
            Debug.Log("asd");
            if (GetComponent<Rigidbody>())
            {
                gameObject.GetComponent<Rigidbody>().useGravity = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            hit = true;
            hitObj = collider.gameObject;
            gameObject.transform.parent = hitObj.transform;
        }
    }
}
