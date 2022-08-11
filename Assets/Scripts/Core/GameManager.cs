using System.Collections.Generic;
using Character;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Core
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        public int Health = 10;
        public int Score;

        [SerializeField] private PlayerController _playerController;
        [SerializeField] private GameObject _gameOverUI;
        [HideInInspector] public List<BaseCharacter> characterControllers;
        public bool isGameOver;
        private void Start()
        {
            ResumeGame();
        }

        private void ResumeGame()
        {
            Time.timeScale = 1;
        }

        private void OnEnable()
        {
            _playerController.OnDestroyedEvent += GameOver;
        }

        private void OnDisable()
        {
            _playerController.OnDestroyedEvent -= GameOver;
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            foreach (var charController in characterControllers) charController.StopMove();
        }

        public void Hit(int hitPoint)
        {
            Health -= hitPoint;
            if (Health <= 0)
            {
                Health = 0;
                GameOver();
            }

            UIManager.Instance.UpdateHealthUI();
        }

        public void AddScore()
        {
            Score++;
            UIManager.Instance.UpdateScoreUI();
        }

        private void GameOver()
        {
            PauseGame();
            isGameOver = true;
            _gameOverUI.SetActive(true);
        }

        public void TryAgain() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}