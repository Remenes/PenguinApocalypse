using UnityEngine;

public class Selfdestruct : MonoBehaviour
{
    public float lifeTime;
    public bool destroyOnStart;
	
	private void Start ()
    {
        Destruct();
	}
	
	
	public void Destruct ()
    {
        Destroy(gameObject, lifeTime);
	}
}
