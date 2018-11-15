using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsUpdateTextController : MonoBehaviour {

    private Text updateText;
    private Animator animator;

	// Use this for initialization
	void Awake () {
        animator = GetComponent<Animator>();
        updateText = GetComponent<Text>();
	}
	
    public void Show(int amount)
    {
        updateText.text = "+ " + amount.ToString();
        animator.SetTrigger("Show");
    }
}
