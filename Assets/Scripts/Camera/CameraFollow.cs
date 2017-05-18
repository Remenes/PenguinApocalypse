using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private GameObject player;
    private Vector2 playerPosition;

    //private float timeToInterpolate = 5f;
    [SerializeField]
    private float timeToInterpolate = .55f;

    [SerializeField]
    private float aimingDistanceOffset = 3.5f;
    
    private Camera mainCam;
    private float zOffset;

    private Vector2 currentVelocity = Vector2.zero;

    public bool DynamicCam = false;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        zOffset = transform.position.z;
        mainCam = Camera.main;
    }

    // Update Camera after everything is done
    void LateUpdate() {
        playerPosition = player.transform.position;

        //DO EITHER followPlayer_Damp() OR followPlayer_Instant() and followMouse()
        switch (DynamicCam) {
            case true:
                followMouse();
                break;
            case false:
                followPlayer_Damp();
                break;
        }
	}

    //One way to move camera. Smoothly follow the player
    private void followPlayer_Damp() {
        //Vector2 newPosition = Vector2.Lerp(transform.position, player.transform.position, /*Vector2.Distance(player.transform.position, transform.position) * Time.fixedDeltaTime*/ Time.deltaTime * timeToInterpolate);
        Vector2 newPosition = Vector2.SmoothDamp(transform.position, player.transform.position, ref currentVelocity, timeToInterpolate, Mathf.Infinity, Time.deltaTime);
        transform.position = Helper.Vector2toVector3(newPosition, transform.position.z);
    }

    //Another way to move camera. Smoothly follow where to the mouse points. Sort of like a zoom for the mouse, with the origins to the player
    
    private void followMouse() {
        //Get mouse Position on screen
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = -mainCam.transform.position.z;
        Vector2 mousePosition = mainCam.ScreenToWorldPoint(mousePoint);
        
        //Get Camera Target Mouse Position
        Vector2 targetOffset = (mousePosition - playerPosition);
        if (Vector2.Distance(playerPosition, mousePosition) > aimingDistanceOffset) {
            targetOffset = targetOffset.normalized * aimingDistanceOffset;
        }
        Vector2 targetPosition = targetOffset + playerPosition;

        //Change Mouse to target Location
        Vector2 newPosition = Vector2.SmoothDamp(transform.position, targetPosition, ref currentVelocity, timeToInterpolate, Mathf.Infinity, Time.deltaTime);        
        transform.position = Helper.Vector2toVector3(newPosition, zOffset);
    }
}
