using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyDefaultSpawner : MonoBehaviour {
    
    private CircleCollider2D collider;
    private EnemyType enemyType = EnemyType.DEFAULT;
    public EnemyType type {
        get { return enemyType; }
    }

    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private int test_NumStartingEnemies;
    private int numberOfEnemiesLeft;
    [SerializeField]
    private float enemySpawnTime;
    public float spawnEnemy_Time {
        get { return enemySpawnTime; }
        set { enemySpawnTime = value; }
    }
    
	// Use this for initialization
	void Start () {
        collider = GetComponent<CircleCollider2D>();

        //Testing
        StartEnemySpawner(test_NumStartingEnemies);
	}
    
    protected virtual IEnumerator spawnEnemy() {
        float elapsedTime = 0;
        while (numberOfEnemiesLeft > 0) {
            if (elapsedTime >= enemySpawnTime && SpawnCondition()) {
                elapsedTime = 0;
                numberOfEnemiesLeft--;

                //SpawnEnemy
                Vector2 newEnemyPosition = (Vector2)transform.position + Random.insideUnitCircle * collider.radius;
                GameObject newEnemy = Instantiate(enemy, newEnemyPosition, new Quaternion());

            }
            elapsedTime += Time.deltaTime;
            yield return 0;
        }
    }

    protected virtual bool SpawnCondition() {
        return true;
    }

    public void StartEnemySpawner(int newNumOfEnemies, float? newSpawningTime = null) {
        if (numberOfEnemiesLeft > 0) {
            throw new System.Exception(string.Format("Trying to Start Enemy Spawn for {0} but there are still {1} enemies left: Coroutine is still in progress.", enemy.ToString(), numberOfEnemiesLeft));
        }
        numberOfEnemiesLeft = newNumOfEnemies;
        if (newSpawningTime.HasValue)
            enemySpawnTime = newSpawningTime.Value;

        StartCoroutine(spawnEnemy());
    }
}
