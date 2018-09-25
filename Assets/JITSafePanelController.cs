using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JITSafePanelController : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Awake () {
        animator = GetComponent<Animator>();
	}
	
    public void Show()
    {
        animator.SetTrigger("Show");
    }

    public void Hide (bool shouldHideImmediately = false)
    {
        if (shouldHideImmediately)
        {
            animator.SetTrigger("HideImmediate");
        }
        else
        {
            animator.SetTrigger("Hide");
        }
    }


}
