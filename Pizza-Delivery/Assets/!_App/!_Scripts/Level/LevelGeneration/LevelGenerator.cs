using System;
using UnityEngine;
using UnityEngine.Pool;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Level _levelPrefab;

    private ObjectPool<Level> _levelPool;
    private Vector3 _currentLevelSpawnPoint;
    private Level _currentLevel;
    private Level _lastLevel;

    private void Awake()
    {
        _levelPool = new ObjectPool<Level>(OnCreatePrefab, OnGetObject, OnReleaseObject);
        InitializeLevel();
    }

    private Level OnCreatePrefab()
    {
        return Instantiate(_levelPrefab);
    }

    private void OnGetObject(Level level)
    {
        level.gameObject.SetActive(true);
        level.SetSpawnAction(SpawnNewLevel);
    }

    private void OnReleaseObject(Level level)
    {
        level.gameObject.SetActive(false);
    }

    private void InitializeLevel()
    {
        _currentLevelSpawnPoint = Vector3.zero;
        _lastLevel = null;
        _currentLevel = null;
        SpawnNewLevel();
    }

    private void SpawnNewLevel()
    {
        var level = _levelPool.Get();
        level.transform.position = _currentLevelSpawnPoint;
        _currentLevelSpawnPoint = level.NextLevelSpawnPosition;

        if (_lastLevel != null)
        {
            _levelPool.Release(_lastLevel);
        }

        _lastLevel = _currentLevel;
        _currentLevel = level;
    }
}
