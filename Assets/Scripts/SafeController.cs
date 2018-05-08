using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeController : MonoBehaviour {

	public int startingHealth;
	public int currentHealth;

	public GameObject coinPrefab;

	public GameController gameController;

	public int coinValue;

	public Text keyCostText;
	public int keyCost;



	public Animator animator;
	public Image healthBarImage;
	// Use this for initialization
	void Start () {
		keyCostText.text = keyCost.ToString ();
		currentHealth = startingHealth;
		animator = GetComponent<Animator>();
	}

	public void init (int startingHealth, int coinValue, int keyCost, GameController gameController)
	{
	        this.startingHealth = startingHealth;
	        this.coinValue = coinValue;
	        this.keyCost = keyCost;
	        this.gameController = gameController;
	}

	public void handleAppearAnimationComplete()
	{
	        Debug.Log ("handleAppearAnimationComplete");
		GetComponent <PolygonCollider2D>().enabled = true;
	}

	public void OnCollisionEnter2D(Collision2D collision)
         {
		if (collision.gameObject.tag == "Explosion")
		{
			handleHitByExplosion ();
			collision.gameObject.GetComponent<ExplosionPuffController>().DestroySelf ();
		}
		else if (collision.gameObject.tag == "Player")
		{
		        Debug.Log ("Safe was hit by player!");
		}
               
         }

	public void handleHitByExplosion()
	{
	        currentHealth = currentHealth - 1;
	        if (currentHealth <= 0)
	        {
			transform.GetComponent<PolygonCollider2D>().enabled = false;
	                gameController.HandleSafeDestroyed (coinValue, transform);
	                //TODO: create explosion
	                Destroy(gameObject);
	        }
	        healthBarImage.fillAmount = ((float)currentHealth/(float)startingHealth);
		healthBarImage.color = Color.Lerp(Color.red, Color.green,healthBarImage.fillAmount );
	}

	public void GenerateCoins (int numCoins)
	{
	        for (int i = 0; i < numCoins; i++)
	        {
	                Debug.Log("Generating coin");
	                GameObject coin = Instantiate (coinPrefab, transform.parent);
	                coin.transform.localScale = new Vector3 (384, 384, 1);
	                coin.transform.localPosition = transform.localPosition;
	                coin.GetComponent<Rigidbody2D>().velocity = GetRandom2DDirection();
	                gameController.coinList.Add (coin);
	        }
	}

	public Vector2 GetRandom2DDirection()
	{
		float x = Random.Range(-1f, 1f);
		float y = Random.Range(-1f, 1f);
		Vector2 direction = new Vector2 (x, y);
		//if you need the vector to have a specific length:
		float coinSpeed = Random.Range (1, 3);
		return (direction.normalized * coinSpeed);
	}
}
