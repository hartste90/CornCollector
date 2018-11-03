using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundOverlayController : MonoBehaviour {

	private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }
    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }
    public void HideImmediate()
    {
        animator.SetTrigger("HideImmediate");
    }
}
