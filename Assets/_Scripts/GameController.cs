using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCountMax;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private int score;
    private bool gameOver;
    private bool restart;

    IEnumerable <int> hazardCountRange;
    readonly int hazardCountMin = 0;

	void Awake()
	{
        hazardCountRange = Enumerable.Range(hazardCountMin, hazardCountMax);
	}

	void Start()
	{
        Init();
        StartCoroutine (SpawnWaves());
	}

    void Update()
    {
        if(restart)
        {
            if (Input.touchCount > 0)
            {
                SceneManager.LoadScene(0);
            } else if (Input.anyKey)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            foreach (int count in hazardCountRange)
            {
                SpawnHazard();
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if(gameOver)
            {
                restartText.text = "Tap for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore (int score)
    {
        this.score += score;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }

    void Init()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
    }

    void SpawnHazard()
    {
        GameObject hazard = hazards[Random.Range(0, hazards.Length)];
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Instantiate(hazard, spawnPosition, Quaternion.identity);
    }

    void UpdateScore ()
    {
        scoreText.text = "Score: " + score;
    }
}
