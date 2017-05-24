using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour {
    
    public event Helper.VoidDelegate OnLevelChange = delegate { };

    private int currentLevel = 0; // To be increased to one when the game starts
    private int totalLevels;
    public int getCurrentLevel {
        get { return currentLevel; }
    }

    public event Helper.VoidDelegate OnEnemiesLeftChange = delegate { };

    [SerializeField]
    private EnemySpawner enemySpawner;
    //Enemy count
    private static int enemiesLeft = 0;
    public int getEnemiesLeft() {
        return enemiesLeft;
    }

    //List of levels which points to Enemytypes and their amounts
    //Index = 0 would be level 1
    private List<Dictionary<EnemyType, KeyValuePair<int, float?>>> spawnInfo = new List<Dictionary<EnemyType, KeyValuePair<int, float?>>>();
    [SerializeField]
    private TextAsset spawnInfoFile;
    private void readSpawnInfoToList() {
        StreamReader readSpawnInfo = new StreamReader("Assets/Scripts/GameManager/" + spawnInfoFile.name + ".txt");
        using (readSpawnInfo) {

            //Save the level, starts at -1 because of 0-indexing, and 1 is added once the text reads a "----Level----"
            totalLevels = -1;
            //Go through the text file
            string line;
            while ((line = readSpawnInfo.ReadLine()) != null) {

                //Check to see if the initial char is special
                if (line.StartsWith("#") || line.Trim().Equals(""))
                    continue;
                else if (line[0] == '-') {
                    spawnInfo.Add(new Dictionary<EnemyType, KeyValuePair<int, float?>>());
                    ++totalLevels;
                    continue;
                }

                //Split the line in the format corresponding to the one told in the text file
                string[] enemySpawnInfo = line.Split(':');
                EnemyType enemyType = (EnemyType) System.Enum.Parse(typeof(EnemyType), enemySpawnInfo[0].Trim());
                int enemyAmount = System.Convert.ToInt16(enemySpawnInfo[1].Trim());
                float? enemySpawnAmountPerSecond = null;
                if (enemySpawnInfo.Length > 2) { // Also contains a new timer
                    if (!enemySpawnInfo[2].Trim().Equals(""))
                        enemySpawnAmountPerSecond = System.Convert.ToInt16(enemySpawnInfo[2].Trim());
                }

                spawnInfo[totalLevels].Add(enemyType, new KeyValuePair<int, float?>(enemyAmount, enemySpawnAmountPerSecond) );
            }
            ++totalLevels;

            ////DEBUG PRINTING
            //for (int level = 0; level < totalLevels; ++level) {
            //    //foreach (Dictionary<EnemyType, int> value in spawnInfo) {
            //    print("LEVEL: " + (level+1));
            //    foreach (var pair in spawnInfo[level]) {
            //        print("KEY: " + pair.Key + " | " + pair.Value);
            //    }
            //}
        }
        readSpawnInfo.Close();
    }

    // ---------------------- Use this for initialization ------------------------------------

    void Start() {
        readSpawnInfoToList();
        increaseLevel();
    }

    // --------------------- Extra Methods -----------------

    public void decrementEnemyCount() {
        --enemiesLeft;

        OnEnemiesLeftChange();

        if (enemiesLeft == 0) {
            StartCoroutine(nextLevel());
        } else if (enemiesLeft < 0) {
            throw new System.Exception("Enemy Left is less then 0!!!");
        }
    }

    private IEnumerator nextLevel() {
        yield return new WaitForSeconds(2.0f);
        increaseLevel();
    }

    private void increaseLevel() {
        currentLevel++;

        OnLevelChange();

        if (currentLevel > totalLevels) {
            //Change the exception to something more valuable later on
            throw new System.Exception("Current Level passed Total levels");
        }
        else {
            foreach (var pair in spawnInfo[currentLevel - 1]) {
                EnemyType enemyType = pair.Key;
                int enemyAmount = pair.Value.Key;
                float? enemySpawnTime = pair.Value.Value;

                enemySpawner.StartEnemySpawner(enemyType, enemyAmount, enemySpawnTime);
                enemiesLeft += enemyAmount;
            }
            OnEnemiesLeftChange();
        }
    }

}
