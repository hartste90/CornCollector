using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePanelController : MonoBehaviour {

    private CanvasGroup canvasGroup;
    private Animator animator;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
    }

    public void EnablePanelInput()
    {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void DisablePanelInput()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

    }

    public void Show()
    {
        animator.SetTrigger("Show");

    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }
}
