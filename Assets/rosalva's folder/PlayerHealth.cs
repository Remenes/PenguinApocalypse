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

	//void Update () { TakeDamage(20); }
}
