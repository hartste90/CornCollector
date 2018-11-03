using UnityEngine;
using UnityEngine.UI;

public class SafeController : MonoBehaviour {


    private int startingHealth = 1;

    public Image healthBarImage;
    public BoxCollider2D collider;

    private int coinValue;
    private int currentHealth;
    private Animator animator;
    private GameController gameController;
    private bool isBeingDestroyed = false;

    void Start () 
    {
		currentHealth = startingHealth;
		animator = GetComponent<Animator>();
	}

    private void Update()
    {
        if(isBeingDestroyed)
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
        collider.enabled = true;
	}

	public void OnCollisionEnter2D(Collision2D collision)
    {
    	if (collision.gameObject.tag == "Explosion")
    	{
    		HandleHitByExplosion ();
    		collision.gameObject.GetComponent<ExplosionPuffController>().DestroySelf (true);
    	}      
    }

    public void HandleHitByExplosion()
    {
        currentHealth = currentHealth - 1;
        if (currentHealth <= 0 && !isBeingDestroyed)
        {
            collider.enabled = false;
            gameController.HandleSafeDestroyed(coinValue, transform);
            DestroySelf(false);
        }
        healthBarImage.fillAmount = ((float)currentHealth / (float)startingHealth);
        healthBarImage.color = Color.Lerp(Color.red, Color.green, healthBarImage.fillAmount);
    }

    public void DestroySelf(bool immediate)
    {
        gameController.safeList.Remove(gameObject);
        if (immediate)
        {
            Destroy(gameObject);
        }
        else
        {
            isBeingDestroyed = true;
        }
    }
}
