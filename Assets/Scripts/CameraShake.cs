using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    private float currentShakeTime;

    private const float DEFAULT_SHAKE_STRENGTH = .55f;
    public float shakeStrength;

    public float delayBetweenShakes = .05f;

    private float currentDelayShakeTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        /* TEST */
        if (Input.GetKeyDown(KeyCode.Space)) {
            ShakeScreen(.55f, shakeStrength);
        }
        /* END TEST */

		if (currentShakeTime > 0) {
            if (currentDelayShakeTime > delayBetweenShakes) {
                currentDelayShakeTime = 0;
                transform.localPosition = Vector2To3.convert(Random.insideUnitCircle * shakeStrength);
            }
            currentShakeTime -= Time.deltaTime;
            currentDelayShakeTime += Time.deltaTime;
        }
        else {
            transform.localPosition = Vector3.zero;
        }
	}

    public void ShakeScreen(float shakeTime, float shakeStrength = DEFAULT_SHAKE_STRENGTH) {
        print("Shake");
        this.currentShakeTime = shakeTime;
        this.shakeStrength = shakeStrength;
    }

}
