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
    private int _indexCurrentWave;
    private Wave _currentWave;
    private float _nextWaveTimeCountdown;

    private float _timeRate;
    private int _lastHumanSpawn;
    [SerializeField] private Vector2Int _randomHumanSpawnTimeRateLimit = new(4, 8);

    private void Start()
    {
        _spawnStartPositionY = transform.position.y;
        _currentWave = _waves[GameManager.Instance.CurrentWave];
    }

    private void Update()
    {
        if (_timeRate > 0)
            _timeRate -= Time.deltaTime;
        else
        {
            _timeRate = GetRandomTimeRate(_currentWave.timeSpawnRate.x, _currentWave.timeSpawnRate.y);
            Spawn();
        }

        if (_nextWaveTimeCountdown > 0)
            _nextWaveTimeCountdown -= Time.deltaTime;
        else NextWave();
    }

    private void NextWave()
    {
    }

    private void Spawn()
    {
        if (_lastHumanSpawn == 0)
        {
            _lastHumanSpawn = Random.Range(_randomHumanSpawnTimeRateLimit.x, _randomHumanSpawnTimeRateLimit.y);
            Instantiate(_humanPrefab[0], GetRandomSpawnPosition(), Quaternion.identity,
                GameManager.Instance.spawner.transform);
        }
        else
        {
            _lastHumanSpawn--;
            Instantiate(_zombiePrefab[0], GetRandomSpawnPosition(), Quaternion.identity,
                GameManager.Instance.spawner.transform);
        }
    }

    public float GetRandomTimeRate(float min, float max)
    {
        _timeRate = Random.Range(min, max);
        return _timeRate;
    }

    public Vector3 GetRandomSpawnPosition()
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
    public float totalTime;
    public float totalSpawn;
}