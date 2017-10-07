using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : MonoBehaviour {

    public float ReturnSpeed = 1f;
    public AnimationCurve ReturnAnimationCurve; //movement curve for the return
    public float DelayBeforeReturn = 5f;
    private float animTimer = 0f; //keep track of the time of our animation

    private Transform originalParent;
    private bool isReturning = false;
    private Vector3 releasedPosition, returnedPosition;
    private Quaternion releasedRotation, returnedRotation;


    void Awake()
    {
        var obj = GetComponent<Valve.VR.InteractionSystem.WermholeObject>(); 
        obj.onDetachFromHand.AddListener(StartReturnTimer);
        obj.onPickUp.AddListener(StopReturning);

        originalParent = this.transform.parent;

    }

    // Use this for initialization
    void Start()
    {
        returnedPosition = this.transform.position;
        returnedRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReturning)
        {
            float t = ReturnAnimationCurve.Evaluate(animTimer);
            //Debug.Log(t);
            this.transform.position = Vector3.Lerp(releasedPosition, returnedPosition, t);
            this.transform.rotation = Quaternion.Slerp(releasedRotation, returnedRotation, t);
            animTimer += Time.deltaTime * ReturnSpeed;

            if (t == 1f)//reached the return goal
            {
                RevertToOriginalState();
            }

        }
    }


    private void RevertToOriginalState()
    {
        StopReturning();
        this.transform.parent = originalParent;
        var obj = GetComponent<Valve.VR.InteractionSystem.WermholeObject>();
        obj.onReturn.Invoke();
    }

    private void StartReturnTimer()
    {
        Invoke("StartReturn", DelayBeforeReturn);
    }

    private void StartReturn() //return this object back to it's original location
    {
        releasedPosition = this.transform.position;
        releasedRotation = this.transform.rotation;
        this.GetComponent<Rigidbody>().isKinematic = true;
        isReturning = true;
    }

    private void StopReturning() //reverts object back to its original state
    {
        isReturning = false;
        animTimer = 0f;
        CancelInvoke("StartReturn");//cancel the return in-case we picked up the object again after we let it go
    }
}

