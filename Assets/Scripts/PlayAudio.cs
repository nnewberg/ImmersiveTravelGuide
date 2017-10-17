using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour {

    public AudioClip Clip;
    public AudioSource Source;
    public float DelayBeforePlay = 1f;
    public float DelayBeforeStop = 0f;

    void Awake()
    {
        var obj = GetComponent<Valve.VR.InteractionSystem.WermholeObject>();
        obj.onPickUp.AddListener(DelayPlay);
        obj.onDetachFromHand.AddListener(DelayStop);

    }

    private void DelayPlay()
    {
        Source.clip = Clip;
        Invoke("Play", DelayBeforePlay);
    }

    private void Play()
    {
        Source.Play();
        StartCoroutine("FadeInAudio");
    }

    IEnumerator FadeInAudio()
    {
        for (float f = 0f; f <= 1f; f += 0.01f)
        {
            Source.volume = f;
            yield return null;
        }

    }

    private void DelayStop()
    {
        Invoke("Stop", DelayBeforeStop);
    }

    private void Stop()
    {
        StartCoroutine("FadeOutAudio");
    }

    IEnumerator FadeOutAudio()
    {
        for (float f = 1f; f >= 0f; f -= 0.01f)
        {
            Source.volume = f;
            yield return null;
        }

        Source.Stop();
        Source.volume = 1f;
    }

}
