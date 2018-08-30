using UnityEngine;

public class MineController : MonoBehaviour 
{
	public GameObject explosionPrefab;
    public GameController gameController;
    private bool isBeingDestroyed = false;


    private void Update()
    {
        if (isBeingDestroyed)
        {
            Destroy(gameObject);
        }
    }

    public void MineExplode()
	{
        GameObject explosionObject = Instantiate(explosionPrefab, transform.parent);
        explosionObject.GetComponent<ExplosionController>().gameController = gameController;
        explosionObject.transform.localPosition = transform.localPosition;
        DestroySelf(true);
	}

    public void DestroySelf(bool immediate)
    {
        gameController.mineList.Remove(gameObject);
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
