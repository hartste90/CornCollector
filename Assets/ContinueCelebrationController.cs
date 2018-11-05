using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueCelebrationController : MonoBehaviour {

    public CelebrationImageController celebrationImageController;
    private Animator animator;

	void Awake () {
        animator = GetComponent<Animator>();
	}
	
    public void Celebrate()
    {
        animator.SetTrigger("Free-Celebrate");
        celebrationImageController.PlayEnter();
    }

    public void HideImmediate()
    {
        animator.SetTrigger("HideImmediate");
    }

    public void OnCelebrationAnimationFinished()
    {
        celebrationImageController.PlayBounce();
    }

        
}
