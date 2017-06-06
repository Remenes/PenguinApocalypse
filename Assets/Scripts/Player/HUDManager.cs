﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class HUDManager : MonoBehaviour
{
    public static HUDManager reference;

    public Image healthBar;
    public Image sprintBar;
    public Text ammoNum;
    public GameObject GameOverScreen; //Should be a child of HUD Manager
    public Text scoreText;
    public EventSystem backupEventSystem;

    private PlayerHealth playerHealth;
    private PlayerMovement playerMove;
    private PlayerShooting playerShoot;
	
	void Awake ()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerShoot = player.GetComponent<PlayerShooting>();
        playerMove = player.GetComponent<PlayerMovement>();

        GameOverScreen.SetActive(false);

        //Singleton pattern for getting reference
        if(reference == null)
        {
            reference = this;
        }
        else if(reference != this)
        {
            Debug.LogWarning("Additional HUD Manager found. It has been destroyed.");
            Destroy(gameObject);
        }

        //Check for Event System
        if (!FindObjectOfType<EventSystem>())
        {
            backupEventSystem.gameObject.SetActive(true);
        }
        else
        {
            Destroy(backupEventSystem.gameObject);
        }
	}
	
	public void UpdateHealthBar ()
    {
        healthBar.fillAmount = playerHealth.currentHealth / playerHealth.maxHealth;
	}

    public void UpdateSprintBar (float curSprintTime, float maxSprintTime, bool isCooldown)
    {
        if (!isCooldown)
        {
            sprintBar.fillAmount = 1 - curSprintTime / maxSprintTime;
        }
        else
        {
            sprintBar.fillAmount = curSprintTime / maxSprintTime;
        }
    }

    public void UpdateAmmoNum (/*float amount*/)
    {
        Debug.Log(playerShoot.name + "is shooting but ammo is not set up yet in HUD.");
        //ammoNum.text = playerShooting.currentAmmo + "/" + playerShooting.maxAmmo;
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        //Insert other restart stuff

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
