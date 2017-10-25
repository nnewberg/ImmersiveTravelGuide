using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGazeTrigger : MonoBehaviour {

    void Awake()
    {
        var obj = GetComponent<Valve.VR.InteractionSystem.WermholeObject>();
        obj.onGazeTrigger.AddListener(changeColor);
        obj.onGazeExit.AddListener(revertColor);
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void changeColor()
    {
        GetComponent<Renderer>().material.SetColor("_Color",Color.blue);
    }

    private void revertColor()
    {
        GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }

}
