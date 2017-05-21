using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Slider healthBar;
    public Text ammoNum;

    private PlayerHealth playerHealth;
    private PlayerShooting playerShooting;
	
	void Awake ()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerShooting = player.GetComponent<PlayerShooting>();
	}
	
	public void UpdateHealthBar ()
    {
        if(playerHealth.currentHealth > healthBar.maxValue)
        {
            healthBar.maxValue = playerHealth.maxHealth;
        }
        healthBar.value = playerHealth.currentHealth;
	}

    public void UpdateAmmoNum (/*float amount*/)
    {
        Debug.Log(playerShooting.name + "is shooting but ammo is not set up yet in HUD.");
        //ammoNum.text = playerShooting.currentAmmo + "/" + playerShooting.maxAmmo;
    }
}
