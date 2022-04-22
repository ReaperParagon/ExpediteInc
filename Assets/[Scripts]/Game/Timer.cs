using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    public float timeRemaining = 0.1f;

    private void Update()
    {
        if (timeRemaining > 0.0f)
            UpdateTimer();
    }

    private void OnEnable()
    {
        ObjectTracker.OnSetComplete += AddTime;
    }
    
    private void OnDisable()
    {
        ObjectTracker.OnSetComplete -= AddTime;
    }

    /// Functions ///

    public void AddTime()
    {
        timeRemaining += 10.0f;
    }

    private void UpdateTimer()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0.0f) timeRemaining = 0.0f;
        timerText.text = timeRemaining.ToString("0.000") + "s";

        if (timeRemaining <= 0.0f)
            GameOver();
    }

    private void GameOver()
    {
        FindObjectOfType<GameManager>().EndGame();
    }
}
