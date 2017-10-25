using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeTrigger : MonoBehaviour {

    private float durationToTrigger = 1f;
    private float currGazeDuration = 0f;
    private int layerMask = 1 << 8; //only raycast check on WermholeObjects
    private GameObject currObject;
    private bool isFocused = false;

    void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if(currGazeDuration > durationToTrigger && !isFocused) //we looked at object long enough to trigger
        {
            currGazeDuration = 0f;
            currObject.GetComponent<Valve.VR.InteractionSystem.WermholeObject>().onGazeTrigger.Invoke();

            isFocused = true;
        }

        if (Physics.Raycast(transform.position, fwd, out hit, 10f, layerMask))
        {
            if(!GameObject.ReferenceEquals(currObject, hit.collider.gameObject)) //new object we are looking at
            {
                currGazeDuration = 0f;
                currObject = hit.collider.gameObject;
            }
            else if (!isFocused) //continue gazing at the object
            {
                currGazeDuration += Time.deltaTime;
            }

        }else //looked away
        {
            currGazeDuration = 0f;
            if (currObject)
            {
                currObject.GetComponent<Valve.VR.InteractionSystem.WermholeObject>().onGazeExit.Invoke();
                currObject = null;
                isFocused = false;
            }
        }
            
    }
}
