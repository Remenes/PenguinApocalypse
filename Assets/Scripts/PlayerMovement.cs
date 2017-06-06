using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private float horiMovement;
    private float vertMovement;
    private bool facingLeft;
    private bool sprintOn;
    private bool sprintCooled = false;
    private float speed;
    private float timer;
    private float lastSprint = -10.0f; //Time the last sprint ended

    public float MoveSpeed = 5f;
    public float SprintSpeed = 20f;
    public KeyCode SprintKey = KeyCode.Space;
    public float SprintTime = 1f;
    public float SprintCooldown = 3f;
    public SpriteRenderer background;

    public bool is3D = false;
    
    // Use this for initialization
	void Start () {

        speed = MoveSpeed;
        facingLeft = true;
        sprintOn = false;
		
	}
	
	// Update is called once per frame
	void Update () {
        getInputAxis();
        if (!sprintOn && Input.GetKeyDown(SprintKey) && sprintCooled) //Start sprinting
        {
            sprintOn = true;
            sprintCooled = false;
            speed = SprintSpeed;
            timer = 0;
        }
        else if (sprintOn && timer < SprintTime) //Keep sprinting
        {
            timer += Time.deltaTime;

            HUDManager.reference.UpdateSprintBar(timer, SprintTime, false);
        }

        if (timer > SprintTime) //Stop sprinting
        {
            sprintOn = false;
            timer = 0;
            speed = MoveSpeed;
            lastSprint = Time.time;
            
            //HUDManager.reference.UpdateSprintBar(0);
        }
        movePlayer();

        float timeSinceLastSprint = Time.time - lastSprint;
        if(timeSinceLastSprint < SprintCooldown) //Sprint is still cooling down
        {
            HUDManager.reference.UpdateSprintBar(timeSinceLastSprint, SprintCooldown, true);
        }
        else if (!sprintOn) //Sprint ass cooled down
        {
            sprintCooled = true;

            HUDManager.reference.UpdateSprintBar(1, 1, true);
        }
    }

    private void movePlayer() {
        Vector3 offset;
        if (is3D)
        {
            offset = new Vector3(horiMovement, 0, vertMovement);
        }
        else
        {
            offset = new Vector2(horiMovement, vertMovement);
        }
        if (offset.sqrMagnitude > 1)
            Vector3.Normalize(offset);
        offset *= speed;
        Vector3 newPos = transform.position + offset * Time.deltaTime;

        if (background.bounds.Contains(newPos))
        {
            transform.position = newPos;
        }
        //transform.position += offset * Time.deltaTime;
        flip(Input.mousePosition);
    }

    private void getInputAxis() {

        horiMovement = Input.GetAxis("Horizontal");
        vertMovement = Input.GetAxis("Vertical");
    }

    private void flip(Vector3 mousePos)
    {
        if (mousePos.x > Screen.width/2 && facingLeft || mousePos.x < Screen.width/2 && !facingLeft)
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
