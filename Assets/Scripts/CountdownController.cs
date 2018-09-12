using UnityEngine;

public class CountdownController : MonoBehaviour 
{
	public GameController gameController;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowCountdown()
    {
        animator.SetTrigger("Show");
    }

	public void HandleCountdownAnimationComplete()
	{
        gameController.HandleCountdownAnimationComplete();
	}
}
