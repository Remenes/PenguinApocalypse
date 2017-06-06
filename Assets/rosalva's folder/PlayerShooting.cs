using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public GameObject projectile;
    private Rigidbody2D shot_rg;
    public float projectile_speed = 150;
    public float repeat_rate = .2f;


    private Transform playerShootingPoint;
    private AudioSource sound;
    private float soundBasePitch;

	// Use this for initialization
	void Start () {
        playerShootingPoint = transform.GetChild(0);
        sound = playerShootingPoint.GetComponent<AudioSource>();
        soundBasePitch = sound.pitch;
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

        sound.pitch = soundBasePitch + Random.Range(-0.1f, 0.1f);
    }

    void MakeSound ()
    {
        Debug.Log(sound.clip.name);
        sound.Play();
    }

	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            InvokeRepeating("Fire", 0, repeat_rate);
            //InvokeRepeating("MakeSound", 0, repeat_rate * 2);
            sound.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("Fire");
            //CancelInvoke("MakeSound");
            sound.Stop();
        }

        //transform.Translate(Vector3.forward * Time.deltaTime);
    }
}
