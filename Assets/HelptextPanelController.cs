using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelptextPanelController : MonoBehaviour {

    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}

    public void ShowHelpText()
    {
        animator.SetTrigger("Show");
    }
	
}
