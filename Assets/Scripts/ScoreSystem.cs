using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreSystem : MonoBehaviour
{
    public static float score { get; private set; }

    public Text scoreDislay;
    public Text deathScoreDislay;
    public Text highscoreDisplay;
	
	public void ChangeScore (float amount)
    {
        score += amount;
        scoreDislay.text = "Score= " + score.ToString();
	}
	
	public void DisplayHighscores ()
    {
        List<float> highscores = new List<float>();

        //Put all saved highscores and currrent score into a list
        highscores.Add(PlayerPrefs.GetFloat("Highscore1"));
        highscores.Add(PlayerPrefs.GetFloat("Highscore2"));
        highscores.Add(PlayerPrefs.GetFloat("Highscore3"));
        highscores.Add(PlayerPrefs.GetFloat("Highscore4"));
        highscores.Add(PlayerPrefs.GetFloat("Highscore5"));
        highscores.Add(score);

        highscores.Sort();
        highscores.Remove(highscores[0]);

        //Save highscores
        PlayerPrefs.SetFloat("Highscore1", highscores[4]);
        PlayerPrefs.SetFloat("Highscore2", highscores[3]);
        PlayerPrefs.SetFloat("Highscore3", highscores[2]);
        PlayerPrefs.SetFloat("Highscore4", highscores[1]);
        PlayerPrefs.SetFloat("Highscore5", highscores[0]);

        //Display highscores
        string displayText ="";
        for (int a =0; a < highscores.Count; a++)
        {
            displayText += (a + 1) + ": " + highscores[highscores.Count - a - 1];
            if (a != highscores.Count - 1)
            {
                displayText += "\n";
            }
        }

        highscoreDisplay.text = displayText;
        deathScoreDislay.text += score;
    }
}
