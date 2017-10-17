using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerAnimation : MonoBehaviour {

    public Animation Anim;
    public EventAnimMap EventAnimMap;

    void Awake()
    {
        ////Subscribe to pickup and dropping events on this object
        var obj = GetComponent<Valve.VR.InteractionSystem.WermholeObject>();

        foreach (var entry in EventAnimMap.EventAnimations){

            if (entry.Event == EventAnimMap.EventType.OnPickUp)
            {
                obj.onPickUp.AddListener(() => {PlayAnimation(entry.Animation); });
            }
            if (entry.Event == EventAnimMap.EventType.OnDetachFromHand)
            {
                obj.onDetachFromHand.AddListener(() => { PlayAnimation(entry.Animation); });
            }
        }




        //Anim.clip = AnimationClip;

    }

    private void PlayAnimation(AnimationClip clip)
    {
        Anim.clip = clip;
        Anim.Play();
    }


}
