using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType {
    DEFAULT,
    BIG
}

public class EnemySpawner : MonoBehaviour {
    
    private CircleCollider2D[] enemySpawnPoints;
    private void setEnemySpawnPoints() {
        Transform enemySpawnPoints_Parent = transform.GetChild(0);
        enemySpawnPoints = enemySpawnPoints_Parent.GetComponentsInChildren<CircleCollider2D>();
        numOfEnemySpawnPoints = enemySpawnPoints.Length;
        print(numOfEnemySpawnPoints);
        print(enemySpawnPoints[0].transform);
        print(enemySpawnPoints[1].transform);
    }
    private int numOfEnemySpawnPoints;

    public GameObject[] enemies;
    private Dictionary<EnemyType, int> enemiesRemaining = new Dictionary<EnemyType, int>();
    private Dictionary<EnemyType, float> enemiesSpawnTime = new Dictionary<EnemyType, float>();

    private enum SpawnType { ConsecutiveSpawn, LinkSpawnAtAllSpawnPoints }
    [SerializeField]
    private SpawnType spawnType;

    // Use this for initialization
    void Start() {
        setEnemySpawnPoints();

        //TEST
        StartEnemySpawner(EnemyType.DEFAULT, 10, 1);
        StartEnemySpawner(EnemyType.BIG, 1, 10);
    }

    protected virtual IEnumerator spawnEnemy(EnemyType enemy) {
        float elapsedTime = 0;
        while (enemiesRemaining[enemy] > 0) {
            if (elapsedTime >= enemiesSpawnTime[enemy]) {
                elapsedTime = 0;
                
                //SpawnEnemy
                if (spawnType == SpawnType.ConsecutiveSpawn) {
                    spawnEnemyIndividually(enemy);
                } else if (spawnType == SpawnType.LinkSpawnAtAllSpawnPoints) {
                    spawnByLinkingAtSpawnLocations(enemy);
                }
            }
            elapsedTime += Time.deltaTime;
            yield return 0;
        }
    }

    private void spawnEnemyIndividually(EnemyType enemy) {
        CircleCollider2D randomSpawnPoint = enemySpawnPoints[Random.Range(0, numOfEnemySpawnPoints)];
        Vector2 newEnemyPosition = (Vector2)randomSpawnPoint.transform.position + Random.insideUnitCircle * randomSpawnPoint.radius;
        GameObject newEnemy = Instantiate(enemies[(int)enemy], newEnemyPosition, new Quaternion());
        enemiesRemaining[enemy]--;
    }

    private void spawnByLinkingAtSpawnLocations(EnemyType enemy) {
        foreach (CircleCollider2D spawnPoint in enemySpawnPoints) {
            Vector2 newEnemyPosition = (Vector2)spawnPoint.transform.position + Random.insideUnitCircle * spawnPoint.radius;
            GameObject newEnemy = Instantiate(enemies[(int)enemy], newEnemyPosition, new Quaternion());
            enemiesRemaining[enemy]--;
            if (enemiesRemaining[enemy] <= 0)
                return;
        }
    }

    public void StartEnemySpawner(EnemyType enemy, int numOfEnemies, float? newSpawningTime = null) {
        if (enemiesRemaining.ContainsKey(enemy) && enemiesRemaining[enemy] > 0) {
            throw new System.Exception(string.Format("Trying to Start Enemy Spawn for {0} but there are still {1} enemies left: Coroutine is still in progress.", enemy.ToString(), enemiesRemaining[enemy]));
        }
        enemiesRemaining[enemy]= numOfEnemies;
        if (newSpawningTime.HasValue)
            enemiesSpawnTime[enemy] = newSpawningTime.Value;

        StartCoroutine(spawnEnemy(enemy));
    }
}
