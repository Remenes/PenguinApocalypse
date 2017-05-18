using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour {

    //NOT FINAL. JUST A TEST
    public int LEVEL = 1;

    private EnemyDefaultSpawner[] defaultSpawners;

	// Use this for initialization
	void Start () {
        defaultSpawners = GetComponentsInChildren<EnemyDefaultSpawner>();
	}
	
	
}
