﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour {

    public float DistanceThreshold = 1f;
    public float ScaleAmount = 2f;
    public float DelayBeforeShrink = 1f;
    private Transform camPostion;
    private float initScale;
    private bool canGrow = false;
    private bool pickedUpOnce = false;

    void Awake()
    {
        var obj = GetComponent<Valve.VR.InteractionSystem.WermholeObject>();
        obj.onPickUp.AddListener(InitializeGrow);
        obj.onDetachFromHand.AddListener(StopGrowing);

        camPostion = Camera.main.gameObject.transform;

    }
	
	// Update is called once per frame
	void Update () {

        if (canGrow) //Only able to grow when it has been grabbed
        {
            float distToFace = Vector3.Distance(this.transform.position, camPostion.position);
            if (distToFace < DistanceThreshold)
            {
                float t = 1f - normalize(distToFace, DistanceThreshold, 0.3f);
                this.transform.localScale = Vector3.one * Mathf.Lerp(initScale, 2f * initScale, t);
            }
            else
            {
                this.transform.localScale = Vector3.one * initScale;

            }

        }


    }

    private void InitializeGrow()
    {
        if (!pickedUpOnce) //DIRTTY like ur mom
        {
            initScale = this.transform.localScale.x;
            pickedUpOnce = true;
        }


        canGrow = true;
    }

    private void StopGrowing()
    {
        canGrow = false;
        Invoke("InvokeShrink", DelayBeforeShrink);
    }

    private void InvokeShrink()
    {
        StartCoroutine("ReturnToInitialScale", 1f);
    }

    IEnumerator ReturnToInitialScale(float time)
    {
        float elapsedTime = 0f;
        float bigScale = this.transform.localScale.x;
        
        while (elapsedTime < time)
        {
            float currScale = Mathf.Lerp(bigScale, initScale, elapsedTime / time);
            this.transform.localScale = Vector3.one * currScale;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

    }

    private float normalize(float val, float max, float min)
    {
        return (val - min) / (max - min);
    }
}