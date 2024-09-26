using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<GameObject> targets;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    private int livesValue=0;
    public float spawnRate = 1.0f;

    public GameObject gameOverPanel;
    public bool isGameActive;

    public Button restartButton;


    public GameObject PausePanel;
    public TrailRenderer trails;
    public bool isPaused;
    void Start()
    {


        restartButton.onClick.AddListener(RestartGame);
    }

    public void StartGame(int diff)
    {
        ChangeLives(3);
        spawnRate /= diff;
        isGameActive = true;
        StartCoroutine(SpawnTrget());
        score = 0;
        scoreText.text = "Score" + score;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PausedGame();
        }
        trails.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
Input.mousePosition.y, 10.0f));
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver()
    {
        gameOverPanel.gameObject.SetActive(true);
        isGameActive = false;
    }
    IEnumerator SpawnTrget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score" + score;
    }


    public void ChangeLives(int newLivesValue)
    {
        livesValue += newLivesValue;
        livesText.text = "Lives:" + livesValue;
        if (livesValue <= 0)
        {
            GameOver();
        }
    }


    void PausedGame()
    {

        if (!isPaused)
        {
            isPaused = true;
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            isPaused = false;
            PausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
