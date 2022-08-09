using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public int Health = 3;
    public int CurrentWave = -1;
    [HideInInspector] public float TotalPlayTime;

    public int Score;

    public GameObject spawner;
    public GameObject enemies;

    [SerializeField] private GameObject _gameOverUI;
    [HideInInspector] public List<ZombieController> zombieControllers;
    public bool isGamePaused;

    public void SetGamePause(bool isPaused)
    {
        isGamePaused = isPaused;

        foreach (var zombieController in zombieControllers)
        {
            zombieController.ToggleMove();
        }
    }

    private void Update()
    {
        TotalPlayTime += Time.deltaTime;
    }

    public void Hit(int hitPoint)
    {
        Health -= hitPoint;
        UIManager.Instance.UpdateHealthUI();
        if (Health <= 0)
        {
            Health = 0;
            GameOver();
        }
    }

    public void AddScore()
    {
        Score++;
        UIManager.Instance.UpdateScoreUI();
    }

    public void GameOver()
    {
        SetGamePause(true);
        _gameOverUI.SetActive(true);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}