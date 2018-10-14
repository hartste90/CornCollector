using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePanelController : MonoBehaviour {

    public CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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
}
