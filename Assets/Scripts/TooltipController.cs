using UnityEngine;

public class TooltipController : MonoBehaviour {

	public Animator animator;
	void Start () 
    {
		animator = GetComponent<Animator>();
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
        Hide();
	}
}
