using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailLeaver : MonoBehaviour {


    public float exhaustFrequency = .1f;
    public bool isDroppingExhaust;
    public GameObject exhaustPrefab;

    private float lastExhaustTime;


    // Use this for initialization
    void Start () {
        lastExhaustTime = Time.time + exhaustFrequency;

    }

    // Update is called once per frame
    void Update () {
        if (isDroppingExhaust)
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

    public void DropExhaust()
    {
        GameObject exhaust = Instantiate(exhaustPrefab, transform.parent.parent);
        exhaust.transform.localPosition = transform.parent.transform.localPosition;
    }
}
