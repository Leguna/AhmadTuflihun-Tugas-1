using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    private GameManager _gameManager;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text healthText;
    [SerializeField] private Text waveTime;
    [SerializeField] private Text waveName;
    [SerializeField] private Text spawnCount;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        UpdateHealthUI();
        UpdateScoreUI();
        UpdateTimeUI(0);
    }

    public void UpdateSpawnCountUI(int count)
    {
        spawnCount.text = count <= 0 ? "" : $"{count} Incoming";
    }

    public void UpdateScoreUI()
    {
        scoreText.text = $"Score: {Math.Clamp(_gameManager.Score, 0, int.MaxValue)}";
    }

    public void UpdateHealthUI()
    {
        healthText.text = $"Health: {Math.Clamp(_gameManager.Health, 0, int.MaxValue)}";
    }

    public void UpdateTimeUI(float time)
    {
        waveTime.text = GetTimeString(time);
    }

    private string GetTimeString(float timeInSeconds)
    {
        return timeInSeconds <= 0 ? "" : $"{timeInSeconds / 60:00}:{timeInSeconds % 60:00}";
    }

    public void UpdateWaveName(string currentWaveName)
    {
        waveName.text = currentWaveName;
    }
}