using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueCelebrationController : MonoBehaviour {

    public EndgameScreenController endgameScreenController;
    private Animator animator;

	// Use this for initialization
	void Awake () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Celebrate()
    {
        animator.SetTrigger("Free-Celebrate");
    }

    public void HideImmediate()
    {
        animator.SetTrigger("HideImmediate");
    }

    public void OnCelebrationAnimationFinished()
    {
        endgameScreenController.OnContinueGame();
    }

        
}
