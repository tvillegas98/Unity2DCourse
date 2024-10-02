using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    int playerLives = 3;

    [SerializeField]
    TextMeshProUGUI livesText;

    [SerializeField]
    TextMeshProUGUI scoreText;

    int playerScore = 0;

    void Awake()
    {
        int numberOfGameSession = FindObjectsOfType<GameSession>().Length;

        if (numberOfGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void IncreaseCoinScore(int coinValue)
    {
        playerScore += coinValue;
        scoreText.text = playerScore.ToString();
    }

    private void TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
    }
}
