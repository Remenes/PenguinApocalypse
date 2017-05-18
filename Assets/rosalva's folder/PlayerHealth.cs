using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player is dead");
        }
    }

    public void RestoreHealth(int health)
    {
        currentHealth += health;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<HealthPickUp>() != null)
            RestoreHealth(collision.gameObject.GetComponent<HealthPickUp>().health);
    }

    //void Update () { TakeDamage(20); }
}
