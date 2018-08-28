using UnityEngine;

public class ExhaustController : MonoBehaviour {

    public float shrinkRate;
    private float startingScale;
    
	void Start () 
    {
        startingScale = transform.localScale.x;
	}
	
	void Update () 
    {
        if (transform.localScale.x <= startingScale / 10)
        {
            Destroy(gameObject);
        }
        transform.localScale *= shrinkRate;
	}
}
