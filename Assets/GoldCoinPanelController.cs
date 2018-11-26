using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinPanelController : MonoBehaviour {

    public delegate void HideAnimationCompleteCallback();
    public HideAnimationCompleteCallback hideCompleteCallback;


    public void HandleHideAnimationComplete()
    {
        if (hideCompleteCallback != null)
        {
            hideCompleteCallback();
            hideCompleteCallback = null;
        }
    }
}
