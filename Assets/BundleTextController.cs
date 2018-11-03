using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BundleTextController : MonoBehaviour {

	private Text bundleText;
	// Use this for initialization
	void Start () {
        bundleText = GetComponent<Text>();
        bundleText.text = "Version:  " + Application.version;
	}
}
