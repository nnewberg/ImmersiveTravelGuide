using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour {

    public float Scale = 0.01f;
    public float Speed = 1f;
    public bool IsTranslating = true;
    public bool IsRotating, IsScaling = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(IsTranslating)
            this.transform.position += Mathf.Sin(Time.time * Speed) * Scale  * Time.deltaTime * Vector3.up;
        if (IsRotating)
            this.transform.Rotate(Mathf.Sin(Time.time * Speed)* Scale * Time.deltaTime * Vector3.forward);
		
	}
}
