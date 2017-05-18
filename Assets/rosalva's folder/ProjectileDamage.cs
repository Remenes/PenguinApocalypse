using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour {

    public float damage = 10;

    void OnTriggerEnter2D(Collider2D coll)
    {
		if (coll.gameObject.tag == "Enemy") {
			coll.gameObject.GetComponent<Health> ().TakeDamage (damage);
			//Destroy (coll.gameObject);
			Destroy (gameObject);
		}
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
