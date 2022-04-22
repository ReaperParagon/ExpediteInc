using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int lives;

    [SerializeField]
    private GameObject resultsUI;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI resultsScoreText;

    [SerializeField]
    private HealthUIManager healthUI;

    [SerializeField]
    private PauseMenu pauseMenu;

    private int score;

    private void Awake()
    {
        // Create Object Color Dictionary
        ObjectColorsDictionary.CreateDictionary();
    }

    private void OnEnable()
    {
        ObjectSpawner.OnObjectSpawn += AddScore;
    }

    private void OnDisable()
    {
        ObjectSpawner.OnObjectSpawn -= AddScore;
    }

    /// Functions ///

    public void LoseLife()
    {
        if (--lives <= 0)
            EndGame();

        UpdateHealthUI();
    }

    public void AddScore()
    {
        score++;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        string scoreString = "Score: " + score.ToString();
        resultsScoreText.text = scoreString;
        scoreText.text = scoreString;
    }

    private void UpdateHealthUI()
    {
        healthUI.UpdateUI(lives);
    }

    private void EndGame()
    {
        resultsUI.SetActive(true);

        pauseMenu.PauseGame(true);
        pauseMenu.allowPauseMenu = false;

        UpdateScoreUI();
    }

}
