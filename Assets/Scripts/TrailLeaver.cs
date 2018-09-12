using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailLeaver : MonoBehaviour {


    public float exhaustFrequency = .1f;
    public bool dropExhaust;
    public GameObject exhaustPrefab;

    private float lastExhaustTime;


    // Use this for initialization
    void Start () {
        lastExhaustTime = Time.time + exhaustFrequency;

    }

    // Update is called once per frame
    void Update () {
        if (dropExhaust)
        {
            CheckExhaust();
        }
    }

    private void CheckExhaust()
    {
        if (Time.time > exhaustFrequency + lastExhaustTime)
        {
            lastExhaustTime = Time.time;
            DropExhaust();
        }
    }

    private void DropExhaust()
    {
        GameObject exhaust = Instantiate(exhaustPrefab, transform.parent);
        exhaust.transform.localPosition = transform.localPosition;
    }
}
