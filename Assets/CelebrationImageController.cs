using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebrationImageController : MonoBehaviour {


	private Animator animator;
	// Use this for initialization
	void Awake () {
        animator = GetComponent<Animator>();
	}
	public void PlayBounce()
    {
        animator.SetTrigger("Bounce");
    }

    public void PlayEnter()
    {
        animator.SetTrigger("Enter");
    }

    public void HideImmediate()
    {
        animator.SetTrigger("HideImmediate");
    }
}
