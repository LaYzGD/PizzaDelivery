using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _nextLevelSpawnPosition;
    [SerializeField] private GameObject[] _levelVariations;

    public Vector3 NextLevelSpawnPosition => _nextLevelSpawnPosition.position;

    private Action _levelSpawnTrigger;

    private void OnEnable()
    {
        _levelVariations[UnityEngine.Random.Range(0, _levelVariations.Length)].SetActive(true);
    }

    private void OnDisable()
    {
        foreach (var level in _levelVariations)
        {
            level.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _levelSpawnTrigger?.Invoke();
    }

    public void SetSpawnAction(Action callback)
    {
        _levelSpawnTrigger = callback;
    }
}
