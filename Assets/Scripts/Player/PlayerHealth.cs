using UnityEngine;

public class PlayerHealth : Health
{

    private HUDManager hud;
    public GameObject hpack;

    protected override void Awake()
    {
        base.Awake();
        hud = FindObjectOfType<HUDManager>();
    }

    private void Start()
    {
        hud.UpdateHealthBar();
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        hud.UpdateHealthBar();
    }

    public override void Heal(float amount)
    {
        base.Heal(amount);
        hud.UpdateHealthBar();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(hpack.tag))
        {
            Heal(collision.gameObject.GetComponent<HealthPickUp>().health);
            Destroy(collision.gameObject);
        }
    }

}
