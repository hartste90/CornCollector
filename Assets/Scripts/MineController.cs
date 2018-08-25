using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineController : MonoBehaviour {

	public GameObject explosionPrefab;
	public Text countDownLabel;
	public int countDownNumber = 3;

	public Animator mineAnimator;
	public Animator countdownAnimator;
	// Use this for initialization
	void Start()
	{
		//StartCoroutine( ActivateAfterSeconds (1));
	}

	IEnumerator ActivateAfterSeconds(int waitTime) 
	{
        yield return new WaitForSeconds(waitTime);
        GetComponent <PolygonCollider2D>().enabled = true;
		UpdateCountdownLabel (countDownNumber);
		StartCoroutine (ReduceCountdown (1));
		mineAnimator.SetTrigger ("TurnRed");

	}
	IEnumerator ReduceCountdown(int timerSpeed) 
	{
	        yield return new WaitForSeconds(timerSpeed);
		CountdownTick ();
//		countdownAnimator.SetTrigger ("CTA");
		if (countDownNumber == 3)
		{
		        mineAnimator.SetTrigger ("TurnRed");
		}
		if (countDownNumber <= 0)
		{
            MineExplode();
		}
		StartCoroutine (ReduceCountdown (1));
	}

	public void CountdownTick()
	{
	        countDownNumber --;
		UpdateCountdownLabel (countDownNumber);
	}

	public void UpdateCountdownLabel(int num)
	{
	        countDownLabel.text = num+"";
	}

	public void MineExplode()
	{
	        GameObject explosionObject = Instantiate(explosionPrefab, transform.parent);
		explosionObject.transform.localPosition = transform.localPosition;
	        Destroy(gameObject);
	}

}
