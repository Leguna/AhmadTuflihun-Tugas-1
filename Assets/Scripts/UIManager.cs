using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    private GameManager _gameManager;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text healthText;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        UpdateHealthUI();
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        print($"Score: {_gameManager.Score}");
        scoreText.text = $"Score: {_gameManager.Score}";
    }

    public void UpdateHealthUI()
    {
        print($"Score: {_gameManager.Health}");
        healthText.text = $"Health: {_gameManager.Health}";
    }
}