using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_GameInfo : MonoBehaviour {

    [SerializeField]
    private Text currentLevel;
    private void updateLevelText() {
        currentLevel.text = "Level: " + gameManager.getCurrentLevel.ToString();
    }

    [SerializeField]
    private Text enemiesLeft;
    private void updateEnemiesLeftText() {
        enemiesLeft.text = "Enemies Left: " + gameManager.getEnemiesLeft().ToString();
    }

    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GetComponent<GameManager>();

        gameManager.OnLevelChange += updateLevelText;
        gameManager.OnEnemiesLeftChange += updateEnemiesLeftText;
	}
	
}
