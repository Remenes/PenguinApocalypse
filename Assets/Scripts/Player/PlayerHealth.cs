using UnityEngine;

public class PlayerHealth : Health
{

    private HUDManager hud;

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


}
