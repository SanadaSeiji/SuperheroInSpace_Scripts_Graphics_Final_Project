using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabBow : MonoBehaviour {

    bool hasBow = false;

    public GameObject leftBow;
    public GameObject rightBow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            if (hasBow == false)
            {
                hasBow = true;
                leftBow.SetActive(true);
            }
            else
            {
                hasBow = false;
                leftBow.SetActive(false);
            }
        }
        else if (OVRInput.GetDown(OVRInput.Button.Two)) {

            if (hasBow == false)
            {
                hasBow = true;
                rightBow.SetActive(true);
            }
            else
            {
                hasBow = false;
                rightBow.SetActive(false);
            }
        }

    }
}
