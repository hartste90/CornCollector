using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayPanelController : MonoBehaviour {

    private Animator animator;


    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void HideImmediate()
    {
        animator.SetTrigger("HideImmediate");
    }

    public void Show()
    {
        animator.SetTrigger("Show");
    }

    public void ShowImmediate()
    {
        animator.SetTrigger("ShowImmediate");
    }
}
