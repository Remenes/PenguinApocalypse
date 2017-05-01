using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public GameObject projectile;
    private Rigidbody2D shot_rg;
    public float projectile_speed = 150;

	// Use this for initialization
	void Start () {
        
		
	}

    void Fire()
    {
        GameObject shot = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        //give initial veloctiy
        shot_rg = shot.GetComponent<Rigidbody2D>();
        shot_rg.velocity = new Vector3(0, projectile_speed, 0);
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            InvokeRepeating("Fire", 0.00001f, .02f);
        if (Input.GetKeyUp(KeyCode.Space))
            CancelInvoke("Fire");
    }
}
