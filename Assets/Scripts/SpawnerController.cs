using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerController : SingletonMonoBehaviour<SpawnerController>
{
    [SerializeField] private Vector2 spawnLimitPositionX;
    private float _spawnStartPositionY;

    [SerializeField] private List<Wave> _waves;
    [SerializeField] private List<GameObject> _zombiePrefab;
    [SerializeField] private List<GameObject> _humanPrefab;

    private Wave _currentWave;
    private float _currenDelaySpawn;
    private int _spawnCount;
    private float _timeRate;
    private int _lastHumanSpawn;
    [SerializeField] private float _delayBetweenWave;
    [SerializeField] private Vector2Int _randomHumanSpawnTimeRateLimit = new(4, 8);

    private void Start()
    {
        _spawnStartPositionY = transform.position.y;
        NextWave();
    }

    private void Update()
    {
        if (GameManager.Instance.isGamePaused) return;

        if (_currenDelaySpawn > 0)
        {
            _currenDelaySpawn -= Time.deltaTime;
            UIManager.Instance.UpdateTimeUI(_currenDelaySpawn);
            return;
        }

        if (_spawnCount <= 0)
        {
            NextWave();
            return;
        }

        if (_timeRate > 0)
        {
            _timeRate -= Time.deltaTime;
            return;
        }


        if (_spawnCount > 0)
        {
            _spawnCount--;
            UIManager.Instance.UpdateSpawnCountUI(_spawnCount);
            _timeRate = GetRandomTimeRate(_currentWave.timeSpawnRate.x, _currentWave.timeSpawnRate.y);
            Spawn();
        }
    }

    private void NextWave()
    {
        GameManager.Instance.CurrentWave++;
        _currentWave =
            _waves[GameManager.Instance.CurrentWave >= _waves.Count ? 0 : GameManager.Instance.CurrentWave];
        _spawnCount = _currentWave.totalSpawn;
        _currenDelaySpawn = _delayBetweenWave;
        print($"Wave: {_currentWave.name}");
        UIManager.Instance.UpdateWaveName(_currentWave.name);
    }

    private void Spawn()
    {
        GameObject spawnedObject;
        if (_lastHumanSpawn == 0)
        {
            _lastHumanSpawn = Random.Range(_randomHumanSpawnTimeRateLimit.x, _randomHumanSpawnTimeRateLimit.y);
            spawnedObject = Instantiate(_humanPrefab[0], GetRandomSpawnPosition(), Quaternion.identity,
                GameManager.Instance.spawner.transform);
        }
        else
        {
            _lastHumanSpawn--;
            spawnedObject = Instantiate(_zombiePrefab[0], GetRandomSpawnPosition(), Quaternion.identity,
                GameManager.Instance.spawner.transform);
        }

        spawnedObject.TryGetComponent(out ZombieController zombieController);
        GameManager.Instance.zombieControllers.Add(zombieController);
    }

    private float GetRandomTimeRate(float min, float max)
    {
        _timeRate = Random.Range(min, max);
        return _timeRate;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(Random.Range(spawnLimitPositionX.x, spawnLimitPositionX.y), _spawnStartPositionY);
    }
}

[Serializable]
public class Wave
{
    public string name;
    public Vector2 timeSpawnRate;
    public Vector2 humanTimeRate;
    public int totalSpawn;
}