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
	void Start () {
		keyCostText.text = keyCost.ToString ();
		currentHealth = startingHealth;
		animator = GetComponent<Animator>();
	}

	public void Init (GameController gameController, int startingHealth = 1, int coinValue = 1)
	{
	        this.startingHealth = startingHealth;
	        this.coinValue = coinValue;
	        this.gameController = gameController;
	}

	public void HandleAppearAnimationComplete()
	{
	        //Debug.Log ("handleAppearAnimationComplete");
		GetComponent <PolygonCollider2D>().enabled = true;
	}

	public void OnCollisionEnter2D(Collision2D collision)
    {
    	if (collision.gameObject.tag == "Explosion")
    	{
    		HandleHitByExplosion ();
    		collision.gameObject.GetComponent<ExplosionPuffController>().DestroySelf ();
    	}      
    }

    public void HandleHitByExplosion()
    {
        currentHealth = currentHealth - 1;
        if (currentHealth <= 0)
        {
            transform.GetComponent<PolygonCollider2D>().enabled = false;
            gameController.HandleSafeDestroyed(coinValue, transform);
            //TODO: create explosion
            Destroy(gameObject);
        }
        healthBarImage.fillAmount = ((float)currentHealth / (float)startingHealth);
        healthBarImage.color = Color.Lerp(Color.red, Color.green, healthBarImage.fillAmount);
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
