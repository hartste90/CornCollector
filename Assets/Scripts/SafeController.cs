using UnityEngine;
using UnityEngine.UI;

public class SafeController : MonoBehaviour {


    private int startingHealth = 1;

    public GameObject coinPrefab;
	public GameController gameController;

	private int coinValue;
    private int currentHealth;
    private Animator animator;
	private Image healthBarImage;

	void Start () 
    {
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
            Destroy(gameObject);
        }
        healthBarImage.fillAmount = ((float)currentHealth / (float)startingHealth);
        healthBarImage.color = Color.Lerp(Color.red, Color.green, healthBarImage.fillAmount);
    }
}
