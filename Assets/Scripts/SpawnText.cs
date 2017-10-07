using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnText : MonoBehaviour {

    public TextMesh Text;
    public float DelayBeforeSpawn = 0f;

    private TextMesh textMesh;

    void Awake()
    {
        //Subscribe to pickup and dropping events on this object
        var obj = GetComponent<Valve.VR.InteractionSystem.WermholeObject>();
        obj.onPickUp.AddListener(InvokeSpawn);
        obj.onDetachFromHand.AddListener(RemoveText);

        //set text to transparent by default
        Color c = Text.color;
        c.a = 0f;
        Text.color = c;

    }

    private void InvokeSpawn()
    {
        Invoke("Spawn", DelayBeforeSpawn);
    }

    private void Spawn()
    {
        StartCoroutine("FadeInText");

    }

    IEnumerator FadeInText()
    {
        for (float f = 0f; f <= 1f; f += 0.01f)
        {
            Color c = Text.color;
            c.a = f;
            Text.color = c;
            yield return null;
        }
    }

    IEnumerator FadeOutText()
    {
        for (float f = 1f; f >= 0f; f -= 0.01f)
        {
            Color c = Text.color;
            c.a = f;
            Text.color = c;
            yield return null;
        }
    }

    private void RemoveText()
    {
        StartCoroutine("FadeOutText");
    }
}
