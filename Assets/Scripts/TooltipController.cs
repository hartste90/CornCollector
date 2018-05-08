using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipController : MonoBehaviour {

	public Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Show()
	{
	        animator.SetTrigger("show");
	}

	public void Hide()
	{
	       animator.SetTrigger ("hide");
	}

	public void DisableTooltipObject()
	{
	        gameObject.SetActive (false);
	}
}
