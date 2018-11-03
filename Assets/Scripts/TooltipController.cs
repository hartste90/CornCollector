using UnityEngine;

public class TooltipController : MonoBehaviour {

	public Animator animator;
    private bool tooltipIsVisible = false;
	void Start () 
    {
		animator = GetComponent<Animator>();
	}

	public void Show()
	{
        if (!tooltipIsVisible)
        {
            animator.SetTrigger("show");
            tooltipIsVisible = true;
        }

	}

	public void Hide()
    {
        if (tooltipIsVisible)
        {
            animator.SetTrigger("hide");
            tooltipIsVisible = false;
        }
       
	}

	public void DisableTooltipObject()
	{
        Hide();
	}
}
