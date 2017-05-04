using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private float horiMovement;
    private float vertMovement;

    public float MoveSpeed = 5f;
    
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        getInputAxis();
        movePlayer();

        // attempted to rotate game object based on direction of movement
        //if (transform.rotation.y == 0 && horiMovement > 0)
        //    transform.Rotate(0, 180, 0);
        //if (transform.rotation.y == 180f && horiMovement < 0)
        //    transform.Rotate(0, -180, 0);
    }

    private void movePlayer() {
        Vector3 offset = new Vector2(horiMovement, vertMovement);
        if (offset.sqrMagnitude > 1)
            Vector3.Normalize(offset);
        offset *= MoveSpeed;

        transform.position += offset * Time.deltaTime;
    }

    private void getInputAxis() {
        horiMovement = Input.GetAxis("Horizontal");
        vertMovement = Input.GetAxis("Vertical");
    }
}
