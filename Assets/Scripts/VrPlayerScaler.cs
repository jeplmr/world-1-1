using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrPlayerScaler : MonoBehaviour
{

    public float scale;

    public Camera centerEye; 

    private float lastMeasuredDistance;
    private Vector3 lastMeasuredPositionLeft;
    private Vector3 lastMeasuredPositionRight;
    private Vector3 averagedControllerPosition;
    private float lastMeasuredDistanceFromHead; 

    private void Start()
    {
        transform.localScale = new Vector3(scale, scale, scale); 
    }


    // Update is called once per frame
    void Update()
    {
        //transform.localScale = Vector3.one * scale;
        //Debug.Log(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger));
        Scale();
        Move(); 
    }

    private void LateUpdate()
    {
        GetLastControllerDistance();
        GetLastControllerPositionsDistance(); 
    }


    void Scale()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) >= 0.75f &&
            OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.75f)
        {
            float currentDistance = Vector3.Distance(
                OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch),
                OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));

            float difference = lastMeasuredDistance - currentDistance;

            transform.localScale = transform.localScale += new Vector3(difference, difference, difference);

            if (transform.localScale.x <= 1f ||
                transform.localScale.y <= 1f ||
                transform.localScale.z <= 1f)
            {
                transform.localScale = Vector3.one; 
            }

        }
    }

    void Move()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) >= 0.75f &&
            OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.75f)
        {
            Vector3 currentPositionLeft = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch); 
            Vector3 currentPositionRight = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            Vector3 currentAverage = currentPositionLeft + currentPositionRight / 2;

            float currentDistanceFromHead = Vector3.Distance(currentAverage, centerEye.transform.position); 

            float difference = lastMeasuredDistanceFromHead - currentDistanceFromHead;

            transform.position += difference * Vector3.one; 
        }
    }


    void GetLastControllerDistance()
    {
        lastMeasuredDistance = Vector3.Distance(
                OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch),
                OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));
    }

    void GetLastControllerPositionsDistance()
    {
        lastMeasuredPositionLeft = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        lastMeasuredPositionRight = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        averagedControllerPosition = (lastMeasuredPositionLeft + lastMeasuredPositionRight) / 2;
        lastMeasuredDistanceFromHead = Vector3.Distance(averagedControllerPosition, centerEye.transform.position); 
    }


}
