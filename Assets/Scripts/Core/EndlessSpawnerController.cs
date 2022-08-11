using System.Collections;
using UnityEngine;

namespace Core
{
    public class EndlessSpawnerController : MonoBehaviour
    {
        [Header("Spawner")] [SerializeField] private float spawnTimeRate;
        [SerializeField] private Vector2 spawnRange;
        [Range(0, 1)] [SerializeField] private float humanSpawnChance;
        [SerializeField] private Vector2Int humanSpawnPityRange;
        private int _humanSpawnPity;
        [SerializeField] private GameObject[] zombieObjects;
        [SerializeField] private GameObject[] humanObjects;

        [Header("Waves")] [HideInInspector] public int waveCount;
        [SerializeField] private float _initialSpawnLeft;
        [SerializeField] private int _delayBetweenWaves;
        private int _spawnLeft;
        private float _timeSinceLastWave;

        [Header("Difficulty")] [Range(1, 2)] [SerializeField]
        private float _difficultyIncreaseRate;

        private void Start()
        {
            NextWave();
        }

        private void NextWave()
        {
            waveCount++;
            _spawnLeft = (int)(_initialSpawnLeft + waveCount * _difficultyIncreaseRate);
            InvokeRepeating(nameof(SpawnObject), spawnTimeRate, spawnTimeRate);
            UIManager.Instance.UpdateWaveName($"Wave: {waveCount}");
        }

        IEnumerator WaveFinish(int seconds)
        {
            CancelInvoke(nameof(SpawnObject));
            for (int i = seconds - 1; i >= 0; i--)
            {
                yield return new WaitForSeconds(1);
                UIManager.Instance.UpdateTimeUI(i);
            }

            NextWave();
        }

        private void SpawnObject()
        {
            if (_spawnLeft <= 0)
            {
                StartCoroutine(nameof(WaveFinish), _delayBetweenWaves);
                return;
            }

            var random = Random.Range(0, zombieObjects.Length);
            var randomPosition = new Vector3(Random.Range(spawnRange.x, spawnRange.y), transform.position.y, 0);
            var randomObject = zombieObjects[random];
            var randomHuman = Random.Range(0f, 1f);
            if (randomHuman < humanSpawnChance || _humanSpawnPity <= 0)
            {
                _humanSpawnPity = Random.Range(humanSpawnPityRange.x, humanSpawnPityRange.y);
                randomObject = humanObjects[Random.Range(0, humanObjects.Length)];
            }

            Instantiate(randomObject, randomPosition, Quaternion.identity, transform);
            _spawnLeft--;
            _humanSpawnPity--;

            UIManager.Instance.UpdateSpawnCountUI(_spawnLeft);
        }
    }
}