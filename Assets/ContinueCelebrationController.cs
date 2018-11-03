using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueCelebrationController : MonoBehaviour {

    public EndgameScreenController endgameScreenController;
    public CelebrationImageController celebrationImageController;
    private Animator animator;

	// Use this for initialization
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
        //endgameScreenController.OnContinueGame();
        celebrationImageController.PlayBounce();
    }

        
}
