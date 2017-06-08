using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public float pointValue;

    private Health enemyHealth;
    private GameManager gameManager;

    private void Awake()
    {
        if(GetComponent<Health>() == null)
        {
            Debug.LogError("There is no health script on " + gameObject.name + "! Please add one!", this);
        }
        else
        {
            enemyHealth = GetComponent<Health>();

            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            enemyHealth.OnDeath += gameManager.decrementEnemyCount;
            enemyHealth.OnDeath += EnemyDie;
        }
    }

    private void EnemyDie ()
    {
        GetComponent<SpriteRenderer>().color = Color.red; //Death effect
        HUDManager.reference.scorer.ChangeScore(pointValue); //Add points

        Destroy(gameObject, 0.05f);
	}

    private void OnDestroy()
    {
        enemyHealth.OnDeath -= gameManager.decrementEnemyCount;
        enemyHealth.OnDeath -= EnemyDie;
    }
}
