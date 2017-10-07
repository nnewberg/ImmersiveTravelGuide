using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour {

    public float Scale = 0.01f;
    public float Speed = 1f;
    public bool IsTranslating = true;
    public bool IsRotating, IsScaling = false;
    private bool isFrozen = false;

    void Awake()
    {
        Valve.VR.InteractionSystem.WermholeObject obj = GetComponent<Valve.VR.InteractionSystem.WermholeObject>();

        if (obj) //TODO: don't let non-wermhole objects have this.
        {
            obj.onPickUp.AddListener(StopOscillation); //Stop oscillating if we pick the object up
            obj.onReturn.AddListener(StartOscillation);
        }

    }
	// Update is called once per frame
	void Update () {

        if (!isFrozen)
        {
            if (IsTranslating)
                this.transform.position += Mathf.Sin(Time.time * Speed) * Scale * Time.deltaTime * Vector3.up;
            if (IsRotating)
                this.transform.Rotate(Mathf.Sin(Time.time * Speed) * Scale * Time.deltaTime * Vector3.forward);
        }
		
	}

    private void StopOscillation()
    {
        isFrozen = true;
    }

    private void StartOscillation()
    {
        isFrozen = false;
    }
}
