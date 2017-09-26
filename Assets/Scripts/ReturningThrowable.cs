using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class ReturningThrowable : Throwable
    {
        public AnimationCurve ReturnAnimationCurve; //movement curve for the return
        private float returnSpeed = 1f;
        private float animTimer = 0f; //keep track of the time of our animation
        private bool isReturning = false;
        private Vector3 releasedPosition, returnedPosition;
        private Quaternion releasedRotation, returnedRotation;


        void Awake()
        {
            onDetachFromHand.AddListener(StartReturnTimer);
            onPickUp.AddListener(StopReturning);
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
                float t = ReturnAnimationCurve.Evaluate(animTimer) * returnSpeed;
                Debug.Log(t);
                this.transform.position = Vector3.Lerp(releasedPosition, returnedPosition, t);
                this.transform.rotation = Quaternion.Slerp(releasedRotation, returnedRotation, t);
                animTimer += Time.deltaTime;

                if (t == 1f) //reached the goal
                    StopReturning();
            }
        }

        private void StartReturnTimer()
        {
            Invoke("StartReturn", 5f);
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

}
