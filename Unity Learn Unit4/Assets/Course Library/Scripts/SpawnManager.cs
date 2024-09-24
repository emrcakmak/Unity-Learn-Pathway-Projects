using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPreferb;
    public GameObject[] powerUpGem;

    public GameObject bossPreferb;
    public int enemycount;
    private int newEnemy = 1;
    private int currentRound = 1;
    private int currentBossMinyon = 0;
    private int generateBossRoundCounter = 3;
    private PlayerControllerX Mainplayer;
    public GameObject RestartPanel;
    void Start()
    {
        SpawnEnemyandPowerUpGem(1);
        Instantiate(powerUpGem[0], GeneratePosition(), powerUpGem[0].transform.rotation);
        Mainplayer = GameObject.FindObjectOfType<PlayerControllerX>();
    }

    // Update is called once per frame
    void Update()
    {
        enemycount = FindObjectsOfType<EnemyController>().Length;
        if (Mainplayer.isGameEnd != true)
        {
            if (enemycount == 0)
            {
                currentRound++;
                newEnemy++;

                if (currentRound % generateBossRoundCounter == 0)
                {
                    SpawnBoss();
                }
                else
                    SpawnEnemyandPowerUpGem(newEnemy);

            }
        }
        else
        {
            RestartPanel.SetActive(true);
        }
        
    }

    private Vector3 GeneratePosition()
    {
        float posX = Random.Range(-9, 9);
        float posz = Random.Range(-9, 9);
        Vector3 newPos = new Vector3(posX, 0f, posz);

        return newPos;
    }
    void SpawnBoss() {

        currentBossMinyon = newEnemy - 2;



        var boss = Instantiate(bossPreferb, GeneratePosition(), bossPreferb.transform.rotation);
        boss.GetComponent<EnemyController>().miniEnemySpawn = currentBossMinyon;
    }



    void SpawnEnemyandPowerUpGem(int enemtCount)
    {
        int powerUpSize = Random.Range(0, newEnemy);
        if (powerUpSize == 0)
        {
            powerUpSize = 1;
        }

        for (int i = 0; i < enemtCount; i++)
        {

            Instantiate(enemyPreferb, GeneratePosition(), enemyPreferb.transform.rotation);
        }
        for (int i = 0; i < powerUpSize; i++)
        {
            int powerUpBhevior = Random.Range(0, powerUpGem.Length);
            Instantiate(powerUpGem[powerUpBhevior], GeneratePosition(), powerUpGem[powerUpBhevior].transform.rotation);
        }
    }

    public void SpawnMiniEnemy(int bossMin)
    {
       
        for(int i = 0;i< bossMin; i++)
        {

            Instantiate(enemyPreferb, GeneratePosition(), enemyPreferb.transform.rotation);
        }


    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
