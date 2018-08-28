using UnityEngine;

public class CountdownController : MonoBehaviour 
{
	public GameController gameController;

	public void HandleCountdownAnimationComplete()
	{
        gameController.HandleCountdownAnimationComplete();
	}
}
