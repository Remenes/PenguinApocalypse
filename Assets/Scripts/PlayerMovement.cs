using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private float horiMovement;
    private float vertMovement;
    private bool facingLeft;
    private bool sprintOn;
    private float speed;
    private float timer;

    public float MoveSpeed = 5f;
    public float SprintSpeed = 20f;
    public KeyCode SprintKey = KeyCode.Space;
    public float SprintTime = 1f;

    
    // Use this for initialization
	void Start () {

        speed = MoveSpeed;
        facingLeft = true;
        sprintOn = false;
		
	}
	
	// Update is called once per frame
	void Update () {
        getInputAxis();
        if (!sprintOn && Input.GetKeyDown(KeyCode.Space))
        {
            sprintOn = true;
            speed = SprintSpeed;
            timer = 0;
        }

        else if (sprintOn && timer < SprintTime)
        {
            timer += Time.deltaTime;
        }

        if (timer > SprintTime)
        {
            sprintOn = false;
            timer = 0;
            speed = MoveSpeed;
        }
        movePlayer();

    }

    private void movePlayer() {
        Vector3 offset = new Vector2(horiMovement, vertMovement);
        if (offset.sqrMagnitude > 1)
            Vector3.Normalize(offset);
        offset *= speed;

        transform.position += offset * Time.deltaTime;
        flip(horiMovement);
    }

    private void getInputAxis() {

        horiMovement = Input.GetAxis("Horizontal");
        vertMovement = Input.GetAxis("Vertical");
    }

    private void flip(float horizontal)
    {
        if (horizontal > 0 && facingLeft || horizontal < 0 && !facingLeft)
        {
            facingLeft = !facingLeft;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void dash()
    {
        Vector3 offset = new Vector2(horiMovement, vertMovement);
        if (offset.sqrMagnitude > 1)
            Vector3.Normalize(offset);
        offset *= SprintSpeed;

        transform.position += offset * Time.deltaTime;

    }
}
