

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelController : MonoBehaviour {

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
