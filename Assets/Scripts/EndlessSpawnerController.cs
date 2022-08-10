using System.Collections;
using UnityEngine;

public class EndlessSpawnerController : SingletonMonoBehaviour<EndlessSpawnerController>
{
    [Header("Spawner")] [SerializeField] private float spawnTimeRate;
    [SerializeField] private Vector2 spawnRange;
    [Range(0, 1)] [SerializeField] private float humanSpawnChance;
    [SerializeField] private Vector2Int humanSpawnPityRange;
    private int _humanSpawnPity;
    [SerializeField] private GameObject[] spawnObjects;

    [Header("Waves")] [HideInInspector] public int waveCount;
    [SerializeField] private float _initialSpawnLeft;
    private int _spawnLeft;
    [SerializeField] private int _delayBetweenWaves;
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

        var random = Random.Range(1, spawnObjects.Length);
        var randomPosition = new Vector3(Random.Range(spawnRange.x, spawnRange.y), transform.position.y, 0);
        var randomObject = spawnObjects[random];
        var randomHuman = Random.Range(0f, 1f);
        if (randomHuman < humanSpawnChance || _humanSpawnPity <= 0)
        {
            _humanSpawnPity = Random.Range(humanSpawnPityRange.x, humanSpawnPityRange.y);
            randomObject = spawnObjects[0];
        }

        Instantiate(randomObject, randomPosition, Quaternion.identity, transform);
        _spawnLeft--;
        _humanSpawnPity--;

        UIManager.Instance.UpdateSpawnCountUI(_spawnLeft);
    }
}