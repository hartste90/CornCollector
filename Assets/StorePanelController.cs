using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePanelController : MonoBehaviour {


    public RemoveAdsButtonController removeAdsController;

    private CanvasGroup canvasGroup;
    private Animator animator;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
    }

    public void HandleRemoveAdsPurchased()
    {
        removeAdsController.RemoveAdsPurchased();
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

    public void EnterRight()
    {
        animator.SetTrigger("EnterRight");
    }
    public void ExitRight()
    {
        animator.SetTrigger("ExitRight");
    }
    public void HideImmediate()
    {
        animator.SetTrigger("HideImmediate");
    }
    public void ShowImmediate()
    {
        animator.SetTrigger("ShowImmediate");
    }
}
