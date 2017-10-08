using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnText : MonoBehaviour {

    public TextMesh Text;
    public float DelayBeforeFadeIn = 1f;
    public float DelayBeforeFadeOut = 0f;

    private Material textMat;
    private bool isTextShown = false;

    void Awake()
    {
        //Subscribe to pickup and dropping events on this object
        var obj = GetComponent<Valve.VR.InteractionSystem.WermholeObject>();
        obj.onPickUp.AddListener(DelaySpawn);
        obj.onDetachFromHand.AddListener(DelayRemoval);


        textMat = Text.GetComponent<Renderer>().material;
        //set text to transparent by default
        Color c = textMat.color;
        c.a = 0f;
        textMat.color = c;



    }

    private void DelaySpawn()
    {
        if (!isTextShown) //only fade in the text when it's completely hidden
        {
            Invoke("Spawn", DelayBeforeFadeIn);
        }else //if it's already shown, kill any fade out processes
        {
            //want to stop the fade out process
            CancelInvoke("RemoveText");
        }
    }

    private void Spawn()
    {
        StartCoroutine("FadeInText");

    }

    IEnumerator FadeInText()
    {
        for (float f = 0f; f <= 1f; f += 0.01f)
        {
            Color c = textMat.color;
            c.a = f;
            textMat.color = c;
            yield return null;
        }

        isTextShown = true;
    }

    IEnumerator FadeOutText()
    {
        for (float f = 1f; f >= 0f; f -= 0.01f)
        {
            Color c = textMat.color;
            c.a = f;
            textMat.color = c;
            yield return null;
        }

        isTextShown = false;
    }

    private void DelayRemoval()
    {
        if (isTextShown)
        {
            Invoke("RemoveText", DelayBeforeFadeOut);

        }else
        {
            CancelInvoke("Spawn");
        }
    }

    private void RemoveText()
    {
        StartCoroutine("FadeOutText");
    }
}
