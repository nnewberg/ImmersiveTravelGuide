using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour {

    public GameObject[] Objects;
    public float DelayBeforeSpawn = 1f;
    public float DelayBeforeRetract = 10f;
    public AnimationCurve SpawnAnimationCurve;
    private Vector3[] initialObjectScales; //array that stores each object's initial scale
    private bool isSpawned = false;

    void Awake()
    {
        var obj = GetComponent<Valve.VR.InteractionSystem.WermholeObject>();
        obj.onPickUp.AddListener(DelaySpawn);

        initialObjectScales = new Vector3[Objects.Length]; //initialize initial object scales
        for (int i = 0; i < Objects.Length; i++)
        {
            initialObjectScales[i] = Objects[i].transform.localScale;
        }

    }

    private void DelaySpawn()
    {
        if (!isSpawned)
        {
            Invoke("StartSpawn", DelayBeforeSpawn);
        }
    }

    private void StartSpawn()
    {
        foreach(GameObject go in Objects)
        {
            go.SetActive(true);
        }

        StartCoroutine("Spawn", 1f);

        Invoke("RetractSpawn", DelayBeforeRetract);

    }

    private void RetractSpawn()
    {
        if(isSpawned)
            StartCoroutine("ReverseSpawn", 1f);
    }

    IEnumerator Spawn(float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            for (int i = 0; i < Objects.Length; i++) { 
               Objects[i].transform.localScale = initialObjectScales[i] * SpawnAnimationCurve.Evaluate(elapsedTime);
            }

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        isSpawned = true;
    }

    IEnumerator ReverseSpawn(float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            for (int i = 0; i < Objects.Length; i++)
            {
                Objects[i].transform.localScale = initialObjectScales[i] * SpawnAnimationCurve.Evaluate(1f - elapsedTime);
            }

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        isSpawned = false;
    }

}
