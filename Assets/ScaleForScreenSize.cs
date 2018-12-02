using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleForScreenSize : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        //get current scale
        Vector3 scale = transform.localScale;
        //multiply by screen size differential
        float diff = Screen.width / 1440f;
        Vector3 newScale = new Vector3 (scale.x * diff, scale.y * diff, scale.z);//scale * diff;
        transform.localScale = newScale;
        //Debug.Break();

    }
}
