using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Health playerHealth;

    private void Awake()
    {
        if (GetComponent<Health>() == null)
        {
            Debug.LogError("There is no health script on " + gameObject.name + "! Please add one!", this);
        }
        else
        {
            playerHealth = GetComponent<Health>();
            playerHealth.OnDeath += PlayerDie;
        }
    }

	private void PlayerDie ()
    {
        //Debug.Log("YOU DIED!");
        HUDManager.reference.GameOver();
    }

    private void OnDestroy()
    {
        playerHealth.OnDeath -= PlayerDie;
    }
}
