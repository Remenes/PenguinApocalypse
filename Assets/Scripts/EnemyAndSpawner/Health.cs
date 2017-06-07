using UnityEngine;

public class Health : MonoBehaviour
{
    public Rigidbody HealthPack;
    public float currentHealth { get; protected set; }
    public float maxHealth { get; protected set; }
    public event Helper.VoidDelegate OnDeath;
    public bool spawnHealth = false;

    [SerializeField]
    protected float initialHealth; //For now, maybe change to better method later
    public ParticleSystem hurtParticlesPrefab;
    public ParticleSystem healParticlesPrefab; //Particle systems need to be set to not loop
    public AudioClip hurtSound;
    public AudioClip healSound;

    protected ParticleSystem hurtParticles;
    protected ParticleSystem healParticles;
    protected new AudioSource audio;

    protected virtual void Awake ()
    {
        maxHealth = initialHealth;
        currentHealth = maxHealth;

        if (hurtParticlesPrefab != null)
        {
            //Debug.Log("Adding hurt particles for " + gameObject.name);
            hurtParticles = Instantiate(hurtParticlesPrefab, transform);
            hurtParticles.transform.localPosition = Vector3.zero;
            hurtParticles.Stop();
        }
        if (healParticlesPrefab != null)
        {
            //Debug.Log("Adding heal particles for " + gameObject.name);
            healParticles = Instantiate(healParticlesPrefab, transform);
            healParticles.transform.localPosition = Vector3.zero;
            healParticles.Stop();
        }

        audio = GetComponentInChildren<AudioSource>();
        if (audio != null && (hurtSound == null || healSound == null))
        {
            Debug.LogWarning("Please add sounds to Health on " + gameObject.name, this);
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
            //Debug.Log("Playing hurt particles for " + gameObject.name);
            hurtParticles.Play();
        }

        if(audio != null && !audio.isPlaying)
        {
            if (audio.clip != hurtSound && hurtSound != null)
            {
                audio.clip = hurtSound;
            }
            audio.Play();
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

        if (audio != null && !audio.isPlaying)
        {
            if (audio.clip != healSound && healSound != null)
            {
                audio.clip = healSound;
            }
            audio.Play();
        }
    }

    private bool hasDied = false; //Just to make sure the onDeath doesn't register multiple times when they get hurt quickly
    public void Die()
    {
        if (!hasDied) {
            OnDeath();
            hasDied = true;
        }
        if (spawnHealth == true && Random.Range(1, 10) == 1)
        {
            Instantiate(HealthPack);
        }
    }
}
