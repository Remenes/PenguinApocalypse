using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private Health enemyHealth;

    private void Awake()
    {
        if(GetComponent<Health>() == null)
        {
            Debug.LogError("There is no health script on " + gameObject.name + "! Please add one!", this);
        }
        else
        {
            enemyHealth = GetComponent<Health>();
            enemyHealth.OnDeath += GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().decrementEnemyCount;
            enemyHealth.OnDeath += EnemyDie;
        }
    }

    private void EnemyDie ()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        Destroy(gameObject, /*0.5f*/ 0);
	}

    private void OnDestroy()
    {
        enemyHealth.OnDeath -= EnemyDie;
    }
}
