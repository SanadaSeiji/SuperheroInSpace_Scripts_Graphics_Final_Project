using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour {

    // Use this for initialization
    public static ArrowManager Instance;
    public GameObject tracked_object;
    private GameObject currentArrow;
    public GameObject arrowPrefab;
    public GameObject arrowPrefab_bow;
    public GameObject stringAttachPoint;
    //public GameObject arrowStartPosition;
    private bool isAttached = true;
    public GameObject stringStartPoint;
    private bool pullInProgress = false;

    private float dist;     //the force applied on the arrow

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    void OnDestroy()
    {
        if (Instance = this)
            Instance = null;
    }

    void Start() {

        //arrowPrefab = gameObject;
    }

    // Update is called once per frame
    void Update() {

        //attachArrow();

        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > .1 && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) == 0)
        {
            if (!pullInProgress)
            {
                pullInProgress = true;
                currentArrow = Instantiate(arrowPrefab, gameObject.transform);
                currentArrow.transform.parent = stringAttachPoint.transform;
                //currentArrow.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            }
            PullString();

        }
        else if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) == 0 && pullInProgress == true) {

            ReleaseString(dist);
            pullInProgress = false;
        }

    }

    public void PullString()
    {
        if (isAttached)
        {
            dist = (tracked_object.transform.position - stringStartPoint.transform.position).magnitude;
            stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3(1 * dist, 0f, 0f);
        }
    }

    public void ReleaseString(float magnitude) {


        //currentArrow = Instantiate(arrowPrefab_bow, gameObject.transform);
        currentArrow.AddComponent<Rigidbody>();
        currentArrow.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        Rigidbody r = currentArrow.GetComponent<Rigidbody>();
        r.velocity = currentArrow.transform.forward * 50f * magnitude;
        r.useGravity = true;

        currentArrow.transform.parent = null;

        stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition;
    }

}
