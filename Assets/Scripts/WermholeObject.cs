using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
    public class WermholeObject : Throwable
    {
        [HideInInspector]
        public UnityEvent onReturn;
        [HideInInspector]
        public UnityEvent onGazeTrigger, onGazeExit;
    }

}
