using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public int Health = 3;
    public int CurrentWave = 1;
    [HideInInspector] public float TotalPlayTime;

    public int Score;

    public GameObject spawner;
    public GameObject enemies;

    private void Update()
    {
        TotalPlayTime += Time.deltaTime;
    }

    public void Hit(int hitPoint)
    {
        Health -= hitPoint;
        UIManager.Instance.UpdateHealthUI();
        if (Health <= 0) GameOver();
    }

    public void AddScore()
    {
        Score++;
        UIManager.Instance.UpdateScoreUI();
    }

    public void GameOver()
    {
        print("Game Over");
    }
}