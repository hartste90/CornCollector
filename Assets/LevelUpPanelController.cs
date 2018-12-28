using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanelController : MonoBehaviour {

    private Animator animator;
    public Text levelText;

    public delegate void panelAnimationCompleteCallback();
    public panelAnimationCompleteCallback panelCompleteAnimationCallback;

	void Start () {
        animator = GetComponent<Animator>();
	}

    public void Show(int level)
    {
        levelText.text = "Level " + level.ToString();
        animator.SetTrigger("Show");
    }
	

    public void handlePanelAnimationComplete()
    {
        panelCompleteAnimationCallback();
    }
}
