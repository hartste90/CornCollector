using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JITStorePanelController : MonoBehaviour {

    private Animator animator;
    // Use this for initialization
    void Awake()
    {
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
}
