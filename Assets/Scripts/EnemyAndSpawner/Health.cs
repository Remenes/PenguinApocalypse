using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth { get; protected set; }
    public float maxHealth { get; protected set; }
    public event Helper.VoidDelegate OnDeath;

    [SerializeField]
    protected float initialHealth; //For now, maybe change to better method later
    public ParticleSystem hurtParticlesPrefab;
    public ParticleSystem healParticlesPrefab; //Particle systems need to be set to not loop

    protected ParticleSystem hurtParticles;
    protected ParticleSystem healParticles;

    protected virtual void Awake ()
    {
        maxHealth = initialHealth;
        currentHealth = maxHealth;

        if (hurtParticlesPrefab != null)
        {
            hurtParticles = Instantiate(hurtParticlesPrefab, Vector3.zero, Quaternion.identity, transform);
            hurtParticles.Stop();
        }
        if (healParticlesPrefab != null)
        {
            healParticles = Instantiate(hurtParticlesPrefab, Vector3.zero, Quaternion.identity, transform);
            healParticles.Stop();
        }
    }
	
	public virtual void TakeDamage (float amount)
    {
        //Debug.Log(gameObject.name + " has taken " + amount + " damage.");

        currentHealth -= amount;
        if (currentHealth <= 0) //If no health left, then die
        {
            Die();
        }

        if (hurtParticles != null)
        {
            hurtParticles.Play();
        }
    }

    public virtual void Heal(float amount)
    {
        //Debug.Log(gameObject.name + " has healed " + amount + " health.");

        currentHealth += amount;
        if (currentHealth <= 0) //Just in case TakeDamage didn't catch it
        {
            Die();
        }
        else if (currentHealth > maxHealth)  //Don't let the player have more than their max health
        {
            currentHealth = maxHealth;
        }


        if (healParticles != null)
        {
            healParticles.Play();
        }
    }

    public void Die()
    {
        OnDeath();
    }
}
