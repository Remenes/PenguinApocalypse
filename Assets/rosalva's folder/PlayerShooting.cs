using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public GameObject projectile;
    private Rigidbody2D shot_rg;
    public float projectile_speed = 150;
    public float repeat_rate = .2f;


    private Transform playerShootingPoint;

	// Use this for initialization
	void Start () {
        playerShootingPoint = transform.GetChild(0);
	}

    void Fire()
    {
        GameObject shot = Instantiate(projectile, playerShootingPoint.position, Quaternion.identity) as GameObject;
        //give initial veloctiy
        shot_rg = shot.GetComponent<Rigidbody2D>();
        //shot_rg.velocity = new Vector3(0, projectile_speed, 0);

        Vector3 sp = Camera.main.WorldToScreenPoint(playerShootingPoint.position);
        Vector3 dir = (Input.mousePosition - sp).normalized;

        shot_rg.AddForce(dir*projectile_speed);


		shot_rg.velocity = dir*projectile_speed;
		Destroy (shot, 3f);
    }

	
	void Update () {
        if (Input.GetMouseButtonDown(0))
          InvokeRepeating("Fire", 0.00001f, repeat_rate);
        if (Input.GetMouseButtonUp(0))
          CancelInvoke("Fire");

        //transform.Translate(Vector3.forward * Time.deltaTime);
    }
}
