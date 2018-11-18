using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentLevelPanelController : MonoBehaviour {

    public Text levelText;

    private Animator animator;
    
	void Start () {
        animator = GetComponent<Animator>();
	}


    public void Show(int level)
    {
        levelText.text = "Level " + level;
        animator.SetTrigger("Show");
    }

    public void Hide()
    {
        animator.SetTrigger("Hide");
    }
}
