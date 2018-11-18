using UnityEngine;

public class MineController : MonoBehaviour 
{
	public GameObject explosionPrefab;
    public GameController gameController;
    private bool isBeingDestroyed = false;

    public GameObject explosionPuffPrefab;
    public GameObject[] explosionPuffObjectList;
    public float explosionStrength;

    private void Update()
    {
        if (isBeingDestroyed)
        {
            Destroy(gameObject);
        }
    }

    public void MineExplode()
	{
        //instantiate explosionPuffs
        explosionPuffObjectList = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            GameObject explosionPuffObject = Instantiate(explosionPuffPrefab, transform.parent, true);
            ExplosionPuffController puffCtr = explosionPuffObject.GetComponent<ExplosionPuffController>();
            puffCtr.gameController = gameController;
            gameController.explosionPuffList.Add(explosionPuffObject);
            explosionPuffObject.transform.localPosition = transform.localPosition;
            explosionPuffObject.transform.localScale = Vector3.one;
            explosionPuffObjectList[i] = explosionPuffObject;
            explosionPuffObjectList[i].GetComponent<Rigidbody2D>().AddTorque(Random.Range(-1f, 1f), ForceMode2D.Impulse);

        }

        explosionPuffObjectList[0].GetComponent<Rigidbody2D>().AddForce((transform.rotation * Vector3.right).normalized * explosionStrength, ForceMode2D.Force);
        explosionPuffObjectList[0].GetComponent<Transform>().Rotate(new Vector3(0, 0, -90));
        explosionPuffObjectList[1].GetComponent<Rigidbody2D>().AddForce((transform.rotation * Vector3.up).normalized * explosionStrength, ForceMode2D.Force);
        explosionPuffObjectList[2].GetComponent<Rigidbody2D>().AddForce((transform.rotation * Vector3.left).normalized * explosionStrength, ForceMode2D.Force);
        explosionPuffObjectList[2].GetComponent<Transform>().Rotate(new Vector3(0, 0, 90));
        explosionPuffObjectList[3].GetComponent<Rigidbody2D>().AddForce((transform.rotation * Vector3.down).normalized * explosionStrength, ForceMode2D.Force);
        explosionPuffObjectList[3].GetComponent<Transform>().Rotate(new Vector3(0, 0, 180));


        //GameObject explosionObject = Instantiate(explosionPrefab, transform.parent);
        //explosionObject.GetComponent<ExplosionController>().gameController = gameController;
        //explosionObject.transform.localPosition = transform.localPosition;
        DestroySelf(true);
	}

    public void DestroySelf(bool immediate)
    {
        gameController.mineList.Remove(this);
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
