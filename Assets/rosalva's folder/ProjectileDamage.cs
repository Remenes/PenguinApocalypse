using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour {

    float damage = 10;

    void OnTriggerEnter(Collider coll)
    {
        //if (coll.gameObject.tag == "Enemy")
          //  coll.gameObject.GetComponent<Health>().takeDamage(damage);
        Destroy(gameObject);
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
