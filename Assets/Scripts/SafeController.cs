using UnityEngine;
using UnityEngine.UI;

public class SafeController : MonoBehaviour {


    private int startingHealth = 1;

    public GameObject coinPrefab;
    public Image healthBarImage;

    private int coinValue;
    private int currentHealth;
    private Animator animator;
    private GameController gameController;
    private bool isBusted = false;

    void Start () 
    {
		currentHealth = startingHealth;
		animator = GetComponent<Animator>();
	}

    private void Update()
    {
        if(isBusted)
        {
            Destroy(gameObject);
        }
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
        if (currentHealth <= 0 && !isBusted)
        {
            transform.GetComponent<PolygonCollider2D>().enabled = false;
            gameController.HandleSafeDestroyed(coinValue, transform);
            DestroySelf();
        }
        healthBarImage.fillAmount = ((float)currentHealth / (float)startingHealth);
        healthBarImage.color = Color.Lerp(Color.red, Color.green, healthBarImage.fillAmount);
    }

    public void DestroySelf()
    {
        isBusted = true;

    }
}
