using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth { get; private set; }
    public float maxHealth { get; private set; }

    public float initialHealth; //For now, maybe change to better method later

    public event Helper.VoidDelegate OnDeath;

	void Start ()
    {
        maxHealth = initialHealth;
        currentHealth = maxHealth;
	}
	
	public void TakeDamage (float amount)
    {
        Debug.Log(gameObject.name + " has taken " + amount + " damage.");

        //Maybe spawn a particle system or something?

        currentHealth -= amount;
        if (currentHealth <= 0) //If no health left, then die
        {
            Die();
        }
	}

    public void Heal(float amount)
    {
        Debug.Log(gameObject.name + " has healed " + amount + " health.");

        //Maybe spawn a particle system or something?

        currentHealth += amount;
        if (currentHealth <= 0) //Just in case TakeDamage didn't catch it
        {
            Die();
        }
    }

    public void Die()
    {
        OnDeath();
    }
}
