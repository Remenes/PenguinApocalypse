﻿using System.Collections;
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
        //shot_rg.velocity = new Vector3(0, projectile_speed, 0);

        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = (Input.mousePosition - sp).normalized;
        shot_rg.AddForce(dir*200);

    }

	
	void Update () {
        if (Input.GetMouseButtonDown(0))
          InvokeRepeating("Fire", 0.00001f, .02f);
        if (Input.GetMouseButtonUp(0))
          CancelInvoke("Fire");

        //transform.Translate(Vector3.forward * Time.deltaTime);
    }
}
