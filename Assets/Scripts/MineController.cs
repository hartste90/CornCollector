using UnityEngine;

public class MineController : MonoBehaviour 
{
	public GameObject explosionPrefab;	

	public void MineExplode()
	{
        GameObject explosionObject = Instantiate(explosionPrefab, transform.parent);
        explosionObject.transform.localPosition = transform.localPosition;
        Destroy(gameObject);
	}

}
