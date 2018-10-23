using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreButtonController : MonoBehaviour {


	private Button button;
	private Animator animator;
	// Use this for initialization
	void Awake () 
    {
        button = GetComponent<Button>();
        animator = GetComponent<Animator>();
	}
	
    public void Show()
    {
        animator.SetTrigger("Show");
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }

    public void HideImmediate()
    {
        animator.SetTrigger("HideImmediate");
    }

    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
    }
}
